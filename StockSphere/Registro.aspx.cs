using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            lblRedireccion.Visible = false;
            divRedireccion.Visible = false;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            Usuario usuario = new Usuario();
            try
            {
                List<Usuario> usuariosRegistrados = repoUsuario.ObtenerUsuarios();
                usuario.Clave = txtPassword.Text;
                usuario.CorreoElectronico = txtCorreoElectronico.Text;
                usuario.NombreUsuario = txtNombre.Text;
                usuario.RolID = 2;
                foreach (Usuario u in usuariosRegistrados)
                {
                    if (u.CorreoElectronico == usuario.CorreoElectronico)
                    {
                        lblMensaje.Text = "Este mail ya se encuentra registrado.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                        return;
                    }
                }
                if(txtCorreoElectronico.Text == "" || txtNombre.Text == "" || txtPassword.Text == "")
                {
                    lblMensaje.Text = "Todos los campos son obligatorios.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }
                if (!Regex.IsMatch(txtCorreoElectronico.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }
                repoUsuario.CrearUsuario(usuario);
                lblMensaje.Text = "Usuario registrado correctamente.";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                divRedireccion.Visible = true;
                lblRedireccion.Visible = true;
            
               
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar usuario: " + ex.Message;
                lblMensaje.Visible = true;
            }

        }

        
    }
}