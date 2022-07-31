using Real.Time.Chat.Bot;
using Xunit;
using FluentAssertions;

namespace Real.Time.Chat.Tests.Bot
{
    public class BotTest
    {
        [Fact]
        public void Is_message_invalid_to_call_stock()
        {
            var result = new BotCall().IsStockCall("Hello world");
            result.Should().BeFalse();
        }

        [Fact]
        public void Is_message_valid_to_call_stock()
        {
            var result = new BotCall().IsStockCall("/stock=stock_code");
            result.Should().BeTrue();
        }
    }
}
