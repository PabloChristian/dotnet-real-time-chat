﻿using Microsoft.IdentityModel.Tokens;
using Refit;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Real.Time.Chat.MessageBus.Contracts.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public ChatService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public IChatApi CreateApi()
        {
            var token = GetToken();
            var httpClient = CreateHttpClientForApi();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return RestService.For<IChatApi>(httpClient);
        }

        private string GetToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }

        public HttpClient CreateHttpClientForApi() =>
            _httpClientFactory.CreateClient("realtimechat");
    }
}
