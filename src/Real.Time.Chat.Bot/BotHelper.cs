using System.Net.Http.Headers;

namespace Real.Time.Chat.Bot
{
    public static class BotHelper
    {
        public static string GetStockInformation(string prefix, string url)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(prefix);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result = Task.Run(async () => await client.GetAsync(url)).Result;
            return Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
        }

        public static bool IsStockCall(string receivedMessage) => string.Compare(receivedMessage, 0, "/stock=", 0, 7) == 0;
    }
}
