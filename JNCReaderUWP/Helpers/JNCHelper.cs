using JNovelClub;
using JNovelClub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace JNCReaderUWP.Helpers
{
    public class JNCHelper
    {
        public static JNClient InstantiateClient()
        {
            var key = "userAuth";
            var roaming = ApplicationData.Current.RoamingSettings.Values;
            JNClient jnc = null;
            if (roaming.ContainsKey(key))
                jnc = new JNClient(JsonConvert.DeserializeObject<AuthenticationToken>(roaming[key].ToString()));
            else jnc = new JNClient();
            return jnc;
        }
    }
}
