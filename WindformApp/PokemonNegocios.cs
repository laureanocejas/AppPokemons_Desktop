using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindformApp
{
    internal class PokemonNegocios
    {
        public List<Pokemons>Listar()
        {
            List<Pokemons>lista=new List<Pokemons>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=DESKTOP-PHQLRTP;database=POKEDEX_DB;integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Numero,Nombre,Descripcion From POKEMONS";
                comando.Connection = conexion;

                conexion.Open();
                lector= comando.ExecuteReader();
                
                while(lector.Read())
                {
                    Pokemons aux = new Pokemons();
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    lista.Add(aux);

                }
                conexion.Close();
                return lista;

            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}
