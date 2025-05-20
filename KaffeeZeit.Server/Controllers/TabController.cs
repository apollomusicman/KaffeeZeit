using System.Net;
using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Tab = KaffeeZeit.Server.Controllers.Dtos.Tab;

namespace KaffeeZeit.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TabController : ControllerBase
    {
        private readonly TabService _tabService = TabService.Instance;

        public TabController()
        {
        }

        [HttpGet]
        public RunningTabs Get()
        {
            var tabs = _tabService.Tabs.Select(t => new Tab
                {
                    CoworkerName = t.Coworker.Name,
                    CoworkerId = t.Coworker.Id,
                    RunningTab = t.RunningTab
                })
                .ToList();
            return new RunningTabs { CoworkerTabs = tabs, Revision = _tabService.Revision };
        }

        [HttpGet]
        [Route("whos-next")]
        public Models.Coworker GetWhoIsNext()
        {
            return _tabService.GetWhoIsNextToPay();
        }

        [HttpPost]
        public HttpResponseMessage Post(OrderRequest orderRequest)
        {
            try
            {
                _tabService.HandleOrder(orderRequest);
            }
            catch (InvalidOperationException)
            { 
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                // TODO add error code or message.
                return response;
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("pay")]
        public HttpResponseMessage PostPayment(PaymentRequest paymentRequest)
        {
            try
            {
                _tabService.HandlePayment(paymentRequest);
            }
            catch (InvalidOperationException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                // TODO add error code or message.
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}