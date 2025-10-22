using dominio;
using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
                poke.UrlImagen = txtUrlImagen.Text;
                poke.Tipo = (Elemento)cbxTipo.SelectedItem;
                poke.Debilidad =(Elemento) cbxDebilidad.SelectedItem;

                negocio.agregar(poke);
                MessageBox.Show("Agregado existosamente");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemons_Load(object sender, EventArgs e)
        {
            
            ElementoNegocios elementoNegocio = new ElementoNegocios();
            try
            {
                cbxTipo.DataSource = elementoNegocio.listar();
                cbxDebilidad.DataSource = elementoNegocio.listar();
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxPokemon.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }




    }
}
