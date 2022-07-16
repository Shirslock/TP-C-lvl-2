using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Codigo, Nombre, A.Descripcion, C.Descripcion Categoria, M.Descripcion Marca, ImagenUrl, Precio, A.IdCategoria , A.IdMarca , A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M where M.Id = A.IdMarca and C.Id = A.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Mark();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Category();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        } 

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();  
            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo , Nombre , Descripcion ,IdMarca,IdCategoria, ImagenUrl , Precio) values (@codigo,@nombre,@descripcion,@idMarca,@idCategoria, @imagenurl , @precio)");
                datos.setearParametros("@codigo", nuevo.Codigo);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@idMarca", nuevo.Marca.Id);
                datos.setearParametros("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@imagenurl", nuevo.ImagenUrl);
                datos.setearParametros("@precio", nuevo.Precio);
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

        public void modificar(Articulo arti)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update ARTICULOS set Codigo = @codigo, Nombre = @nombre , Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio where Id = @id");
                datos.setearParametros("@codigo",arti.Codigo);
                datos.setearParametros("@nombre",arti.Nombre);
                datos.setearParametros("@descripcion",arti.Descripcion);
                datos.setearParametros("@idMarca", arti.Marca.Id);
                datos.setearParametros("@idCategoria", arti.Categoria.Id);
                datos.setearParametros("@imagenUrl", arti.ImagenUrl);
                datos.setearParametros("@precio", arti.Precio);
                datos.setearParametros("@id", arti.Id);

                datos.ejecutarAccion();


            }
            catch ( Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
           
        }

        public void eliminacionFisica(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from ARTICULOS where Id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
