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


        //BUTTONS Y EVENTOS

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);

            }
            
        }

        private void fmwArticulos_Load(object sender, EventArgs e)
        {
            cargar();

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

        private void txtFiltrar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltrar.Text;

            if (filtro.Length > 3)
            {
                listaFiltrada = listaArticulo.FindAll(X => X.Nombre.ToUpper().Contains(filtro.ToUpper()) || X.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || X.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            }
            else
            {
                listaFiltrada = listaArticulo;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }



        //MÉTODOS//

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
                ocultarColumnas();
                cargarImagen(listaArticulo[0].ImagenUrl);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
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
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        
    }
}
