using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Web.ViewModel;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Real.Time.Chat.Web.Data
{
    public class ChatService
    {
#if !DEBUG
        private const string URL = "http://realtime-chat.api:5001/";
#else 
        private const string URL = "https://localhost:44367/";
#endif
        public string GetURL() => URL;
        public async Task<List<UserDto>> GetUser(string token)
        {
            HttpClientHandler clientHandler = new();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            List<UserDto> users = new();
            using (HttpClient client = new(clientHandler))
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.GetAsync("api/user");
                try
                {
                    var actionResult = JsonConvert.DeserializeObject<ApiOkReturn>(await response.Content.ReadAsStringAsync());
                    users = JsonConvert.DeserializeObject<List<UserDto>>(JsonConvert.SerializeObject(actionResult.Data));
                }
                catch
                {

                }

                return users;
            }
        }

        public async Task<HttpResponseMessage> PostNewUser(UserViewModel model)
        {
            HttpClientHandler clientHandler = new ()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            using HttpClient client = new(clientHandler);
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();

            var content = new StringContent(content: JsonConvert.SerializeObject(model), encoding: System.Text.Encoding.UTF8, mediaType: "application/json");

            return await client.PostAsync("api/user/signin", content);
        }

        public async Task<HttpResponseMessage> Login(string email, string password)
        {
            HttpClientHandler clientHandler = new ();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using HttpClient client = new (clientHandler);
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            var content = new StringContent(content: JsonConvert.SerializeObject(new { email, password }), encoding: System.Text.Encoding.UTF8, mediaType: "application/json");

            return await client.PostAsync("api/login", content);
        }

        public async Task<List<MessageDto>> GetUseMessages(string token, string email)
        {
            HttpClientHandler clientHandler = new();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new(clientHandler))
            {
                List<MessageDto> messages = new();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.GetAsync("api/user/messages/" + email);
                try
                {
                    var actionResult = JsonConvert.DeserializeObject<ApiOkReturn>(await response.Content.ReadAsStringAsync());
                    messages = JsonConvert.DeserializeObject<List<MessageDto>>(JsonConvert.SerializeObject(actionResult.Data));
                }
                catch 
                {

                }

                return messages;
            }
        }
        public async Task SendMessage(string token, string sender, string consumer, string message)
        {
            HttpClientHandler clientHandler = new();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new(clientHandler))
            {
                List<UserDto> users = new();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                var content = new StringContent(content: JsonConvert.SerializeObject(new { sender, consumer, message }), encoding: System.Text.Encoding.UTF8, mediaType: "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                await client.PostAsync("api/user/send", content);
            }
        }
    }
}
