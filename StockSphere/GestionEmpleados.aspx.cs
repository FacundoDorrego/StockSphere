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
                    CargarEmpleados(Convert.ToInt32(Request.QueryString["empresaID"]));
                }
            }
        }

        protected void CargarEmpleados(int empresaID)
        {
            try
            {
                RepositorioEmpleado repoEmpleado = new RepositorioEmpleado();
                dgvEmpleados.DataSource = repoEmpleado.ObtenerEmpleadosxEmpresa(empresaID);
                dgvEmpleados.DataBind();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
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
                    if (repoUsuario.Loguear(cuentaEmpleado))
                    {

                        bool asignado = AsignarEmpleado(cuentaEmpleado);
                        if (asignado)
                        {
                            lblMensaje.Text = "Empleado agregado correctamente";
                            lblMensaje.CssClass = "alert alert-success";
                            lblMensaje.Visible = true;
                        }
                        else
                        {
                            lblMensaje.Text = "Error al asignar empleado";
                            lblMensaje.CssClass = "alert alert-danger";
                            lblMensaje.Visible = true;

                        }
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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            int empresaIDVolver = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaIDVolver);
        }

        protected void dgvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnMostrarEmpleados_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.Visible == true)
            {
                dgvEmpleados.Visible = false;
            }
            else
            {
                dgvEmpleados.Visible = true;
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}