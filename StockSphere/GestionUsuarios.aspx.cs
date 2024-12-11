using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Repositorios;
using Clases;
using System.Text.RegularExpressions;

namespace StockSphere
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        private RepositorioUsuario repoUsuario = new RepositorioUsuario();
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
                divFiltros.Visible = false;
            }
            else
            {
                dgvUsuarios.Visible = true;
                listUsuarios.Visible = true;
                divFiltros.Visible = true;
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
                else if (!Regex.IsMatch(txtCorreoElectronicoModificar.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
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
                else if (!Regex.IsMatch(txtCorreoElectronicoAgregar.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }
                else
                {
                    int rolID = ddlIDRolAgregar.SelectedIndex + 1; //Correccion de index
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

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = ddlFiltro.SelectedValue;
            try
            {

                if (!string.IsNullOrEmpty(filtro))
                {
                    if (filtro == "Nombre")
                    {
                        string nombre = txtFiltro.Text;
                        List<Usuario> usuarios = repoUsuario.ObtenerUsuarios();
                        List<Usuario> usuariosFiltrados = usuarios.Where(usuario => usuario.NombreUsuario.Contains(nombre)).ToList();
                        if(usuariosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron usuarios con ese nombre.";
                            lblMensaje.Visible = true;
                        }
                        dgvUsuarios.DataSource = usuariosFiltrados;
                        dgvUsuarios.DataBind();

                    }
                    else if (filtro == "ID")
                    {
                        int id = Convert.ToInt32(txtFiltro.Text);
                        List<Usuario> usuarios = repoUsuario.ObtenerUsuarios();
                        List<Usuario> usuariosFiltrados = usuarios.Where(usuario => usuario.UsuarioID == id).ToList();
                        if (usuariosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron usuarios con ese ID.";
                            lblMensaje.Visible = true;
                        }
                        dgvUsuarios.DataSource = usuariosFiltrados;
                        dgvUsuarios.DataBind();
                    }


                }
                else
                {
                    lblMensaje.Text = "Debe seleccionar un filtro.";
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al filtrar las empresas: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            ddlFiltro.SelectedIndex = 0;
            CargarUsuarios();
        }
    }
}