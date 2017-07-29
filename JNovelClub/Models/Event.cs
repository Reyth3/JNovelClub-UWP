using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Models
{

    public class Event : IResponseModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string LinkFragment { get; set; }
        public DateTime Date { get; set; }
        public string Id { get; set; }
        public Attachment[] Attachments { get; set; }
    }
}
