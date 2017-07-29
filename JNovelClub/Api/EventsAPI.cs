using JNovelClub.Helpers;
using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Api
{
    class EventsAPI
    {
        public EventsAPI(JNClient client)
        {
            _client = client;
        }

        private JNClient _client;

        public async Task<Response<Event[]>> GetListOfEvents()
        {
            using (var http = _client._apiClient)
            using (var res = await http.GetAsync("api/events"))
            {
                return new Response<Event[]>(await res.Content.ReadAsStringAsync());
            }
        }
    }
}
