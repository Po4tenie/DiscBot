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


            string jsonUrl = $"https://api.thecatapi.com/v1/images/search?api_key=eeeb7e8d-f2df-4e06-b2e1-0dc050ce2b5b";
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
