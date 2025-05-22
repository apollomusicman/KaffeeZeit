using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Models;
using System.Collections.Immutable;
using Tab = KaffeeZeit.Server.Models.Tab;

namespace KaffeeZeit.Server.Service
{
    public sealed class TabService
    {
        // TODO Ideally use dependency injection to manage lifecycle, come back to that if there is time.
        private static readonly Lazy<TabService> _instance = new(() => new TabService());
        // TODO This should be stored in a db or file for fault tolerance. Come back to that if there is time. 
        private readonly HashSet<Tab> _tabs = [];
        private int _revision = 0;
        private decimal _currentOrderBalance = 0;
        private readonly Lock _lock = new();

        public static TabService Instance { get { return _instance.Value; } }
        public ImmutableHashSet<Tab> Tabs { get { return [.. _tabs]; } }
        public int Revision { get { return _revision; } }
        

        public void AddTab (Coworker coworker)
        {
            _tabs.Add(new Tab { Coworker = coworker, IsNextToPay = false });
        }

        public void RemoveTab (Coworker coworker)
        {
            RemoveTabByCoworkerId(coworker.Id);
        }

        public void RemoveTabByCoworkerId(Guid coworkerId)
        {
            var tabToRemove = _tabs.SingleOrDefault(t => t.Coworker.Id == coworkerId);
            if (tabToRemove is not null)
                _tabs.Remove(tabToRemove);
        }

        public void HandleOrder(OrderRequest orderRequest)
        {
            if (orderRequest.Revision != _revision || _currentOrderBalance > 0)
                throw new InvalidOperationException();

            lock (_lock)
            {
                _revision += 1;
                foreach (var order in orderRequest.Orders)
                {
                    if (order.DrinkCost != 0 && order.UseFavorite)
                        throw new ApplicationException();
                    var tab = _tabs.SingleOrDefault(t => t.Coworker.Id == order.CoworkerId);
                    var cost = order.UseFavorite ? tab?.Coworker.FavoriteDrinkCost : order.DrinkCost;
                    tab?.AddToTab(order.DrinkCost);
                }
                _currentOrderBalance = orderRequest.Orders.Sum(o => o.DrinkCost);
                var nextToPay = _tabs.Single(t => t.Coworker.Id == GetWhoIsNextToPay().Id);
                nextToPay.IsNextToPay = true;
            }
        }

        public Coworker GetWhoIsNextToPay()
        {
            var result = _tabs.OrderByDescending(t => t.RunningTab).FirstOrDefault()?.Coworker;
            if (result is null)
                throw new InvalidOperationException();
            return result;
        }

        public void HandlePayment(PaymentRequest paymentRequest)
        {
            var tab = _tabs.SingleOrDefault(t => t.Coworker.Id == paymentRequest.CoworkerId);
            if (tab is null)
                throw new InvalidOperationException();
            if (!paymentRequest.OverrideNext && !tab.IsNextToPay)
            {
                throw new ApplicationException();
            }

            lock (_lock)
            {
                tab.RemoveFromTab(_currentOrderBalance);
                _currentOrderBalance = 0;
                tab.IsNextToPay = false;
            }
        }
    }
}