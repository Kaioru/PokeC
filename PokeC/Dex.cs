using System;
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

namespace PokeC
{
    public partial class Dex : MaterialForm
    {
        public Account account { get; set; }
        public Pokemon.PokemonService DefaultPokemonService { get; set; }

        public Dex(Account account, Pokemon.PokemonService service)
        {
            InitializeComponent();
            DefaultPokemonService = service;

            StartPosition = FormStartPosition.CenterScreen;
            Text = "PokeC - " + account.Username;
            ActiveControl = txtSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text;
            Pokemon.Pokemon[] results = DefaultPokemonService.getSearchResults(query);

            dgvResults.Rows.Clear();

            foreach (Pokemon.Pokemon p in results) 
            {
                if (p != null)
                    dgvResults.Rows.Add(p.Id, p.Name, p.Ability1, p.Ability2, p.Type1, p.Type2);
            }

            dgvResults.Sort(ID, ListSortDirection.Ascending);
        }

        private void Dex_Load(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }
    }
}
