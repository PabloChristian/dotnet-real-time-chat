using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.MessageBus.Contracts;
using MassTransit;

namespace Real.Time.Chat.MessageBus.Consumers
{
    public class CaptureMessageConsumer : IConsumer<MessageDto>
    {
        private readonly ILogger<CaptureMessageFaultConsumer> _logger;
        private readonly IDeliveryMessageRequest _deliveryMessageRequest;

        public CaptureMessageConsumer(ILogger<CaptureMessageFaultConsumer> logger, IDeliveryMessageRequest deliveryMessageRequest)
        {
            _logger = logger;
            _deliveryMessageRequest = deliveryMessageRequest;
        }

        public async Task Consume(ConsumeContext<MessageDto> context)
        {
            _logger.LogInformation($"Message received - Consumer: {context.Message.Consumer}");

            var message = context.Message;

            try
            {
                var response = await _deliveryMessageRequest.DeliveryMessageAsync(message);
                if (response == null)
                    throw new Exception("The service was not able to delivery the message, trying again in few minutes");

                await context.RespondAsync(response);
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to deliver the message to consumer: {context.Message.Consumer} - Message: {e.Message}");

                await context.RespondAsync(e);

                throw;
            }
        }
    }
}
