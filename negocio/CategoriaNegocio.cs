using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Category> listar()
        {
            List<Category> lista = new List<Category>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select Id , Descripcion from CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Category aux = new Category();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

             
                    lista.Add(aux);
              
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }



            
        } 
        
    }
}
