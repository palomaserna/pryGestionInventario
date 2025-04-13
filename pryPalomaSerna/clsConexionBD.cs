using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

using System.Windows.Forms;
using System.Data;

namespace pryPalomaSerna
{
    internal class clsConexionBD
    {
        //cadena de conexion
         string cadenaConexion = "Server=localhost;Database=Comercio;Trusted_Connection=True;";
        

        //conector
        SqlConnection coneccionBaseDatos;

        //comando
        SqlCommand comandoBaseDatos;

        public string nombreBaseDeDatos;


        public void ConectarBD()
        {
            try
            {
                coneccionBaseDatos = new SqlConnection(cadenaConexion);

                nombreBaseDeDatos = coneccionBaseDatos.Database;

                coneccionBaseDatos.Open();
                
                MessageBox.Show("Conectado a " + nombreBaseDeDatos);
            }
            catch (Exception error)
            {
                MessageBox.Show("Tiene un errorcito - " + error.Message);
            }     

        }

        public void Agregar(clsProducto Producto)
        {
            using (SqlConnection conexion= new SqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO Productos(Codigo, Nombre, Descripcion, Precio, Stock, Categoria)" + 
                    "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @Stock, @Categoria)";

                SqlCommand comando =new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Codigo", Producto.Codigo);
                comando.Parameters.AddWithValue("@Nombre", Producto.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", Producto.Descripcion);
                comando.Parameters.AddWithValue("@Precio", Producto.Precio);
                comando.Parameters.AddWithValue("@Stock", Producto.Stock);
                comando.Parameters.AddWithValue("@Categoria", Producto.Categoria);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

            // READ - Obtener todos los productos
           /* public List<clsProducto> ObtenerTodos()
            {
                List<clsProducto> lista = new List<clsProducto>();
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    string query = "SELECT * FROM Productos";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        clsProducto producto = new clsProducto
                        {
                            Codigo = Convert.ToInt32(reader["Codigo"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = reader["Precio"].ToString(),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            Categoria = reader["Categoria"].ToString(),
                        };
                        lista.Add(producto);
                    }
                }
                return lista;
           */ //}

        public DataTable Obtener(DataGridView dgvProductos)
        {
            DataTable tabla = new DataTable();
            string query = "SELECT Codigo, Nombre, Descripcion, Precio, Stock, Categoria FROM Productos";

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(tabla);
                    dgvProductos.DataSource = tabla;
                }
            }

            return tabla;
        }

        public void Modificar(clsProducto producto)
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    string query = "UPDATE Productos SET Nombre=@Nombre, Descripcion=@Descripcion, Precio=@Precio, " +
                                   "Stock=@Stock, Categoria=@Categoria WHERE Codigo=@Codigo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@Categoria", producto.Categoria);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            
            public void Eliminar(int codigo)
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    string query = "DELETE FROM Productos WHERE Codigo = @Codigo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }



