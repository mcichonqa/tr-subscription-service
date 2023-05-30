using SubscriptionService.Contract;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Services
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> GetSubscription(int clientId);
    }
}
