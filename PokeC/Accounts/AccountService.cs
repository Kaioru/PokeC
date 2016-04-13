using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using DevOne.Security.Cryptography.BCrypt;

namespace PokeC
{
    public class AccountService
    {

        public enum LoginReply
        {
            OK,
            Non_Existing,
            Invalid_Password
        }

        public Dictionary<string, Account> accounts = new Dictionary<string, Account>();

        public AccountService()
        {
        }

        public bool createAccount(String username, Account account)
        {
            if (accounts.ContainsKey(username))
                return false;
            accounts.Add(username, account);

            try
            {
                string json = JsonConvert.SerializeObject(this);
                string path = Path.Combine(Application.UserAppDataPath, "\\accounts.json");
                System.IO.File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                MessageBox.Show("Program not run in administrator mode, accounts not saved.");
            }

            return true;
        }

        public bool deleteAccount(String username) // Should never be used
        {
            return accounts.Remove(username);
        }

        public Account getAccount(String username)
        {
            return accounts[username];
        }

        public LoginReply tryLogin(String username, String password)
        {
            if (accounts.ContainsKey(username))
            {
                Account target = accounts[username];

                if (BCryptHelper.CheckPassword(password, target.HashedPassword))
                {
                    return LoginReply.OK;
                }
                else
                    return LoginReply.Invalid_Password;
            }
            else
                return LoginReply.Non_Existing;
        }

    }
}
