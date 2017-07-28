using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Api
{
    public class APIBase
    {
        public APIBase(JNClient client)
        {
            _client = client;
        }

        internal JNClient _client;
    }
}
