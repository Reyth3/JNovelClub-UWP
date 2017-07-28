using JNovelClub;
using JNovelClub.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Api
{
    public class AuthAPI
    {
        public AuthAPI(JNClient client)
        {
            _client = client;
        }

        private JNClient _client;

        public async Task<Response<AuthenticationToken>> Authenticate(string email, string password)
        {
            var body = new JObject()
            {
                new JProperty("email", email),
                new JProperty("password", password)
            }.ToString();
            using (var http = _client._apiClient)
            using (var res = await http.PostAsync("api/users/login", new StringContent(body, Encoding.UTF8, "application/json")))
            {
                var json = await res.Content.ReadAsStringAsync();
                return new Response<AuthenticationToken>(await res.Content.ReadAsStringAsync());
            }

        }
    }
}
