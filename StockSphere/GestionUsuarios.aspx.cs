using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Repositorios;
using Clases;

namespace StockSphere
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario.RolID != 1)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            if (!IsPostBack)
            {
                CargarUsuarios();
                dgvUsuarios.Visible = false;
            }
        }

        protected void CargarUsuarios()
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            List<Usuario> usuarios = repoUsuario.ObtenerUsuarios();
            dgvUsuarios.DataSource = usuarios;
            dgvUsuarios.DataBind();
        }

        protected void btnMostrarUsuarios_Click(object sender, EventArgs e)
        {
            if(dgvUsuarios.Visible==true)
            {
                dgvUsuarios.Visible = false;
                listUsuarios.Visible = false;
            }
            else
            {
                dgvUsuarios.Visible = true;
                listUsuarios.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEmpresas.aspx");
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}