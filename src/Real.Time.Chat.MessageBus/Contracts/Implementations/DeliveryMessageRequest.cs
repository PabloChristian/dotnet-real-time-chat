using Real.Time.Chat.Shared.Kernel.Entity;
using Polly;

namespace Real.Time.Chat.MessageBus.Contracts.Implementations
{
    public class DeliveryMessageRequest : IDeliveryMessageRequest
    {
        private readonly ILogger<DeliveryMessageRequest> _logger;
        private readonly IChatService _realtimeChatService;

        public DeliveryMessageRequest(ILogger<DeliveryMessageRequest> logger, IChatService realtimeChatService)
        {
            _logger = logger;
            _realtimeChatService = realtimeChatService;
        }

        public async Task<ApiOkReturn> DeliveryMessageAsync(MessageDto message) =>
            await Policy.Handle<Exception>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(2),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(exception.Message + " - retrycount: " + retryCount);
                    })
                .ExecuteAsync(() => _realtimeChatService.CreateApi().DeliveryMessage(message));
    }
}
