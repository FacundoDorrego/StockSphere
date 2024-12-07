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
    public partial class GestionEmpleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    //Algo
                }
            }
        }
        protected bool AsignarEmpleado(Usuario auxUsuario)
        {
            try
            {
                RepositorioEmpleado repoEmpleado = new RepositorioEmpleado();
                RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
                Empleado auxEmpleado = new Empleado();
                auxEmpleado.Usuario = auxUsuario;
                auxEmpleado.Empresa = repoEmpresa.ObtenerEmpresaxID(Convert.ToInt32(Request.QueryString["empresaID"]));
                repoEmpleado.AgregarEmpleado(auxEmpleado);
                return true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                return false;
            }
        }
        protected void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                RepositorioUsuario repoUsuario = new RepositorioUsuario();
                RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
                Usuario cuentaEmpleado = new Usuario();
                
                if (txtCorreo.Text == "" || txtNombre.Text == "" || txtPassword.Text == "")
                {
                    throw new Exception("Todos los campos son obligatorios");
                }
                else
                {
                    cuentaEmpleado.CorreoElectronico = txtCorreo.Text;
                    cuentaEmpleado.NombreUsuario = txtNombre.Text;
                    cuentaEmpleado.Clave = txtPassword.Text;
                    cuentaEmpleado.RolID = 3;
                   
                    repoUsuario.CrearUsuario(cuentaEmpleado);
                    //REVISAR PORQUE NO ASIGNA EMPLEADO PERO SI CREA EL USUARIO
                    bool asignado = AsignarEmpleado(cuentaEmpleado);
                    if (asignado)
                    {
                        lblMensaje.Text = "Empleado agregado correctamente";
                        lblMensaje.CssClass = "alert alert-success";
                        lblMensaje.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }
    }
}