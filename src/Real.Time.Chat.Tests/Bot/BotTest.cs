using Real.Time.Chat.Bot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Real.Time.Chat.Tests.Bot
{
    [TestClass]
    public class BotTest
    {
        [TestMethod]
        public void Is_message_invalid_to_call_stock()
        {
            var result = new BotCall().IsStockCall("Hello world");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Is_message_valid_to_call_stock()
        {
            var result = new BotCall().IsStockCall("/stock=stock_code");
            Assert.IsTrue(result);
        }
    }
}
