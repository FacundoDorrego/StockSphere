using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorios;

namespace StockSphere
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            Usuario usuario = new Usuario();
            try
            {
                List<Usuario> usuariosRegistrados = repoUsuario.ObtenerUsuarios();
                usuario.Clave = txtPassword.Text;
                usuario.CorreoElectronico = txtCorreo.Text;
                usuario.NombreUsuario = txtNombre.Text;
                usuario.RolID = 2; //Rol de usuario
                foreach (Usuario u in usuariosRegistrados)
                {
                    if (u.CorreoElectronico == usuario.CorreoElectronico)
                    {
                        lblMensaje.Text = "Este mail ya se encuentra registrado.";
                        lblMensaje.Visible = true;
                        return;
                    }
                }
                   repoUsuario.CrearUsuario(usuario);
                   lblMensaje.Text = "Usuario registrado correctamente.";   
            } catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar usuario: " + ex.Message;
                lblMensaje.Visible = true;
            }
          
        }
    }
}