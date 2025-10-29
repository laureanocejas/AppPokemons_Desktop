using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{ 
public class PokemonNegocios
{
    public List<Pokemons> Listar()
    {
        List<Pokemons> lista = new List<Pokemons>();
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;

        try
        {
            conexion.ConnectionString = "server=DESKTOP-PHQLRTP;database=POKEDEX_DB;integrated security=true";
            comando.CommandType = System.Data.CommandType.Text;
            //comando.CommandText = "Select Numero,Nombre,Descripcion From POKEMONS";
            //comando.CommandText= "Select Numero,Nombre,Descripcion,UrlImagen From POKEMONS";
            //comando.CommandText = "Select P.Numero,P.Nombre,P.Descripcion,P.UrlImagen, E.Descripcion as Tipo From POKEMONS P,ELEMENTOS E where P.IdTipo=E.Id;\r\n";
            comando.CommandText = "Select P.Numero,P.Nombre,P.Descripcion,P.UrlImagen, E.Descripcion as Tipo,D.Descripcion as Debilidad, P.IdTipo, P.IdDebilidad, P.Id From POKEMONS P, ELEMENTOS E,ELEMENTOS D where P.IdTipo = E.Id And P.IdDebilidad = D.Id";

            comando.Connection = conexion;

            conexion.Open();
            lector = comando.ExecuteReader();

            while (lector.Read())
            {
                    Pokemons aux = new Pokemons();
                    aux.Id =(int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    lista.Add(aux);

            }
            conexion.Close();
            return lista;

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
        public void agregar(Pokemons nuevo)
        {
            AccesoDatos datos=new AccesoDatos();
            try
            {
                //datos.setearConsulta("Insert into POKEMONS(Numero,Nombre,Descripcion,Activo,IdTipo,IdDebilidad)values("+nuevo.Numero +", '"+nuevo.Nombre+"','"+nuevo.Descripcion+"',1,@idTipo,@idDebilidad)");
                datos.setearConsulta("Insert into POKEMONS(Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad, UrlImagen)values("+nuevo.Numero +", '"+nuevo.Nombre+"', '"+nuevo.Descripcion+"', 1, @idTipo, @idDebilidad,@urlImagen)");
                datos.setearParametro("@idTipo",nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.setearParametro("@urlImagen", nuevo.UrlImagen);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void modificar(Pokemons poke)
        {
            AccesoDatos datos=new AccesoDatos();
            try
            {
                datos.setearConsulta("update POKEMONS set Numero=@numero,Nombre=@nombre,Descripcion=@descripcion,UrlImagen=@img,IdTipo=@idTipo,IdDebilidad=@idDebilidad Where Id=@id;\r\n");
                datos.setearParametro("@numero",poke.Numero);
                datos.setearParametro("@nombre",poke.Nombre);
                datos.setearParametro("@descripcion",poke.Descripcion);
                datos.setearParametro("@img",poke.UrlImagen);
                datos.setearParametro("@idTipo",poke.Tipo.Id);
                datos.setearParametro("@idDebilidad",poke.Debilidad.Id);
                datos.setearParametro("@id",poke.Id);

                datos.ejecutarAccion();



            }
            catch (Exception ex)
            {

                throw ex; 
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

    }
}
