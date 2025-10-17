using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindformApp
{
    public partial class FrmPokemons : Form
    {
        public FrmPokemons()
        {
            InitializeComponent();
        }

        private void FrmPokemons_Load(object sender, EventArgs e)
        {
            PokemonNegocios negocio=new PokemonNegocios();
            dgvPokemons.DataSource = negocio.Listar();
        }
    }
}
