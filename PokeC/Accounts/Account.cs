using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeC
{
    public class Account
    {

        public String Username { get; set; }
        public String HashedPassword { get; set; }

        public Account(String username ,String hashed)
        {
            Username = username;
            HashedPassword = hashed;
        }


    }
}
