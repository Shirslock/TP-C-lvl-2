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

namespace frmArticulos
{
    public partial class fmwNuevoArticulo : Form
    {
        public fmwNuevoArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        { 
            Articulo article = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                article.Codigo = txtbCodigoArticulo.Text;
                article.Nombre = txtNombre.Text;
                article.Descripcion = txtDescripcion.Text;
                article.ImagenUrl = txtbImagenUrl.Text;

                negocio.agregar(article);
                MessageBox.Show("Articulo agregado exitosamente");
                Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
