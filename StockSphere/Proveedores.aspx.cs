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
    public partial class Proveedores : System.Web.UI.Page
    {
        //Falta poder agregar o eliminar proveedores.
        private RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
        private int empresaID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                CargarProveedores(empresaID);
            }

        }

        protected void CargarProveedores(int empresaID)
        {
            var proveedores = repositorioProveedor.ObtenerProveedoresxEmpresa(empresaID);
            Usuario usuario = (Usuario)Session["usuario"];
            Empresa empresaSelec = (Empresa)Session["empresaSelec"];
            if (proveedores.Count == 0 || proveedores == null)
            {
                lblMensaje.Text = "No hay proveedores para esta empresa";
                lblMensaje.Visible = true;
            }
            else if (empresaSelec == null || empresaSelec.UsuarioID != usuario.UsuarioID)
            {
                Response.Redirect("AdminEmpresas.aspx");

            }
            else
            {
                dgvProveedores.DataSource = proveedores;
                dgvProveedores.DataBind();
                lblMensaje.Visible = false;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaID);
        }
    }
}