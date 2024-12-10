using System;
using System.Web;
using System.Web.UI;
using Repositorios;
using Clases;
using System.Text.RegularExpressions;

namespace StockSphere
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Response.Redirect("AdminEmpresas.aspx");
            }
            lblMensaje.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            RepositorioUsuario repousuario = new RepositorioUsuario();
            RepositorioEmpleado repoEmpleado = new RepositorioEmpleado();

            try
            {

                usuario = new Usuario(txtCorreoElectronico.Text, txtPassword.Text);
                if (usuario.CorreoElectronico == "" || usuario.Clave == "")
                {

                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Por favor, complete todos los campos.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;

                }
                // Validar formato del correo
                if (!Regex.IsMatch(txtCorreoElectronico.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.Visible = true;
                    return;
                }
                if (repousuario.Loguear(usuario))
                {

                    Session.Add("usuario", usuario);
                    if (usuario.RolID == 3)
                    {
                        Empleado empleado = repoEmpleado.ObtenerEmpleadoxIDUsu(usuario.UsuarioID);
                        Session.Add("empleado", empleado);
                        Response.Redirect("GestionEmpresa.aspx?empresaID=" + empleado.Empresa.EmpresaID, false);
                    }
                    else
                    {
                        Response.Redirect("AdminEmpresas.aspx", false);

                    }
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {

                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Credenciales incorrectas. Por favor, intente de nuevo.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {

                lblMensaje.Visible = true;
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}
