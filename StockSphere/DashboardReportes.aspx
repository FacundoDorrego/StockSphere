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
                                        const total = context.dataset.data.reduce((sum, val) => sum + val, 0);
                                        const porcentaje = ((value / total) * 100).toFixed(2);

                                        return `${label}: $${value} (${porcentaje}%)`;
                                    }
                                }
                            }
                        }
                    }
                });
            }


            const chartMensualData = document.getElementById('<%= hfChartDataMensual.ClientID %>').value;
            if (chartMensualData) {
                const parsedMensualData = JSON.parse(chartMensualData);

                const labelsMensuales = parsedMensualData.map(item => item.Mes);
                const dataMensuales = parsedMensualData.map(item => item.TotalVentas);

                const ctxMensual = document.getElementById('chartVentasMensuales').getContext('2d');
                new Chart(ctxMensual, {
                    type: 'line',
                    data: {
                        labels: labelsMensuales,
                        datasets: [{
                            label: 'Ventas Totales por Mes ($)',
                            data: dataMensuales,
                            backgroundColor: 'rgba(75, 192, 192, 0.6)',
                            borderColor: 'rgba(75, 192, 192, 1)',
                            borderWidth: 2,
                            fill: true
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
                                text: 'Ventas Totales por Mes'
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
                        },
                        scales: {
                            x: {
                                title: {
                                    display: true,
                                    text: 'Mes'
                                }
                            },
                            y: {
                                title: {
                                    display: true,
                                    text: 'Monto ($)'
                                },
                                beginAtZero: true
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
                <h3 class="text-center">Graficos de Ventas</h3>
                <div class="row">

                    <div class="col-md-6 my-4">
                        <h4 class="text-center">Ventas Totales por Productos</h4>
                        <asp:Label ID="lblTotalVentas" runat="server" CssClass="h4 text-primary text-center d-block mb-4"></asp:Label>
                        <canvas id="chartVentas" style="width: 100%; max-width: 400px; margin: auto;"></canvas>
                        <asp:HiddenField ID="hfChartData" runat="server" />
                    </div>

                    <div class="col-md-6 my-4">
                        <h4 class="text-center">Ventas Totales por Mes</h4>
                        <canvas id="chartVentasMensuales" style="width: 100%; max-width: 600px; margin: auto;"></canvas>
                        <asp:HiddenField ID="hfChartDataMensual" runat="server" />
                    </div>
                </div>
                <div id="divDgvVentas" runat="server" visible="false">
                    <div id="divFiltros" class="row mb-3 justify-content-center" runat="server" visible="false">
                        <div class="col-sm-4">
                            <div>
                                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" Visible="false" Placeholder="Buscar"></asp:TextBox>
                                <asp:DropDownList ID="ddlCategoriaVentas" runat="server" Visible="false" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div id="divFechaFiltro" runat="server" visible="false" class="col-sm-auto">
                                <div class="align-content-between">
                                    <asp:Label ID="lblFecha" Text="Introducir a partir de que fecha desea buscar: " runat="server" Visible="false"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtFechaFiltro" TextMode="Date" Visible="false" />
                                </div>
                            </div>

                        </div>
                        <div id="divDllFiltroFecha" class="col-sm-2" runat="server" visible="false">
                            <asp:DropDownList ID="ddlFiltroFecha" runat="server" CssClass="form-control" Visible="false">
                                <asp:ListItem Text="Filtro de fecha:" Value="" />
                                <asp:ListItem Text="Mayor a" Value="MayorA" />
                                <asp:ListItem Text="Igual a" Value="IgualA" />
                                <asp:ListItem Text="Menor a" Value="MenorA" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                                <asp:ListItem Text="Seleccione un filtro:" Value="" />
                                <asp:ListItem Text="ID de venta" Value="IDventa" />
                                <asp:ListItem Text="Nombre de usuario" Value="NombreUsu" />
                                <asp:ListItem Text="ID de usuario" Value="IDusu" />
                                <asp:ListItem Text="Nombre de producto" Value="NombreProd" />
                                <asp:ListItem Text="ID de producto" Value="IDprod" />
                                <asp:ListItem Text="Categoría" Value="Categoria" />
                                <asp:ListItem Text="Fecha" Value="Fecha" />

                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiarFiltro_Click" />
                        </div>
                    </div>

                    <div class="container my-4">
                        <h4 id="listVentas" runat="server" visible="false">Lista de Ventas</h4>
                        <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
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
                        <div class="container">
                            <div class="row">
                                <div class="col-md-2 mx-auto p-4 text-center">

                                    <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar a Excel" CssClass="btn btn-success w-100" OnClick="btnExportarExcel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="card-footer text-center">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" class="btn btn-info" OnClick="btnRegresar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
