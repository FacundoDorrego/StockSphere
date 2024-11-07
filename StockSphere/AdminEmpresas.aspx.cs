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
    public partial class AdminEmpresas : System.Web.UI.Page
    {
        private RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
        private int usuarioID;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está logueado
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx"); // Redirigir al login si no está logueado
            }
            else
            {
                Usuario usuario = (Usuario)Session["usuario"];
                usuarioID = usuario.UsuarioID; // Obtener el ID del usuario logueado
            }

            if (!IsPostBack)
            {
                CargarEmpresas();
            }
        }

        // Cargar las empresas asociadas al usuario
        private void CargarEmpresas()
        {
            try
            {
                List<Empresa> empresas = repositorioEmpresa.ObtenerEmpresasPorUsuario(usuarioID);
                Empresas
                dgv.DataSource = empresas;
                dgvEmpresas.DataBind(); // Esto vincula los datos al GridView
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
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearSp("AgregarEmpresa");
                accesoDatos.SetearParametros("@Nombre", nombre);
                accesoDatos.SetearParametros("@UsuarioID", usuarioID);
                accesoDatos.SetearParametros("@FechaCreacion", DateTime.Now);
                accesoDatos.SetearParametros("@Activa", true);
                accesoDatos.EjecutarAccion();
                CargarEmpresas();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar la empresa: " + ex.Message;
                lblMensaje.Visible = true;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        protected void dgvEmpresas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int empresaID = Convert.ToInt32(dgvEmpresas.DataKeys[e.RowIndex].Value);
            try
            {
                
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.SetearSp("EliminarEmpresa");
                accesoDatos.SetearParametros("@EmpresaID", empresaID);
                accesoDatos.EjecutarAccion();
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
            if (e.CommandName == "VerDetalles")
            {
                // Obtener el ID de la empresa desde el CommandArgument
                int empresaID = Convert.ToInt32(e.CommandArgument);

                // Redirigir a la página de detalles de la empresa pasando el ID como parámetro
                Response.Redirect("Empresa.aspx?empresaID=" + empresaID);
            }
        }
    }
}