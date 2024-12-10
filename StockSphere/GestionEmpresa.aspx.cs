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
    public partial class VerDetalleEmpresa : System.Web.UI.Page
    {
        private RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
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
                    if (Request.QueryString["empresaID"] != null)
                    {
                        Usuario usuario = (Usuario)Session["usuario"];
                        if (usuario.RolID == 3)
                        {
                            Empleado empleado = (Empleado)Session["empleado"];
                            empresaID = empleado.Empresa.EmpresaID;
                            CargarDetallesEmpresa(empresaID);
                            OcultarBotonesEmpleados();

                        }
                        
                        else
                        {
                            if (usuario.RolID == 2)
                            {
                                divActivo.Visible = false;
                            }
                            empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                            CargarDetallesEmpresa(empresaID);

                        }
                    }
                }

            }
        }

        protected void OcultarBotonesEmpleados()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario.RolID == 3)
            {
                btnProveedores.Visible = false;
                btnCategorias.Visible = false;
                btnDashboardReportes.Visible = false;
                btnGestionEmpleados.Visible = false;
            }
        }

        protected void CargarDetallesEmpresa(int empresaID)
        {
            Empresa empresaSelec = repositorioEmpresa.ObtenerEmpresaxID(empresaID);
            if (empresaID == 0)
            {
                Response.Redirect("AdminEmpresas.aspx");
            }

            if (empresaSelec == null)
            {
                Response.Redirect("AdminEmpresas.aspx");
            }
            else
            {
                lblEmpresaID.Text = empresaSelec.EmpresaID.ToString();
                lblActivo.Text = empresaSelec.Activa ? "Sí" : "No";
                lblEmpresaNombre.Text = empresaSelec.Nombre;
                lblEmpresaFechaCreacion.Text = empresaSelec.FechaCreacion.ToString("dd/MM/yyyy");
                Session.Add("empresaSelec", empresaSelec);
            }


        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEmpresas.aspx");
        }

        protected void btnProveedores_Click(object sender, EventArgs e)
        {

            Response.Redirect("Proveedores.aspx?empresaID=" + lblEmpresaID.Text);

        }

        protected void btnProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx?empresaID=" + lblEmpresaID.Text);
        }

        protected void btnCategorias_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categorias.aspx?empresaID=" + lblEmpresaID.Text);
        }

        protected void btnDashboardReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardReportes.aspx?empresaID=" + lblEmpresaID.Text);

        }

        protected void btnGestionEmpleados_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionEmpleados.aspx?empresaID=" + lblEmpresaID.Text);
        }
    }



}