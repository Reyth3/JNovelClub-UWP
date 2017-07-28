using JNovelClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNConsole
{
    class Program
    {
        static void Main(string[] args) => Start().Wait();
        static string e = "mrreynevan2@gmail.com", p = "xardas123";
        static async Task Start()
        {
            var jnc = new JNClient();
            var auth = await jnc.Authenticate(e, p);
            if(!auth)
            {
                Console.WriteLine("Auth failed!");
                Console.ReadKey();
                return;
            }
            var res = await jnc.Parts.GetList();
            var firstPart = res.Result.FirstOrDefault();
            if(firstPart != null)
            {
                var data = await jnc.Parts.GetPartData(firstPart.Id);
            }
            Console.ReadKey();
        }
    }
}
