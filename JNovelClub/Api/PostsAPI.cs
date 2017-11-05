using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Api
{
    public class PostsAPI
    {
        public PostsAPI(JNClient client)
        {
            _client = client;
        }

        private JNClient _client;

        public async Task<Response<Post[]>> GetList()
        {
            using (var http = _client._apiClient)
            using (var res = await http.GetAsync("api/posts"))
            {
                var response = new Response<Post[]>(await res.Content.ReadAsStringAsync());
                return response;
            }
        }
    }
}
