using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Models
{
    public class Post
    {
        public string title { get; set; }
        public string titleslug { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public string tags { get; set; }
        public string forumLink { get; set; }
        public string id { get; set; }
        public Attachment[] attachments { get; set; }
        public int postcount { get; set; }
    }
}
