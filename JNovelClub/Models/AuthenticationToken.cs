using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Models
{

    public class AuthenticationToken
    {
        public string Id { get; set; }
        public int Ttl { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
    }
}
