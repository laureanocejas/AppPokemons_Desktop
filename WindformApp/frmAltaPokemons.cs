using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace WindformApp
{
    public partial class frmAltaPokemons : Form
    {
        public frmAltaPokemons()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Pokemons poke = new Pokemons();
            PokemonNegocios negocio = new PokemonNegocios();

            try
            {
                poke.Numero =int.Parse(txtNumero.Text);
                poke.Nombre =txtNombre.Text;
                poke.Descripcion = txtDescripcion.Text;

                negocio.agregar(poke);
                MessageBox.Show("Agregado existosamente");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
