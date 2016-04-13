using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace PokeC.Pokemon
{
    public class PokemonService
    {
        public Dictionary<int, Pokemon> pokemons = new Dictionary<int, Pokemon>();

        public PokemonService()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pokeapi.co/api/v2/pokemon/?limit=999");
                JObject o = JObject.Parse(json);

                JArray array = (JArray) o["results"];
                foreach (var item in array.Children())
                {
                    new Thread(() =>
                    {
                        using (WebClient wc2 = new WebClient())
                        {
                            string pkmn = wc2.DownloadString((string)item["url"]);
                            Console.WriteLine("1");
                        }
                    }).Start();
                }
            }
        }
    }
}
