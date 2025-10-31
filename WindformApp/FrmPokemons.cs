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
            cbxCampo.Items.Add("Numero");
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Descripcion");


        }
        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvPokemons.CurrentRow != null)
            {
                Pokemons seleccionado = (Pokemons)dgvPokemons.CurrentRow.DataBoundItem;
                //pbxPokemon.Load(seleccionado.UrlImagen); esto me tira una excepcion si la columna esta vacia de url y no tiene imagen
                cargarImagen(seleccionado.UrlImagen);
            }
            

        }

        private void cargar()
        {
            PokemonNegocios negocio = new PokemonNegocios();
            // dgvPokemons.DataSource = negocio.Listar();


            try
            {
                listaPokemons = negocio.Listar();
                dgvPokemons.DataSource = listaPokemons;
                ocultarColumnas();
                cargarImagen(listaPokemons[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        private void ocultarColumnas()
        {
            dgvPokemons.Columns["UrlImagen"].Visible = false;
            dgvPokemons.Columns["Id"].Visible = false;
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
            eliminar();
           
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void eliminar(bool logico=false)
        {
            PokemonNegocios negocio = new PokemonNegocios();
            Pokemons seleccionado;

            try
            {
                DialogResult respuesta = MessageBox.Show("de verdad desea eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Pokemons)dgvPokemons.CurrentRow.DataBoundItem;
                    if (logico)
                        negocio.eliminarLogico(seleccionado.Id);
                    //sino elimina fisico
                    else
                        negocio.eliminar(seleccionado.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            PokemonNegocios negocio = new PokemonNegocios();
            try
            {
                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCirterio.SelectedItem.ToString();
                string filtro = cbxFiltroAvanzado.Text;
                dgvPokemons.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
            //List<Pokemons> listaFiltrada;//creo una lista sin instancia 
            //string filtro = txtFiltro.Text;

            //if(filtro!="")
            //{
            //    //busca por igualdad ==
            //   // listaFiltrada = listaPokemons.FindAll(x => x.Nombre.ToUpper() == filtro.ToUpper());//porque esto devuelve una lista
            //    //busca por concidencia contains
            //    listaFiltrada = listaPokemons.FindAll(x => x.Nombre.ToUpper().Contains (filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()));//porque esto devuelve una lista

            //}
            //else
            //{
            //    listaFiltrada = listaPokemons;
            //}

            //dgvPokemons.DataSource = null;//limpio
            //dgvPokemons.DataSource = listaFiltrada;//actualizo el dgv
            //ocultarColumnas();

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Pokemons> listaFiltrada;//creo una lista sin instancia 
            string filtro = txtFiltro.Text;

            if (filtro.Length<3)
            {
                //busca por igualdad ==
                // listaFiltrada = listaPokemons.FindAll(x => x.Nombre.ToUpper() == filtro.ToUpper());//porque esto devuelve una lista
                //busca por concidencia contains
                listaFiltrada = listaPokemons.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()));//porque esto devuelve una lista

            }
            else
            {
                listaFiltrada = listaPokemons;
            }

            dgvPokemons.DataSource = null;//limpio
            dgvPokemons.DataSource = listaFiltrada;//actualizo el dgv
            ocultarColumnas();

        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbxCampo.SelectedItem.ToString();
            if(opcion=="Numero")
            {
                cbxCirterio.Items.Clear();
                cbxCirterio.Items.Add("Mayor a");
                cbxCirterio.Items.Add("Menor a");
                cbxCirterio.Items.Add("Igual a");
            }
            else
            {
                cbxCirterio.Items.Clear();
                cbxCirterio.Items.Add("comienza con ");
                cbxCirterio.Items.Add("termina con ");
                cbxCirterio.Items.Add("contiene ");
            }
        }
    }
}
