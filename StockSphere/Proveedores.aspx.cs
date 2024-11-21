using Clases;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockSphere
{
    public partial class Proveedores : System.Web.UI.Page
    {
        /
        private RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
        private int empresaID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                    CargarProveedores(empresaID);

                }
            }

        }

        protected void CargarProveedores(int empresaID)
        {
            var proveedores = repositorioProveedor.ObtenerProveedoresxEmpresa(empresaID);
            Usuario usuario = (Usuario)Session["usuario"];
            Empresa empresaSelec = (Empresa)Session["empresaSelec"];
            if (proveedores.Count == 0 || proveedores == null)
            {
                lblMensaje.Text = "No hay proveedores para esta empresa";
                lblMensaje.Visible = true;
            }
            else if (empresaSelec == null || empresaSelec.UsuarioID != usuario.UsuarioID)
            {
                Response.Redirect("AdminEmpresas.aspx");

            }
            else
            {
                dgvProveedores.DataSource = proveedores;
                dgvProveedores.DataBind();
                lblMensaje.Visible = false;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaID);
        }



        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreProv.Text == "" || txtTelefonoProv.Text == "" || txtEmailProv.Text == "" || txtDireccProv.Text == "")
                {
                    lblMensaje.Text = "Debe completar todos los campos";
                    lblMensaje.Visible = true;
                }
                else
                {

                    lblMensaje.Visible = false;
                    RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
                    Proveedor auxProveedor = new Proveedor();
                    auxProveedor.Nombre = txtNombreProv.Text;
                    auxProveedor.Telefono = txtTelefonoProv.Text;
                    auxProveedor.Email = txtEmailProv.Text;
                    auxProveedor.Direccion = txtDireccProv.Text;
                    auxProveedor.EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                    repositorioProveedor.AgregarProveedor(auxProveedor);
                    CargarProveedores(Convert.ToInt32(Request.QueryString["empresaID"]));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void dgvProveedores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int proveedorID = Convert.ToInt32(dgvProveedores.DataKeys[e.RowIndex].Value);
            try
            {
                RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
                repositorioProveedor.EliminarProveedor(proveedorID);
                lblMensaje.Text = "¡Eliminado!";
                CargarProveedores(Convert.ToInt32(Request.QueryString["empresaID"]));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el proveedor: " + ex.Message;
                lblMensaje.Visible = true;

            }
        }
    }
}