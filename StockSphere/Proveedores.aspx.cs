using Clases;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockSphere
{
    public partial class Proveedores : System.Web.UI.Page
    {

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
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
                dgvProveedores.Visible = false;
            }
            else if (empresaSelec == null || empresaSelec.UsuarioID != usuario.UsuarioID)
            {
                Response.Redirect("AdminEmpresas.aspx");

            }
            else
            {
                dgvProveedores.DataSource = proveedores;
                dgvProveedores.DataBind();
                dgvProveedores.Visible = true;
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
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                }
                if (!Regex.IsMatch(txtEmailProv.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }
                string telefono = txtTelefonoProv.Text;
                if (!Regex.IsMatch(telefono, @"^\d*$"))
                {

                    txtTelefonoProv.Text = Regex.Replace(telefono, @"\D", "");
                    lblMensaje.Text = "Por favor, ingrese un telefono valido.";
                    lblMensaje.CssClass = "alert alert-danger";
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



        protected void dgvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int proveedorID = Convert.ToInt32(hiddenProveedorIDEliminar.Value);
            try
            {
                RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
                repositorioProveedor.EliminarProveedor(proveedorID);
                lblMensaje.Text = "¡Eliminado!";

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el proveedor: " + ex.Message;

                lblMensaje.Visible = true;

            }
            finally
            {
                CargarProveedores(Convert.ToInt32(Request.QueryString["empresaID"]));
            }
        }

        protected void btnActualizarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProveedores.EditIndex = -1;
                if (string.IsNullOrWhiteSpace(txtNombreProveedorActualizar.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefonoProveedorActualizar.Text) ||
                    string.IsNullOrWhiteSpace(txtEmailProveedorActualizar.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccionProveedorActualizar.Text))
                {
                    lblMensaje.Text = "Debe completar todos los campos.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }
                if (!Regex.IsMatch(txtEmailProveedorActualizar.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }
                string telefono = txtTelefonoProveedorActualizar.Text;
                if (!Regex.IsMatch(telefono, @"^\d*$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un telefono valido.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }

                else
                {
                    int proveedorID = Convert.ToInt32(hiddenProveedorID.Value);
                    lblMensaje.Visible = false;
                    RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
                    Proveedor auxProveedor = new Proveedor
                    {
                        ProveedorID = proveedorID,
                        Nombre = txtNombreProveedorActualizar.Text,
                        Telefono = txtTelefonoProveedorActualizar.Text,
                        Email = txtEmailProveedorActualizar.Text,
                        Direccion = txtDireccionProveedorActualizar.Text,
                        EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"])
                    };
                    dgvProveedores.EditIndex = -1;
                    repositorioProveedor.ActualizarProveedor(auxProveedor);
                    CargarProveedores(Convert.ToInt32(Request.QueryString["empresaID"]));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void dgvProveedores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int proveedorID = Convert.ToInt32(dgvProveedores.DataKeys[e.NewEditIndex].Value);
            txtNombreProveedorActualizar.Text = dgvProveedores.Rows[e.NewEditIndex].Cells[1].Text;
            txtTelefonoProveedorActualizar.Text = dgvProveedores.Rows[e.NewEditIndex].Cells[2].Text;
            txtDireccionProveedorActualizar.Text = dgvProveedores.Rows[e.NewEditIndex].Cells[3].Text;
            txtEmailProveedorActualizar.Text = dgvProveedores.Rows[e.NewEditIndex].Cells[4].Text;
            hiddenProveedorID.Value = proveedorID.ToString();
            dgvProveedores.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarActualizar", "mostrarFormulario('divActualizarProveedor');", true);
        }

        protected void btnRegresar_Click1(object sender, EventArgs e)
        {
            int empresaIDVolver = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaIDVolver);
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
                        if(nombre == "")
                        {
                            lblMensaje.Text = "Debe ingresar un nombre para filtrar.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        List<Proveedor> proveedores = repositorioProveedor.ObtenerProveedoresxEmpresa(empresaID);
                        List<Proveedor> proveedoresFiltrados = proveedores.Where(proveedor => proveedor.Nombre.Contains(nombre)).ToList();
                        dgvProveedores.DataSource = proveedoresFiltrados;
                        dgvProveedores.DataBind();

                    }
                    else if (filtro == "ID")
                    {
                        int id = Convert.ToInt32(txtFiltro.Text);
                        if(id <= 0)
                        {
                            lblMensaje.Text = "Debe ingresar un ID para filtrar.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        List<Proveedor> proveedores = repositorioProveedor.ObtenerProveedoresxEmpresa(empresaID);
                        List<Proveedor> proveedoresFiltrados = proveedores.Where(proveedor => proveedor.ProveedorID == id).ToList();
                        dgvProveedores.DataSource = proveedoresFiltrados;
                        dgvProveedores.DataBind();
                    }


                }
                else
                {
                    lblMensaje.Text = "Debe seleccionar un filtro.";
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "alert alert-danger";
                    CargarProveedores(empresaID);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al filtrar los proveedores: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            ddlFiltro.SelectedIndex = 0; 
            lblMensaje.Visible = false;
            CargarProveedores(Convert.ToInt32(Request.QueryString["empresaID"]));
        }



        protected void btnMostrarListado_Click(object sender, EventArgs e)
        {
            if (divDgvProveedores.Visible == true)
            {
                divDgvProveedores.Visible = false;
            }
            else
            {
                divDgvProveedores.Visible = true;
            }
        }

      

       
    }
}


