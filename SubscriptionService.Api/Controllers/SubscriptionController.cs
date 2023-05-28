using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Api.Model;
using SubscriptionService.Contract;
using SubscriptionService.Entity.Models;
using SubscriptionService.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetSubscription(int clientId)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionAsync(clientId);

            var lastActiveSubscription = subscription.SubscriptionDetails.Any()
                ? subscription.SubscriptionDetails.OrderByDescending(x => x.ExpiredDate).First()
                : null;

            var subscriptionDto = new SubscriptionDto()
            {
                Id = subscription.Id,
                SubscriptionStatus = subscription.SubscriptionStatus,
                ClientId = clientId
            };

            if (lastActiveSubscription != null)
                subscriptionDto.ActiveSubscription = new SubscriptionDetailsDto()
                {
                    Id = lastActiveSubscription.Id,
                    SubscriptionName = lastActiveSubscription.SubscriptionName,
                    ExpiredDate = lastActiveSubscription.ExpiredDate,
                    PurchaseDate = lastActiveSubscription.PurchaseDate,
                    SubscriptionId = lastActiveSubscription.SubscriptionId
                };

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