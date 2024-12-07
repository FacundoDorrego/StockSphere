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
                CargarRoles();
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

        protected void CargarRoles()
        {
            RepositorioRol repoRol = new RepositorioRol();
            List<Rol> roles = repoRol.ObtenerRoles();
            ddlIDRolAgregar.DataSource = roles;
            ddlIDRolAgregar.DataTextField = "Descripcion";
            ddlIDRolAgregar.DataValueField = "RolID";
            ddlIDRolAgregar.DataBind();
            ddlIDRolModificar.DataSource = roles;
            ddlIDRolModificar.DataTextField = "Descripcion";
            ddlIDRolModificar.DataValueField = "RolID";
            ddlIDRolModificar.DataBind();
        }

        protected void RecargarTodo()
        {
            CargarUsuarios();
            CargarRoles();
            dgvUsuarios.DataBind();
            ddlIDRolAgregar.DataBind();
            ddlIDRolModificar.DataBind();
        }

        protected void btnMostrarUsuarios_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.Visible == true)
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
            int usuarioID = Convert.ToInt32(dgvUsuarios.DataKeys[e.NewEditIndex].Value);
            txtUsuarioIDModificar.Text = usuarioID.ToString();
            txtNombreUsuarioModificar.Text = dgvUsuarios.Rows[e.NewEditIndex].Cells[1].Text;
            txtCorreoElectronicoModificar.Text = dgvUsuarios.Rows[e.NewEditIndex].Cells[2].Text;
            txtContraseñaUsuarioModificar.Text = dgvUsuarios.Rows[e.NewEditIndex].Cells[3].Text;
            ddlIDRolModificar.Text = dgvUsuarios.Rows[e.NewEditIndex].Cells[4].Text;
            dgvUsuarios.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarModificar", "mostrarFormulario('divModificarUsuario');", true);

        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int usuarioID = Convert.ToInt32(hiddenUsuarioIDEliminar.Value);
            try
            {
                RepositorioUsuario repoUsuario = new RepositorioUsuario();
                repoUsuario.EliminarUsuario(usuarioID);
                lblMensaje.Text = "Usuario eliminado con éxito";
                lblMensaje.CssClass = "alert alert-success";
                RecargarTodo();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar usuario" + ex;
                lblMensaje.CssClass = "alert alert-danger";
            }
        }

        public void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreUsuarioModificar.Text) || string.IsNullOrWhiteSpace(txtCorreoElectronicoModificar.Text) || string.IsNullOrWhiteSpace(txtContraseñaUsuarioModificar.Text) || string.IsNullOrWhiteSpace(ddlIDRolModificar.Text))
                {
                    lblMensaje.Text = "Debe completar todos los campos.";
                    lblMensaje.CssClass = "alert alert-danger";
                    return;
                }
                else
                {
                    string nombreUsuario = txtNombreUsuarioModificar.Text;
                    string correoElectronico = txtCorreoElectronicoModificar.Text;
                    string contraseña = txtContraseñaUsuarioModificar.Text;
                    int rolID = ddlIDRolModificar.SelectedIndex + 1;
                    Usuario usuario = new Usuario(nombreUsuario, correoElectronico, contraseña, rolID);
                    usuario.UsuarioID = Convert.ToInt32(txtUsuarioIDModificar.Text);
                    RepositorioUsuario repoUsuario = new RepositorioUsuario();
                    repoUsuario.ModificarUsuario(usuario);
                    lblMensaje.Text = "Usuario modificado con éxito";
                    lblMensaje.CssClass = "alert alert-success";
                    dgvUsuarios.EditIndex = -1;
                    RecargarTodo();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
            }
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreUsuarioAgregar.Text) || string.IsNullOrWhiteSpace(txtCorreoElectronicoAgregar.Text) || string.IsNullOrWhiteSpace(txtContraseñaUsuarioAgregar.Text) || string.IsNullOrWhiteSpace(ddlIDRolModificar.Text))
                {
                    lblMensaje.Text = "Debe completar todos los campos.";
                    lblMensaje.CssClass = "alert alert-danger";
                }
                else
                {
                    int rolID = ddlIDRolAgregar.SelectedIndex +1; //Correccion de index
                    RepositorioUsuario repoUsuario = new RepositorioUsuario();
                    Usuario usuario = new Usuario(txtNombreUsuarioAgregar.Text, txtCorreoElectronicoAgregar.Text, txtContraseñaUsuarioAgregar.Text, rolID);
                    repoUsuario.CrearUsuario(usuario);
                    lblMensaje.Text = "Usuario creado con éxito";
                    lblMensaje.CssClass = "alert alert-success";
                    RecargarTodo();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
            }
        }
    }
}