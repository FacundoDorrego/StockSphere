using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Repositorio;
using Clases;

namespace StockSphere
{
    public partial class Empresas : System.Web.UI.Page
    {
        private RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
        private int usuarioID;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Usuario usuario = (Usuario)Session["usuario"];
                usuarioID = usuario.UsuarioID;
            }

            if (!IsPostBack)
            {
                CargarEmpresas();
            }
        }


        private void CargarEmpresas()
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

        protected void dgvEmpresas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int empresaID = Convert.ToInt32(dgvEmpresas.DataKeys[e.RowIndex].Value);
            try
            {
                RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
                repositorioEmpresa.EliminarEmpresa(empresaID);
                lblMensaje.Text = "¡Eliminado!";
                CargarEmpresas();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar la empresa: " + ex.Message;
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

            hiddenEmpresaID.Value = empresaID.ToString();
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
                    
                    string nombrenuevo = txtNombreEmpresaActualizar.Text;
                    Empresa auxEmpresa = new Empresa
                    {
                        EmpresaID = empresaID,
                        Nombre = nombrenuevo,
                        UsuarioID = usuarioID,
                        FechaCreacion = DateTime.Now,
                        Activa = true
                    };

                    RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
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