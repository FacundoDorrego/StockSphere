using System;
using System.Web;
using System.Web.UI;
using Repositorios;
using Clases;

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
                
                usuario = new Usuario(txtUsername.Text, txtPassword.Text);
                
                
                if (repousuario.Loguear(usuario))
                {
                    
                    Session.Add("usuario", usuario);
                    if(usuario.RolID == 3)
                    {
                        Empleado empleado = repoEmpleado.ObtenerEmpleadoxIDUsu(usuario.UsuarioID);
                        Session.Add("empleado", empleado);
                        Response.Redirect("GestionEmpresa.aspx?empresaID=" + empleado.Empresa.EmpresaID,false);
                    }
                    else
                    {
                    Response.Redirect("AdminEmpresas.aspx",false);

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
