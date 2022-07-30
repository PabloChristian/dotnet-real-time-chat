using Real.Time.Chat.Shared.Kernel.Entity;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Time.Chat.MessageHandler.Contracts.Implementations
{
    public class DeliveryMessageRequest : IDeliveryMessageRequest
    {
        private readonly ILogger<DeliveryMessageRequest> _logger;
        private readonly IChatService _financialChatService;

        public DeliveryMessageRequest(ILogger<DeliveryMessageRequest> logger, IChatService financialChatService)
        {
            _logger = logger;
            _financialChatService = financialChatService;
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
                .ExecuteAsync(() => _financialChatService.CreateApi().DeliveryMessage(message));
    }
}
