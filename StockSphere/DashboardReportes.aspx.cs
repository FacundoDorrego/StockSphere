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
                    CargarVentas(empresaID);

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

            if (dgvVentas.Visible == true)
            {
                listVentas.Visible = false;
                dgvVentas.Visible = false;
            }
            else
            {
                listVentas.Visible = true;
                dgvVentas.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            int empresaIDVolver = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaIDVolver);
        }
    }
}