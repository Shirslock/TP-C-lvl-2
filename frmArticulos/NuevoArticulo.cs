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
using System.IO;
using System.Configuration;

namespace frmArticulos
{
    public partial class frmNuevoArticulo : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;


        public frmNuevoArticulo()
        {
            InitializeComponent();
        }

        public frmNuevoArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Pokemon";

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {   
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtbCodigoArticulo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Mark)cbxMarca.SelectedItem;
                articulo.Categoria = (Category)cbxCategoria.SelectedItem;
                articulo.ImagenUrl = txtbImagenUrl.Text;
                articulo.Precio = decimal.Parse(txtbPrecio.Text);

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Articulo modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Articulo agregado exitosamente");
                }
                if (archivo != null && !(txtbImagenUrl.Text.ToUpper().Contains("HTTP:"))) ;
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["carpeta-imagenes"] + archivo.SafeFileName);
                }
                        
                Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void fmwNuevoArticulo_Load(object sender, EventArgs e) //DESPLEGABLES//
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                
                cbxCategoria.DataSource = categoriaNegocio.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";
                cbxMarca.DataSource = marcaNegocio.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";


                if (articulo != null)
                {
                    txtbCodigoArticulo.Text = articulo.Codigo.ToString();
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                    txtbImagenUrl.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);
                    txtbPrecio.Text = articulo.Precio.ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtbImagenUrl_Leave(object sender, EventArgs e) //IMAGEN CUANDO INSERTO//
        {
            cargarImagen(txtbImagenUrl.Text);
            
        }

        private void cargarImagen(string imagen) //NECESARIO PARA PREVISUALIZAR IMAGEN//
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulos.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnSubirImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg; | jpeg|*.jpeg; | png|*.png";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtbImagenUrl.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }

        }
    }

}
