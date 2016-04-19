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
        public String Ability1 { get; set; }
        public String Ability2 { get; set; }

        public Pokemon(JObject o)
        {
            Id = (int) o["id"];
            Name = (string) o["name"];
            JArray types = (JArray) o["types"];
            //Console.WriteLine(types);

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

            JArray abilities = (JArray)o["abilities"];
            //Console.WriteLine(types);

            i = 1;
            foreach (var item in abilities.Children())
            {
                switch (i)
                {
                    case 1:
                        Ability1 = (string)item["ability"]["name"];
                        break;
                    case 2:
                        Ability2 = (string)item["ability"]["name"];
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
