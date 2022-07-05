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
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return lista;
        } 
        
    }
}
