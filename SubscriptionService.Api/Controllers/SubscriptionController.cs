using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application.Services;
using System.Threading.Tasks;

namespace SubscriptionService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetSubscription(int clientId)
        {
            var subscriptionDto = await _subscriptionService.GetSubscription(clientId);

            return Ok(subscriptionDto);
        }

        //[HttpPost("initial")]
        //public async Task CreateSubscription([FromBody] SubscriptionInput subscriptionInput)
        //{
        //    var subscription = new Subscription()
        //    {
        //        SubscriptionStatus = subscriptionInput.SubscriptionStatus,
        //        ClientId = subscriptionInput.ClientId
        //    };

        //    await _subscriptionRepository.CreateSubscriptionAsync(subscription);
        //}
    }
}