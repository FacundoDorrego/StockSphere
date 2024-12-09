using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Repositorios;
using Clases;
using System.Reflection;

namespace StockSphere
{
    public partial class Empresas : System.Web.UI.Page
    {
        private RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
        private int usuarioID;
        private int usuarioRol;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            else
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario.RolID == 3)
                {
                    Empleado empleado = (Empleado)Session["empleado"];
                    Response.Redirect("GestionEmpresa.aspx?empresaID=" + empleado.Empresa.EmpresaID, false);
                }
                if (usuario.RolID == 2)
                {
                    dgvEmpresas.Columns[3].Visible = false;
                    dgvEmpresas.Columns[5].Visible = false;
                }
                usuarioID = usuario.UsuarioID;
                usuarioRol = usuario.RolID;
            }

            if (!IsPostBack)
            {
                CargarEmpresas();
            }
        }


        private void CargarEmpresas()
        {
            if (usuarioRol == 1)
            {
                try
                {
                    List<Empresa> empresas = repositorioEmpresa.ObtenerEmpresasAdmin();
                    dgvEmpresas.DataSource = empresas;
                    dgvEmpresas.DataBind();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cargar las empresas: " + ex.Message;
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                try
                {
                    List<Empresa> empresas = repositorioEmpresa.ObtenerEmpresasPorUsuario(usuarioID);
                    List<Empresa> empresasActivas = empresas.Where(e => e.Activa).ToList();
                    dgvEmpresas.DataSource = empresasActivas;
                    dgvEmpresas.DataBind();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cargar las empresas: " + ex.Message;
                    lblMensaje.Visible = true;
                }
            }
        }

        protected void btnCrearEmpresa_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreEmpresa.Text;
            if (string.IsNullOrEmpty(nombre))
            {
                lblMensaje.Text = "Debe ingresar un nombre para la empresa.";
                lblMensaje.Visible = true;
                return;
            }
            try
            {
                Empresa auxEmpresa = new Empresa();
                RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
                auxEmpresa.Nombre = nombre;
                auxEmpresa.UsuarioID = usuarioID;
                auxEmpresa.FechaCreacion = DateTime.Now;
                auxEmpresa.Activa = true;
                repositorioEmpresa.AgregarEmpresa(auxEmpresa);
                CargarEmpresas();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar la empresa: " + ex.Message;
                lblMensaje.Visible = true;
            }

        }



        protected void dgvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GestionEmpresa")
            {

                int empresaID = Convert.ToInt32(e.CommandArgument);


                Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaID);
            }
        }
        protected void dgvEmpresas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int empresaID = Convert.ToInt32(dgvEmpresas.DataKeys[e.NewEditIndex].Value);
            txtNombreEmpresaActualizar.Text = dgvEmpresas.Rows[e.NewEditIndex].Cells[1].Text;
            hiddenEmpresaID.Value = empresaID.ToString();
            dgvEmpresas.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarActualizar", "mostrarFormulario('divActualizarEmpresa');", true);

        }

        protected void btnActualizarEmpresa_Click(object sender, EventArgs e)
        {
            try
            {

                int empresaID = Convert.ToInt32(hiddenEmpresaID.Value);
                if (int.TryParse(hiddenEmpresaID.Value, out empresaID))
                {

                    if (string.IsNullOrEmpty(txtNombreEmpresaActualizar.Text))
                    {
                        lblMensaje.Text = "Debe ingresar un nombre para la empresa.";
                        lblMensaje.Visible = true;
                        return;
                    }
                    RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
                    Empresa auxfecha = repositorioEmpresa.ObtenerEmpresaxID(empresaID);
                    DateTime fechaCreacion = auxfecha.FechaCreacion;
                    string nombrenuevo = txtNombreEmpresaActualizar.Text;
                    Empresa auxEmpresa = new Empresa
                    {
                        EmpresaID = empresaID,
                        Nombre = nombrenuevo,
                        UsuarioID = usuarioID,
                        FechaCreacion = auxfecha.FechaCreacion,
                        Activa = true
                    };

                    repositorioEmpresa.ActualizarEmpresa(auxEmpresa);
                    dgvEmpresas.EditIndex = -1;
                    CargarEmpresas();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar la empresa: " + ex.Message;
                lblMensaje.Visible = true;
            }

        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int empresaID = Convert.ToInt32(hiddenEmpresaIDEliminar.Value);
                RepositorioEmpresa repositorio = new RepositorioEmpresa();
                repositorio.EliminarEmpresa(empresaID);

                lblMensaje.Text = "Empresa eliminada exitosamente.";
                lblMensaje.CssClass = "alert alert-success";


                CargarEmpresas();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar la empresa: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
            }
        }
    }
}