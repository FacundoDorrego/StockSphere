using Clases;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockSphere
{
    public partial class AdminProveedores : System.Web.UI.Page
    {
        private RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
        private int provedorID;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        /*protected void dgvProveedores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int proveedorID = Convert.ToInt32(dgvProveedores.DataKeys[e.RowIndex].Value);  // Accede a la clave primaria del proveedor
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.SetearSp("EliminarProveedor");  // Procedimiento almacenado para eliminar proveedor
                accesoDatos.SetearParametros("@ProveedorID", proveedorID);
                accesoDatos.EjecutarAccion();
                CargarProveedores();  // Recargar la lista de proveedores
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el proveedor: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }*/

        /*private void CargarProveedores()
        {
            try
            {
                List<Proveedor> proveedores = repositorioProveedor.ObtenerProveedores();
                dgvProveedores.DataSource = proveedores;
                dgvProveedores.DataBind(); // Esto vincula los datos al GridView
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los proveedores: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }*/
    }

}