<%@ Page Title="Dashboard Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardReportes.aspx.cs" Inherits="StockSphere.DashboardReportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const chartData = document.getElementById('<%= hfChartData.ClientID %>').value;
            if (chartData) {
                const parsedData = JSON.parse(chartData);

                const labels = parsedData.map(item => item.Producto);
                const data = parsedData.map(item => item.TotalVentas);
                const backgroundColors = [
                    'rgba(255, 99, 132, 0.6)', // Rojo
                    'rgba(54, 162, 235, 0.6)', // Azul
                    'rgba(255, 206, 86, 0.6)', // Amarillo
                    'rgba(75, 192, 192, 0.6)', // Verde
                    'rgba(153, 102, 255, 0.6)', // Púrpura
                    'rgba(255, 159, 64, 0.6)',  // Naranja
                    'rgba(199, 199, 199, 0.6)'  // Gris
                ];

                const ctx = document.getElementById('chartVentas').getContext('2d');
                new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Ventas Totales ($)',
                            data: data,
                            backgroundColor: backgroundColors,
                            borderColor: 'rgba(255, 255, 255, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top'
                            },
                            title: {
                                display: true,
                                text: 'Ventas por Producto'
                            },
                            tooltip: {
                                callbacks: {
                                    label: function (context) {
                                        const label = context.label || '';
                                        const value = context.raw || 0;
                                        return `${label}: $${value}`;
                                    }
                                }
                            }
                        }
                    }
                });
            }
        });
</script>

    <div class="container mt-4">
        <div class="card shadow-lg">
            <div class="card-header bg-primary text-white text-center">
                <h2>Módulo de Gestión - Ventas</h2>
            </div>
            <div class="card-body">
                <div class="d-flex justify-content-center my-4">
                    <asp:Button ID="btnMostrarVentas" runat="server" Text="Listado de Ventas" CssClass="btn btn-primary mx-2" OnClick="btnMostrarVentas_Click" />
                </div>
                <div class="container my-4">
                    <h3 class="text-center">Estadísticas de Ventas</h3>
                    <asp:Label ID="lblTotalVentas" runat="server" CssClass="h4 text-primary text-center d-block mb-4"></asp:Label>
                    <canvas id="chartVentas" style="width: 100%; max-width: 400px; margin: auto;"></canvas>
                </div>
                <div class="container my-4">
                    <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
                        <h4 id="listVentas" runat="server" visible="false">Lista de Ventas</h4>
                        <asp:GridView ID="dgvVentas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" Visible="false">
                            <Columns>
                                <asp:BoundField DataField="VentasID" HeaderText="Venta N°:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="FechaVenta" HeaderText="Fecha de venta:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Usuario.UsuarioID" HeaderText="Usuario ID:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Usuario.NombreUsuario" HeaderText="Nombre del usuario:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="ProductoID" HeaderText="Producto:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre del producto:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Producto.Precio" HeaderText="Precio c/u:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad vendida:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Monto" HeaderText="Monto Total:" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="card-footer text-center">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" class="btn btn-info" OnClick="btnRegresar_Click" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfChartData" runat="server" />
</asp:Content>
