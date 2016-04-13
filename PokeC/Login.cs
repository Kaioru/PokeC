using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using DevOne.Security.Cryptography.BCrypt;

namespace PokeC
{
    public partial class Login : MaterialForm
    {

        public AccountService DefaultAccountService { get; set; }
        public Pokemon.PokemonService DefaultPokemonService { get; set; }

        public Login()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            AcceptButton = btnLogin;

            DefaultPokemonService = new Pokemon.PokemonService();
            string path = Path.Combine(Application.UserAppDataPath, "\\accounts.json");
            try
            {
                string json = System.IO.File.ReadAllText(path);
                DefaultAccountService = JsonConvert.DeserializeObject<AccountService>(json);
            }
            catch (Exception e)
            {
                DefaultAccountService = new AccountService();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtLogin.Text;
            String password = txtPassword.Text;

            txtLogin.Enabled = false;
            txtPassword.Enabled = false;

            if (!String.IsNullOrWhiteSpace(username) || !String.IsNullOrWhiteSpace(password))
            {
                AccountService.LoginReply reply = DefaultAccountService.tryLogin(username, password);

                if (reply == AccountService.LoginReply.OK)
                {
                    Account account = DefaultAccountService.getAccount(username);
                    Dex dex = new Dex(account);

                    dex.Show();
                    this.Hide();
                    return;
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password");
                }
            }
            else
                MessageBox.Show("Invalid inputs!");

            txtLogin.Enabled = true;
            txtPassword.Enabled = true;
            txtPassword.Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            String username = txtLogin.Text;
            String password = txtPassword.Text;

            txtLogin.Enabled = false;
            txtPassword.Enabled = false;

            if (!String.IsNullOrWhiteSpace(username) || !String.IsNullOrWhiteSpace(password))
            {
                String hashed = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
                Account account = new Account(username, hashed);

                if (!DefaultAccountService.createAccount(username, account))
                {
                    MessageBox.Show("Register failed, username taken.");
                }
                else
                    MessageBox.Show("Register succeeded.");
            }
            else
                MessageBox.Show("Invalid inputs!");

            txtLogin.Enabled = true;
            txtPassword.Enabled = true;
            txtPassword.Clear();
        }

    }
}
