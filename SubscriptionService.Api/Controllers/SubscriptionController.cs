using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Contract;
using SubscriptionService.Entity.Models;
using SubscriptionService.Repository;
using System.Threading.Tasks;

namespace SubscriptionService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionInfoRepository _subscriptionInfoRepository;

        public SubscriptionController(ISubscriptionInfoRepository subscriptionInfoRepository)
        {
            _subscriptionInfoRepository = subscriptionInfoRepository;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetSubscription(int clientId)
        {
            var subscription = await _subscriptionInfoRepository.GetSubscriptionAsync(clientId);

            if (subscription is null)
                return NotFound($"Subscription for client id {clientId} doesn't exist.");

            return Ok(subscription);
        }

        [HttpPost("create")]
        public async Task CreateSubscription([FromBody] SubscriptionDto subInfo)
        {
            var subscriptionInfo = new SubscriptionInfo()
            {
                SubscriptionName = subInfo.SubscriptionName,
                PurchaseDate = subInfo.PurchaseDate,
                ExpiredDate = subInfo.ExpiredDate,
                ClientId = subInfo.ClientId
            };

            await _subscriptionInfoRepository.CreateSubscriptionAsync(subscriptionInfo);
        }
    }
}