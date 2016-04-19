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
            new Thread(() =>
                   {
                       using (WebClient wc = new WebClient())
                       {
                           var json = wc.DownloadString("http://pokeapi.co/api/v2/pokemon/?limit=60");
                           JObject o = JObject.Parse(json);

                           JArray array = (JArray)o["results"];
                           foreach (var item in array.Children())
                           {
                               string url = (string)item["url"];

                               new Thread(() =>
                               {
                                   using (WebClient wcin = new WebClient())
                                   {
                                       Console.WriteLine("Started parsing " + url);
                                       try
                                       {
                                           var pkmn = wcin.DownloadString(url);
                                           JObject poke = JObject.Parse(pkmn);
                                           Pokemon obj = new Pokemon(poke);

                                           pokemons.Add(obj.Id, obj);
                                           Console.WriteLine("Finished parsing " + url);
                                       }
                                       catch (WebException e) {
                                           Console.WriteLine("Failed parsing " + url);
                                       }
                                   }
                               }).Start();
                           }
                       }
                   }
           ).Start();
        }

        public Pokemon[] getSearchResults(string query) 
        { 
            Pokemon[] results = new Pokemon[151];

            int i = 0;
            foreach (KeyValuePair<int, Pokemon> entry in pokemons) 
            {
                Pokemon p = entry.Value;

                if (p.Name.ToLower().Contains(query.ToLower()))
                {
                    results[i++] = p;
                }
            }

            return results;
        }
    }
}
