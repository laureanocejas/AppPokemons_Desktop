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
        private Pokemons pokemon=null;
        public frmAltaPokemons()
        {
            InitializeComponent();
        }
        public frmAltaPokemons(Pokemons pokemon)
        {
            InitializeComponent();
            this.pokemon= pokemon;
            Text = "Modificiar Pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //poke esta variable ya no me sirve mas porque ya tengo la variable pokemon
            //Pokemons poke = new Pokemons();
            PokemonNegocios negocio = new PokemonNegocios();

            try
            {
                //uso el atributo privado pokemon 
                //si hago aceptar y el pokemon esta null yo quiero agregar un pokemon nuevo
                //si no esta null quiere decir que hay un pokemons
                if (pokemon == null)
                    pokemon = new Pokemons();

                pokemon.Numero =int.Parse(txtNumero.Text);
                pokemon.Nombre =txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                pokemon.Tipo = (Elemento)cbxTipo.SelectedItem;
                pokemon.Debilidad =(Elemento) cbxDebilidad.SelectedItem;

                //como me doy cuenta si quiero ejecutar agregar o modificar
                if (pokemon.Id != 0)
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("modificado existosamente");
                }
                else
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("agregado exitosamente");
                } 
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
                cbxTipo.ValueMember = "Id";
                cbxTipo.DisplayMember = "Descripcion";
                cbxDebilidad.DataSource = elementoNegocio.listar();
                cbxDebilidad.ValueMember = "Id";
                cbxDebilidad.DisplayMember = "Descripcion";

                //validacion para saber si es un pokemon para modificar
                if(pokemon !=null)
                {
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cbxTipo.SelectedValue = pokemon.Tipo.Id;
                    cbxDebilidad.SelectedValue = pokemon.Debilidad.Id;

                }
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
