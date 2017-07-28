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
    public class SeriesAPI
    {
        public SeriesAPI(JNClient client)
        {
            _client = client;
        }

        private JNClient _client;

        public async Task<Response<Series[]>> GetListOfSeries(Dictionary<string, object> filter = null, string[] include = null)
        {
            include = include ?? new string[] { "volumes" };
            var f = RequestHelper.GenerateFiltersString(filter, include);
            using (var http = _client._apiClient)
            using (var res = await http.GetAsync("api/series?filter=" + WebUtility.UrlEncode(f)))
            {
                return new Response<Series[]>(await res.Content.ReadAsStringAsync());
            }
        }

        public async Task<Response<Series>> GetOne(Dictionary<string, object> filter = null, string[] include = null)
        {
            include = include ?? new string[] { "volumes" };
            var f = RequestHelper.GenerateFiltersString(filter, include);
            using (var http = _client._apiClient)
            using (var res = await http.GetAsync("api/series/findOne?filter=" + WebUtility.UrlEncode(f)))
            {
                return new Response<Series>(await res.Content.ReadAsStringAsync());
            }
        }
    }
}
