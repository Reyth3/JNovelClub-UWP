using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Models
{

    public class ErrorResponse : IResponseModel
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public string name { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public string code { get; set; }
    }
}
