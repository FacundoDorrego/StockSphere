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
    public partial class GestionEmpleados : System.Web.UI.Page
    {
        private RepositorioEmpleado repoEmpleado = new RepositorioEmpleado();
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
                else if (!Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
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

                RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
                Empleado auxEmpleado = new Empleado();
                auxEmpleado.Usuario = auxUsuario;
                auxEmpleado.Empresa = repoEmpresa.ObtenerEmpresaxID(Convert.ToInt32(Request.QueryString["empresaID"]));
                repoEmpleado.AgregarEmpleado(auxEmpleado);
                CargarEmpleados(Convert.ToInt32(Request.QueryString["empresaID"]));
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
            if (divDgvEmpleados.Visible == true)
            {
                divDgvEmpleados.Visible = false;
                dgvEmpleados.Visible = false;
                lblListaEmpleados.Visible = false;
            }
            else
            {
                divDgvEmpleados.Visible = true;
                lblListaEmpleados.Visible = true;
                dgvEmpleados.Visible = true;
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int empleadoID = Convert.ToInt32(hiddenEmpleadoIDEliminar.Value);
                repoEmpleado.EliminarEmpleado(empleadoID);
                lblMensaje.Text = "Empleado eliminado correctamente";
                lblMensaje.CssClass = "alert alert-success";
                lblMensaje.Visible = true;
                CargarEmpleados(Convert.ToInt32(Request.QueryString["empresaID"]));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void btnMostrarAgregarEmpleados_Click(object sender, EventArgs e)
        {
            divAgregarEmpleado.Visible = true;
            txtIDEmpleadoMod.Visible = false;
        }

        protected void btnCerrarAgregarEmpleado_Click(object sender, EventArgs e)
        {
            divAgregarEmpleado.Visible = false;
        }

        protected void btnModificarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                int empleadoID = Convert.ToInt32(txtIDEmpleadoMod.Text);
                if (txtNombreMod.Text == "" || txtCorreoMod.Text == "" || txtPasswordMod.Text == "")
                {
                    throw new Exception("Todos los campos son obligatorios");
                }
                else if (!Regex.IsMatch(txtCorreoMod.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }
                else
                {

                    Empleado empleado = repoEmpleado.ObtenerEmpleadoxID(empleadoID);
                    RepositorioUsuario repoUsuario = new RepositorioUsuario();
                    Usuario cuentaEmpleado = new Usuario();
                    cuentaEmpleado.UsuarioID = empleado.Usuario.UsuarioID;
                    cuentaEmpleado.CorreoElectronico = txtCorreoMod.Text;
                    cuentaEmpleado.NombreUsuario = txtNombreMod.Text;
                    cuentaEmpleado.Clave = txtPasswordMod.Text;
                    cuentaEmpleado.RolID = empleado.Usuario.RolID;
                    repoUsuario.ModificarUsuario(cuentaEmpleado);

                    lblMensajeMod.Text = "Empleado modificado correctamente";
                    lblMensajeMod.CssClass = "alert alert-success";
                    lblMensajeMod.Visible = true;
                    dgvEmpleados.EditIndex = -1;
                    CargarEmpleados(Convert.ToInt32(Request.QueryString["empresaID"]));
                }
            }
            catch (Exception ex)
            {
                lblMensajeMod.Text = ex.Message;
                lblMensajeMod.CssClass = "alert alert-danger";
                lblMensajeMod.Visible = true;
            }
        }




        protected void dgvEmpleados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            txtIDEmpleadoMod.Visible = true;
            int empleadoID = Convert.ToInt32(dgvEmpleados.DataKeys[e.NewEditIndex].Value);
            Empleado empleadoSelecc = repoEmpleado.ObtenerEmpleadoxID(empleadoID);
            txtIDEmpleadoMod.Text = empleadoSelecc.EmpleadoID.ToString();
            txtNombreMod.Text = empleadoSelecc.Usuario.NombreUsuario;
            txtCorreoMod.Text = empleadoSelecc.Usuario.CorreoElectronico;
            txtPasswordMod.Text = empleadoSelecc.Usuario.Clave;
            dgvEmpleados.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarModificar", "mostrarFormulario('divModificarEmpleado');", true);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = ddlFiltro.SelectedValue;
            int empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            try
            {
                if (!string.IsNullOrEmpty(filtro))
                {
                    if (filtro == "Nombre")
                    {
                        string nombre = txtFiltro.Text;
                        List<Empleado> empleados = repoEmpleado.ObtenerEmpleadosxEmpresa(empresaID);
                        List<Empleado> empleadosFiltrados = new List<Empleado>();
                        empleadosFiltrados = empleados.Where(empleado => empleado.Usuario.NombreUsuario.Contains(nombre)).ToList();
                        if(empleadosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron empleados con ese nombre";
                            lblMensaje.CssClass = "alert alert-warning";
                            lblMensaje.Visible = true;
                        }
                        dgvEmpleados.DataSource = empleadosFiltrados;
                        dgvEmpleados.DataBind();
                    }
                    else if (filtro == "ID")
                    {
                        int id = Convert.ToInt32(txtFiltro.Text);
                        List<Empleado> empleados = repoEmpleado.ObtenerEmpleadosxEmpresa(empresaID);
                        List<Empleado> empleadosFiltrados = new List<Empleado>();
                        empleadosFiltrados = empleados.Where(empleado => empleado.EmpleadoID == id).ToList();
                        if(empleadosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron empleados con ese ID";
                            lblMensaje.CssClass = "alert alert-warning";
                            lblMensaje.Visible = true;
                        }
                        dgvEmpleados.DataSource = empleadosFiltrados;
                        dgvEmpleados.DataBind();

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

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            lblMensaje.Visible = false;
            lblMensajeMod.Visible = false;
            CargarEmpleados(Convert.ToInt32(Request.QueryString["empresaID"]));

        }
    }
}