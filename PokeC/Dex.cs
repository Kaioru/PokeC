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

        public Dex(Account account)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            Text = "PokeC - " + account.Username;
            ActiveControl = txtSearch; 
        }
    }
}
