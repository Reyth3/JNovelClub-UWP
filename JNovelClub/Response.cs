using JNovelClub.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub
{
    public class Response<T>
    {
        public Response() { }

        public Response(string json)
        {
            this.json = json;
            var token = JToken.Parse(json);

            if (token.SelectToken("error") != null)
            {
                Success = false;
                Error = token.ToObject<ErrorResponse>();
                return;
            }
            Success = true;
            Result = token.ToObject<T>();
        }

        public bool Success { get; }
        public T Result { get; }
        public ErrorResponse Error { get; }

        private string json;
    }
}
