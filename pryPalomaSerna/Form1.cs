using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryPalomaSerna
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clsConexionBD objConectarBD = new clsConexionBD();
            objConectarBD.ConectarBD();
            clsConexionBD BD=new clsConexionBD();
            BD.Obtener(dgvProductos);


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                clsProducto producto = new clsProducto();
                clsConexionBD BD = new clsConexionBD();
                producto.Codigo = Convert.ToInt32(numCodigo.Value);
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.Precio = txtPrecio.Text;
                producto.Stock = Convert.ToInt32(numStock.Value);
                producto.Categoria = txtCategorias.Text;

                BD.Agregar(producto);
                BD.Obtener(dgvProductos);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se agrego el producto");
            }
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            numStock.Value = 0;
            txtCategorias.Text = "";

        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                clsProducto producto = new clsProducto();
                clsConexionBD BD = new clsConexionBD();
                producto.Codigo = Convert.ToInt32(numCodigo.Value);
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.Precio = txtPrecio.Text;
                producto.Stock = Convert.ToInt32(numStock.Value);
                producto.Categoria = txtCategorias.Text;

                BD.Modificar(producto);
                BD.Obtener(dgvProductos);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se agrego el producto");
            }
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            numStock.Value = 0;
            txtCategorias.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            clsProducto codigo= new clsProducto();
            clsConexionBD BD = new clsConexionBD();
            codigo.Codigo= Convert.ToInt32(numCodigo.Value);
            BD.Modificar(codigo);
            BD.Obtener(dgvProductos);
        }
    }
}
