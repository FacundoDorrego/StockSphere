using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorios;
using IronXL;
using System.Windows.Forms;

namespace StockSphere
{
    public partial class DashboardReportes : System.Web.UI.Page
    {
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

                    Usuario usuario = (Usuario)Session["usuario"];
                    empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                    RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
                    Empresa empresa = repoEmpresa.ObtenerEmpresaxID(empresaID);
                    if (empresa.UsuarioID != usuario.UsuarioID)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        CargarVentas(empresaID);

                    }

                }
            }
        }
        private void CargarVentas(int empresaID)
        {
            RepositorioVenta repoVenta = new RepositorioVenta();
            List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);

            if (ventas.Count > 0)
            {
                dgvVentas.DataSource = ventas;
                dgvVentas.DataBind();
                var resumenVentas = ventas.GroupBy(v => v.Producto.Nombre)
                    .Select(g => new
                    {
                        Producto = g.Key,
                        TotalVentas = g.Sum(v => v.Monto)
                    })
                    .ToList();
                var resumenMensual = ventas.GroupBy(v => new { Mes = v.FechaVenta.Month, NombreMes = v.FechaVenta.ToString("MMMM") }).Select
                    (g => new
                    {
                        Mes = g.Key.NombreMes,
                        MesNumero = g.Key.Mes,
                        TotalVentas = g.Sum(v => v.Monto)
                    }).OrderBy(g => g.MesNumero).ToList();
                decimal totalVentas = ventas.Sum(v => v.Monto);
                lblTotalVentas.Text = "Total Ventas: $" + totalVentas.ToString("F2");
                hfChartData.Value = Newtonsoft.Json.JsonConvert.SerializeObject(resumenVentas);
                hfChartDataMensual.Value = Newtonsoft.Json.JsonConvert.SerializeObject(resumenMensual);
            }
            else
            {

                hfChartData.Value = string.Empty;
                hfChartDataMensual.Value = string.Empty;
                lblTotalVentas.Text = "No hay ventas registradas.";
            }
        }


        protected void btnMostrarVentas_Click(object sender, EventArgs e)
        {

            if (divDgvVentas.Visible == true)
            {
                divDgvVentas.Visible = false;
                dgvVentas.Visible = false;
                divFiltros.Visible = false;
                txtFiltro.Visible = false;
            }
            else
            {
                divDgvVentas.Visible = true;
                dgvVentas.Visible = true;
                divFiltros.Visible = true;
                txtFiltro.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            int empresaIDVolver = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaIDVolver);
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlFiltro.SelectedIndex)
            {
                case 1:
                    txtFiltro.Visible = true;
                    ddlCategoriaVentas.Visible = false;
                    divFechaFiltro.Visible = false;
                    divDllFiltroFecha.Visible = false;
                    ddlFiltroFecha.Visible = false;
                    break;
                case 2:
                    txtFiltro.Visible = true;
                    ddlCategoriaVentas.Visible = false;
                    divFechaFiltro.Visible = false;
                    divDllFiltroFecha.Visible = false;
                    ddlFiltroFecha.Visible = false;
                    break;
                case 3:
                    txtFiltro.Visible = true;
                    ddlCategoriaVentas.Visible = false;
                    divFechaFiltro.Visible = false;
                    divDllFiltroFecha.Visible = false;
                    ddlFiltroFecha.Visible = false;
                    break;
                case 4:
                    txtFiltro.Visible = true;
                    ddlCategoriaVentas.Visible = false;
                    divFechaFiltro.Visible = false;
                    divDllFiltroFecha.Visible = false;
                    ddlFiltroFecha.Visible = false;
                    break;
                case 5:
                    txtFiltro.Visible = true;
                    ddlCategoriaVentas.Visible = false;
                    divFechaFiltro.Visible = false;
                    divDllFiltroFecha.Visible = false;
                    ddlFiltroFecha.Visible = false;
                    break;
                case 6:
                    txtFiltro.Visible = false;
                    ddlCategoriaVentas.Visible = true;

                    RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
                    List<Categoria> categorias = repositorioCategoria.ObtenerCategorias();
                    List<Categoria> categoriasEmpresa = new List<Categoria>();
                    int empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                    foreach (Categoria categoria in categorias)
                    {
                        if (categoria.EmpresaID == empresaID)
                        {
                            categoriasEmpresa.Add(categoria);
                        }
                    }
                    ddlCategoriaVentas.DataTextField = "Nombre";
                    ddlCategoriaVentas.DataValueField = "CategoriaID";
                    ddlCategoriaVentas.DataSource = categoriasEmpresa;
                    ddlCategoriaVentas.DataBind();
                    divFechaFiltro.Visible = false;
                    divDllFiltroFecha.Visible = false;
                    ddlFiltroFecha.Visible = false;
                    break;
                case 7:
                    txtFiltro.Visible = false;
                    ddlCategoriaVentas.Visible = false;
                    divFechaFiltro.Visible = true;
                    divDllFiltroFecha.Visible = true;
                    ddlFiltroFecha.Visible = true;
                    txtFechaFiltro.Visible = true;
                    break;
                default:
                    txtFiltro.Visible = true;
                    ddlCategoriaVentas.Visible = false;
                    divFechaFiltro.Visible = false;
                    divDllFiltroFecha.Visible = false;
                    txtFechaFiltro.Visible = false;
                    ddlFiltroFecha.Visible = false;
                    break;
            }
        }



        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            ddlFiltro.SelectedIndex = 0;
            txtFiltro.Text = "";
            lblMensaje.Visible = false;
            divFechaFiltro.Visible = false;
            txtFiltro.Visible = true;
            divDllFiltroFecha.Visible = false;
            ddlFiltroFecha.Visible = false;
            ddlCategoriaVentas.Visible = false;
            CargarVentas(Convert.ToInt32(Request.QueryString["empresaID"]));
        }



        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = ddlFiltro.SelectedValue;
            int empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            try
            {
                if (!string.IsNullOrEmpty(filtro))
                {
                    if (filtro == "IDventa")
                    {
                        if (string.IsNullOrEmpty(txtFiltro.Text))
                        {
                            lblMensaje.Text = "Debe ingresar un valor para filtrar.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        int id = Convert.ToInt32(txtFiltro.Text);
                        if (id <= 0)
                        {
                            lblMensaje.Text = "Ingrese un ID valido.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);
                        List<Venta> ventasFiltradas = ventas.Where(v => v.VentasID == id).ToList();
                        if (ventasFiltradas.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron ventas con ese ID.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        dgvVentas.DataSource = ventasFiltradas;
                        dgvVentas.DataBind();
                    }
                    else if (filtro == "IDusu")
                    {
                        if (string.IsNullOrEmpty(txtFiltro.Text))
                        {
                            lblMensaje.Text = "Debe ingresar un valor para filtrar.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        int id = Convert.ToInt32(txtFiltro.Text);
                        if (id <= 0)
                        {
                            lblMensaje.Text = "Ingrese un ID valido.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);
                        List<Venta> ventasFiltradas = ventas.Where(v => v.Usuario.UsuarioID == id).ToList();
                        if (ventasFiltradas.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron usuarios con ese ID.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        dgvVentas.DataSource = ventasFiltradas;
                        dgvVentas.DataBind();
                    }
                    else if (filtro == "IDprod")
                    {
                        if (string.IsNullOrEmpty(txtFiltro.Text))
                        {
                            lblMensaje.Text = "Debe ingresar un valor para filtrar.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        int id = Convert.ToInt32(txtFiltro.Text);
                        if (id <= 0)
                        {
                            lblMensaje.Text = "Ingrese un ID valido.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);
                        List<Venta> ventasFiltradas = ventas.Where(v => v.Producto.ProductoID == id).ToList();
                        if (ventasFiltradas.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron productos con ese ID.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        dgvVentas.DataSource = ventasFiltradas;
                        dgvVentas.DataBind();
                    }
                    else if (filtro == "NombreUsu")
                    {
                        string nombre = txtFiltro.Text;
                        if (string.IsNullOrEmpty(nombre))
                        {
                            lblMensaje.Text = "Ingrese un nombre valido.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);
                        List<Venta> ventasFiltradas = ventas.Where(v => v.Usuario.NombreUsuario.Contains(nombre)).ToList();
                        dgvVentas.DataSource = ventasFiltradas;
                        dgvVentas.DataBind();
                    }
                    else if (filtro == "NombreProd")
                    {
                        string nombre = txtFiltro.Text;
                        if (string.IsNullOrEmpty(nombre))
                        {
                            lblMensaje.Text = "Ingrese un nombre valido.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);
                        List<Venta> ventasFiltradas = ventas.Where(v => v.Producto.Nombre.Contains(nombre)).ToList();
                        if (ventasFiltradas.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron ventas con ese producto.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        dgvVentas.DataSource = ventasFiltradas;
                        dgvVentas.DataBind();
                    }
                    else if (filtro == "Categoria")
                    {
                        int categoriaID = Convert.ToInt32(ddlCategoriaVentas.SelectedValue);
                        if (categoriaID <= 0)
                        {
                            lblMensaje.Text = "Seleccione una categoria.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);
                        List<Venta> ventasFiltradas = ventas.Where(v => v.Categoria.CategoriaID == categoriaID).ToList();
                        if (ventasFiltradas.Count == 0)
                        {
                            lblMensaje.Text = "No se encontraron ventas con esa categoria.";
                            lblMensaje.Visible = true;
                            return;
                        }
                        dgvVentas.DataSource = ventasFiltradas;
                        dgvVentas.DataBind();
                    }
                    else if (filtro == "Fecha")
                    {
                        lblMensaje.Visible = false;
                        DateTime fecha = DateTime.Parse(txtFechaFiltro.Text);
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        List<Venta> ventas = repoVenta.ObtenerVentasxEmpresa(empresaID);
                        switch (ddlFiltroFecha.SelectedIndex)
                        {
                            case 1:
                                List<Venta> ventasFiltradasMayorA = ventas.Where(v => v.FechaVenta.Date > fecha.Date).ToList();
                                if (ventasFiltradasMayorA.Count == 0)
                                {
                                    lblMensaje.Text = "No hay movimientos en esa fecha.";
                                    lblMensaje.Visible = true;
                                    lblMensaje.CssClass = "alert alert-danger";
                                    return;
                                }
                                dgvVentas.DataSource = ventasFiltradasMayorA;
                                dgvVentas.DataBind();
                                break;
                            case 2:
                                List<Venta> ventasFiltradasIgualA = ventas.Where(v => v.FechaVenta.Date == fecha.Date).ToList();
                                if (ventasFiltradasIgualA.Count == 0)
                                {
                                    lblMensaje.Text = "No hay movimientos en esa fecha.";
                                    lblMensaje.Visible = true;
                                    lblMensaje.CssClass = "alert alert-danger";
                                    return;
                                }
                                dgvVentas.DataSource = ventasFiltradasIgualA;
                                dgvVentas.DataBind();
                                break;
                            case 3:
                                List<Venta> ventasFiltradasMenorA = ventas.Where(v => v.FechaVenta.Date < fecha.Date).ToList();
                                if (ventasFiltradasMenorA.Count == 0)
                                {
                                    lblMensaje.Text = "No hay movimientos en esa fecha.";
                                    lblMensaje.Visible = true;
                                    lblMensaje.CssClass = "alert alert-danger";
                                    return;
                                }
                                dgvVentas.DataSource = ventasFiltradasMenorA;
                                dgvVentas.DataBind();
                                break;
                            default:
                                lblMensaje.Text = "Debe seleccionar un filtro de fecha.";
                                lblMensaje.Visible = true;
                                lblMensaje.CssClass = "alert alert-danger";
                                return;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }

        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {

            var libro = WorkBook.Create(ExcelFileFormat.XLSX);
            var hoja = libro.CreateWorkSheet("Datos");


            for (int col = 0; col < dgvVentas.Columns.Count; col++)
            {
                hoja[$"{(char)('A' + col)}1"].Value = dgvVentas.Columns[col].HeaderText;
            }


            for (int row = 0; row < dgvVentas.Rows.Count; row++)
            {
                for (int col = 0; col < dgvVentas.Columns.Count; col++)
                {

                    var celda = dgvVentas.Rows[row].Cells[col];
                    string valor = celda.Text;
                    hoja[$"{(char)('A' + col)}{row + 2}"].Value = valor;
                }
            }
            string path = Server.MapPath("~/ReporteDeVentas.xlsx");
            libro.SaveAs(path);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", $"attachment; filename=ReporteDeVentas_{DateTime.Now:yyyy-MM-dd}.xlsx");
            Response.TransmitFile(path);
            Response.End();
        }
    }
}