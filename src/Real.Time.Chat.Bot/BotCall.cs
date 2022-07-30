using System.Net.Http.Headers;

namespace Real.Time.Chat.Bot
{
    public class BotCall
    {
        private const string PREFIX = "https://stooq.com/q/l/?s=";
        private const string URL = ".us&f=sd2t2ohlcv&h&e=csv";
        private string quote = string.Empty;

        public string CallServiceStock(string keyWord)
        {
            var url = $"{PREFIX}{keyWord}{URL}";
            quote = GetStockInformation(url).Split(',')[13];
            return VerifyResponse() ? $"{keyWord} quote is ${quote} per share" : $"Stock code \"{keyWord}\" not found";
        }

        public bool VerifyResponse() => !quote.Contains("N/D");

        private static string GetStockInformation(string URI)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(PREFIX);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result = Task.Run(async () => await client.GetAsync(URI)).Result;
            return Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
        }

        public bool IsStockCall(string receivedMessage) => string.Compare(receivedMessage, 0, "/stock=", 0, 7) == 0;
    }
}
