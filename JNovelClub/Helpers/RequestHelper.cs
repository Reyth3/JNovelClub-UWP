using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Helpers
{
    public class RequestHelper
    {
        public static string GenerateFiltersString(Dictionary<string, object> filters, string[] include)
        {
            var json = new JObject();
            if (filters != null && filters.Count > 0)
            {
                var f = new JObject();
                var w = new JProperty("where", f);
                foreach (var set in filters)
                    f.Add(new JProperty(set.Key, set.Value));
                json.Add(w);
            }
            if (include != null && include.Length > 0)
                json.Add(new JProperty("include", include));
            return json.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
