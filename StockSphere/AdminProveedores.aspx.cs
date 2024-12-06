using Clases;
using Repositorios;
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
        private int proveedorID;
        private int empresaID;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void dgvProveedores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int proveedorID = Convert.ToInt32(dgvProveedores.DataKeys[e.RowIndex].Value); 
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.SetearSp("EliminarProveedor");  
                accesoDatos.SetearParametros("@ProveedorID", proveedorID);
                accesoDatos.EjecutarAccion();
                CargarProveedores();  
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el proveedor: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        private void CargarProveedores()
        {
            try
            {
                empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                List<Proveedor> proveedores = repositorioProveedor.ObtenerProveedoresxEmpresa(empresaID);
                dgvProveedores.DataSource = proveedores;
                dgvProveedores.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los proveedores: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }

}