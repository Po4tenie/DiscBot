using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscBot.Handlers
{
    public class CatHandler
    {
        public string? url { get; set; }

        public static string GetCat()
        {



            string cat = "";


            string jsonUrl = $"  ";
            WebClient webClient = new WebClient();

            jsonUrl = webClient.DownloadString(jsonUrl);
            var JsonSerialize = JsonSerializer.Deserialize<List<CatHandler>>(jsonUrl);

            foreach (var item in JsonSerialize)
            {
                cat = item.url;
            }

            return cat;


        }
    } 
}
