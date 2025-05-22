using KaffeeZeit.Server.Controllers.Dtos;
using KaffeeZeit.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Tab = KaffeeZeit.Server.Controllers.Dtos.Tab;

namespace KaffeeZeit.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TabController : ControllerBase
    {
        private readonly TabService _tabService = TabService.Instance;

        public TabController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tabs = _tabService.Tabs.Select(t => new Tab
                {
                    CoworkerName = t.Coworker.Name,
                    CoworkerId = t.Coworker.Id,
                    RunningTab = t.RunningTab,
                    IsNextToPay = t.IsNextToPay
                })
                .ToList();
            return Ok(new RunningTabs { CoworkerTabs = tabs, Revision = _tabService.Revision });
        }

        [HttpPost]
        public IActionResult Post(OrderRequest orderRequest)
        {
            try
            {
                _tabService.HandleOrder(orderRequest);
            }
            catch (InvalidOperationException)
            { 
                var error = new ErrorResponse
                {
                    ErrorCode = "OrderOutOfSyncError",
                    ErrorMessage = "Your order is out of sync, please refresh and try again."

                };
                return BadRequest(error);
            }
            catch (ApplicationException)
            {
                var error = new ErrorResponse
                {
                    ErrorCode = "UseFavoriteAndCostSuppliedError",
                    ErrorMessage = "To use favorite please set drink cost to 0"
                };
                return BadRequest(error);
            }
            return Ok();
        }

        [HttpPost]
        [Route("pay")]
        public IActionResult PostPayment(PaymentRequest paymentRequest)
        {
            try
            {
                _tabService.HandlePayment(paymentRequest);
            }
            catch (InvalidOperationException)
            {
                var error = new ErrorResponse
                {
                    ErrorCode = "NoExistingTabsError",
                    ErrorMessage = "There are no tabs to make a payment towards."
                };
                return BadRequest(error);
            }
            catch (ApplicationException)
            {
                var error = new ErrorResponse
                {
                    ErrorCode = "CoworkerIsNotNextError",
                    ErrorMessage = "The given coworker is not next, if they would like to pay please override next payment and try again."
                };
                return BadRequest();
            }

            return Ok();
        }
    }
}