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
    public partial class FrmPokemons : Form
    {
        private List<Pokemons> listaPokemons;
        public FrmPokemons()
        {
            InitializeComponent();
        }

        private void FrmPokemons_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemons seleccionado = (Pokemons)dgvPokemons.CurrentRow.DataBoundItem;
            //pbxPokemon.Load(seleccionado.UrlImagen); esto me tira una excepcion si la columna esta vacia de url y no tiene imagen
            cargarImagen(seleccionado.UrlImagen);

        }

        private void cargar()
        {
            PokemonNegocios negocio = new PokemonNegocios();
            // dgvPokemons.DataSource = negocio.Listar();


            try
            {
                listaPokemons = negocio.Listar();
                dgvPokemons.DataSource = listaPokemons;
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                dgvPokemons.Columns["Id"].Visible = false;

                cargarImagen(listaPokemons[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemons alta=new frmAltaPokemons();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemons seleccionado;
            seleccionado = (Pokemons)dgvPokemons.CurrentRow.DataBoundItem;
            
            frmAltaPokemons modificar = new frmAltaPokemons(seleccionado);
            modificar.ShowDialog();
            cargar();

        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            PokemonNegocios negocio=new PokemonNegocios();
            Pokemons seleccionado;

            try
            {
                DialogResult respuesta= MessageBox.Show("de verdad desea eliminarlo?","Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Pokemons)dgvPokemons.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); 
            }
        }
    }
}
