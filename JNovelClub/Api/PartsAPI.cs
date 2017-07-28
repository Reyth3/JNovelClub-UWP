using JNovelClub;
using JNovelClub.Helpers;
using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Api
{
    public class PartsAPI
    {
        public PartsAPI(JNClient client)
        {
            _client = client;
        }

        private JNClient _client;

        public async Task<Response<Part[]>> GetList(Dictionary<string, object> filter = null, params string[] include)
        {
            include = include ?? new string[] { "volumes" };
            var f = RequestHelper.GenerateFiltersString(filter, include);
            using (var http = _client._apiClient)
            using (var res = await http.GetAsync("api/parts?filter=" + WebUtility.UrlEncode(f)))
            {
                return new Response<Part[]>(await res.Content.ReadAsStringAsync());
            }
        }

        public async Task<Response<Part>> GetOne(Dictionary<string, object> filter = null, params string[] include)
        {
            include = include ?? new string[] { "volumes" };
            var f = RequestHelper.GenerateFiltersString(filter, include);
            using (var http = _client._apiClient)
            using (var res = await http.GetAsync("api/parts/getOne?filter=" + WebUtility.UrlEncode(f)))
            {
                return new Response<Part>(await res.Content.ReadAsStringAsync());
            }
        }

        public async Task<Response<PartData>> GetPartData(string partId)
        {
            using (var http = _client._apiClient)
            using (var res = await http.GetAsync($"api/parts/{partId}/partData"))
            {
                return new Response<PartData>(await res.Content.ReadAsStringAsync());
            }
        }
    }
}
