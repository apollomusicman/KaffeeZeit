namespace KaffeeZeit.Server.Models
{
    public record Tab
    {
        private decimal _runningTab = 0;

        public required Coworker Coworker { get; init; }

        public decimal RunningTab
        {
            get { return _runningTab; }
        }

        public void AddToTab(decimal amount)
        {
            _runningTab += amount;
        }

        public void RemoveFromTab(decimal amount)
        {
            _runningTab -= amount;
        }
    }
}