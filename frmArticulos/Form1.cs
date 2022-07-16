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
    public partial class fmwArticulos : Form
    {
        private List<Articulo> listaArticulo;

        public fmwArticulos()
        {
            InitializeComponent();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);
        }

        private void fmwArticulos_Load(object sender, EventArgs e)
        {
            cargar();
   
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
                dgvArticulos.Columns["ImagenUrl"].Visible = false;
                dgvArticulos.Columns["Id"].Visible = false;   
                cargarImagen(listaArticulo[0].ImagenUrl);

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
                pbxArticulos.Load(imagen);

            }
            catch (Exception ex)
            {

                pbxArticulos.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmNuevoArticulo alta = new frmNuevoArticulo();
            alta.ShowDialog();
            cargar(); //METODO PARA QUE SE AGREGUE Y REFRESHEE
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmNuevoArticulo modificar = new frmNuevoArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            
        }

        //METODOS//
        private void Eliminar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Desea eliminar el articulo?", "Eliminando Articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminacionFisica(seleccionado.Id);
                    MessageBox.Show("Articulo Eliminado");
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
