using System;
using System.Web;
using System.Web.UI;
using Repositorio;
using Clases;

namespace StockSphere
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            lblMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            RepositorioUsuario repousuario = new RepositorioUsuario();

            try
            {
                
                usuario = new Usuario(txtUsername.Text, txtPassword.Text);
                
                
                if (repousuario.Loguear(usuario))
                {
                    
                    Session.Add("usuario", usuario);
                    lblMessage.Text = "ID USUARIO: " + usuario.UsuarioID.ToString();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                   
                    lblMessage.Visible = true;
                    lblMessage.Text = "Credenciales incorrectas. Por favor, intente de nuevo.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                
                lblMessage.Visible = true;
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
