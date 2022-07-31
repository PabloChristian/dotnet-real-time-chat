using System.Net.Http.Headers;

namespace Real.Time.Chat.Bot
{
    public class BotCall : IDisposable
    {
        private const string PREFIX = "https://stooq.com/q/l/?s=";
        private const string URL = ".us&f=sd2t2ohlcv&h&e=csv";
        private string quote = string.Empty;

        public string CallServiceStock(string keyWord)
        {
            var url = $"{PREFIX}{keyWord}{URL}";
            quote = BotHelper.GetStockInformation(PREFIX,url).Split(',')[13];
            return VerifyResponse() ? $"{keyWord} quote is ${quote} per share" : $"Stock code \"{keyWord}\" not found";
        }

        public bool VerifyResponse() => !quote.Contains("N/D");

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
