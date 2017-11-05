using JNovelClub.Api;
using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub
{
    public class JNClient
    {
        public JNClient()
        { }

        public JNClient(AuthenticationToken auth)
        {
            Token = auth;
        }

        private AuthAPI authAPI;
        private SeriesAPI seriesAPI;
        private PartsAPI partsAPI;
        private EventsAPI eventsAPI;
        private PostsAPI postsAPI;

        internal HttpClient _apiClient
        { get
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip };
                var http = new HttpClient(handler) { BaseAddress = new Uri("https://api.j-novel.club/") };
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.TryParseAdd("*/*");
                http.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                http.DefaultRequestHeaders.AcceptEncoding.TryParseAdd("gzip, deflate, sdch, br");
                http.DefaultRequestHeaders.Host = "api.j-novel.club";
                if(Token != null)
                    http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(Token.Id);
                return http;
            }
        }

        public AuthenticationToken Token { get; private set; }

        public AuthAPI Auth => authAPI ?? (authAPI = new AuthAPI(this));
        public SeriesAPI Series => seriesAPI ?? (seriesAPI = new SeriesAPI(this));
        public PartsAPI Parts => partsAPI ?? (partsAPI = new PartsAPI(this));
        public EventsAPI Events => eventsAPI ?? (eventsAPI = new EventsAPI(this));
        public PostsAPI Posts => postsAPI ?? (postsAPI = new PostsAPI(this));

        #region Authentication Shorthand
        public async Task<bool> Authenticate(string email, string password)
        {
            var response = await Auth.Authenticate(email, password);
            if (response.Success)
            {
                Token = response.Result;
                return true;
            }
            else return false;
        }
        public bool Authenticate(AuthenticationToken token)
        {
            if (token == null)
                return false;
            if (token.Id == null)
                return false;
            if (token.UserId == null)
                return false;
            Token = token;
            return true;
        }
        #endregion
    }
}
