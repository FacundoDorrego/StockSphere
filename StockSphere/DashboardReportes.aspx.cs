using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorio;
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
                // Calcular el total de todas las ventas
                decimal totalVentas = ventas.Sum(v => v.Monto);
                lblTotalVentas.Text = "Total Ventas: $" + totalVentas.ToString("F2");
                hfChartData.Value = Newtonsoft.Json.JsonConvert.SerializeObject(resumenVentas);
            }
            else
            {
                // Si no hay ventas, ocultar el gráfico y mostrar mensaje
                hfChartData.Value = string.Empty;
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
    }
}