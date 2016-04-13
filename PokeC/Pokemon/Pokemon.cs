using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PokeC.Pokemon
{
    public class Pokemon
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Type1 { get; set; }
        public String Type2 { get; set; }

        public Pokemon(JObject o)
        {
            Id = Convert.ToInt32(o["id"]);
            Name = (string) o["name"];
            Console.WriteLine(Id);
            JArray types = (JArray) o["types"];

            int i = 1;
            foreach (var item in types.Children())
            {
                switch (i)
                {
                    case 1:
                        Type1 = (string) item["type"]["name"];
                        break;
                    case 2:
                        Type2 = (string) item["type"]["name"];
                        break;
                }

                i++;

                if (i > 2)
                {
                    break;
                }
            }
        }
    }
}
