<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="StockSphere.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {
            document.getElementById('divAgregarProducto').style.display = 'none';
            document.getElementById('divActualizarProducto').style.display = 'none';
            document.getElementById('divAgregarStock').style.display = 'none';
            document.getElementById('divRegistrarVenta').style.display = 'none';

            if (idFormulario !== '') {
                document.getElementById(idFormulario).style.display = 'block';
            }
        }

        function cerrarFormulario(idFormulario) {
            document.getElementById(idFormulario).style.display = 'none';
        }

        function confirmarEliminacion() {
            return confirm("¿Estás seguro de que deseas eliminar este producto?");
        }

        document.addEventListener('DOMContentLoaded', function () {

            const dropdownElementProductos = document.getElementById('<%= ddlProductos.ClientID %>');
            const dropdownElementVentas = document.getElementById('<%= ddlProductosVenta.ClientID %>');


            if (dropdownElementProductos) {
                inicializarDropdown(dropdownElementProductos);
            }
            if (dropdownElementVentas) {
                inicializarDropdown(dropdownElementVentas);
                dropdownElementVentas.addEventListener('click', function () {
                    __doPostBack(this.id, '');
                });


                dropdownElementVentas.onchange = forzarPostBack;
            }
        });


        function inicializarDropdown(dropdownElement) {
            if (dropdownElement && dropdownElement.options.length > 1) {
                dropdownElement.disabled = false;
                new Choices(dropdownElement, {
                    searchPlaceholderValue: 'Escriba para buscar...',
                    shouldSort: false,
                });
            } else if (dropdownElement && dropdownElement.options.length === 0) {
                dropdownElement.disabled = true;
            }
        }

        function forzarPostBack() {
            const ddl = document.getElementById('<%= ddlProductosVenta.ClientID %>');
            if (ddl) {
                const actualIndex = ddl.selectedIndex;
                ddl.selectedIndex = -1;
                ddl.selectedIndex = actualIndex;
                __doPostBack(ddl.id, '');
            }
        }
    </script>
    <div class="container">
        <div class="container mt-4">
            <div class="card shadow-lg">
                <div class="card-header bg-primary text-white text-center">
                    <h2>Módulo de Gestión - Productos</h2>
                </div>
                <div class="d-flex justify-content-center my-4">
                    <asp:Button ID="btnMostrarAgregar" CssClass="btn btn-success" runat="server" Text="Agregar Producto" OnClientClick="mostrarFormulario('divAgregarProducto'); return false;" OnClick="btnMostrarAgregar_Click" />
                    <asp:Button ID="btnAgregarStock" runat="server" Text="Agregar Stock" CssClass="btn btn-outline-primary mx-2" OnClientClick="mostrarFormulario('divAgregarStock'); return false;" />
                    <asp:Button ID="btnMostrarListado" runat="server" Text="Listado de Productos" CssClass="btn btn-outline-primary mx-2" OnClick="btnMostrarListado_Click" />
                    <asp:Button ID="btnMostrarMovimientos" runat="server" Text="Movimientos de Stock" CssClass="btn btn-outline-primary mx-2" OnClick="btnMostrarMovimientos_Click" />
                    <asp:Button ID="btnRegistrarVenta" runat="server" Text="Registrar Venta" CssClass="btn btn-outline-info mx-2" OnClientClick="mostrarFormulario('divRegistrarVenta'); return false;" />
                </div>
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
                <div id="divAgregarProducto" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Agregar Producto</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <label for="txtNombreProducto">Nombre</label>
                            <asp:TextBox ID="txtNombreProducto" runat="server" class="form-control" Placeholder="Nombre del producto" />
                            <label for="txtDescripcionProducto">Descripción</label>
                            <asp:TextBox ID="txtDescripcionProducto" runat="server" class="form-control" Placeholder="Descripción del producto" />
                            <label for="txtMarca">Marca</label>
                            <asp:TextBox ID="txtMarca" runat="server" class="form-control" Placeholder="Marca" />
                            <label for="txtPrecioProducto">Precio</label>
                            <asp:TextBox ID="txtPrecioProducto" runat="server" class="form-control" Placeholder="Precio del producto" />
                            <label for="txtStockProducto">Stock</label>
                            <asp:TextBox ID="txtStockProducto" runat="server" class="form-control" Placeholder="Stock del producto" />
                            <label for="ddlCategoriaProducto">Categoría</label>
                            <asp:DropDownList ID="ddlCategoriaProducto" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <label for="ddlProveedor">Proveedor</label>
                            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto" CssClass="btn btn-success" OnClick="btnAgregarProducto_Click" onkeydown="return event.key != 'Enter';" />
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divAgregarProducto')">Cerrar</button>
                        </div>
                    </div>
                </div>
                <div id="divActualizarProducto" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Actualizar Producto</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <label for="txtNombreProductoActualizar">Nombre</label>
                            <asp:TextBox ID="txtNombreProductoActualizar" runat="server" class="form-control" Placeholder="Nombre del producto" />
                            <label for="txtDescripcionProductoActualizar">Descripción</label>
                            <asp:TextBox ID="txtDescripcionProductoActualizar" runat="server" class="form-control" Placeholder="Descripción del producto" />
                            <label for="txtMarcaActualizar">Marca</label>
                            <asp:TextBox ID="txtMarcaActualizar" runat="server" class="form-control" Placeholder="Marca" />
                            <label for="txtPrecioProductoActualizar">Precio</label>
                            <asp:TextBox ID="txtPrecioProductoActualizar" runat="server" class="form-control" Placeholder="Precio del producto" />
                            <label for="txtStockProductoActualizar">Stock</label>
                            <asp:TextBox ID="txtStockProductoActualizar" runat="server" class="form-control" Placeholder="Stock del producto" />
                            <label for="ddlCategoriaProductoActualizar">Categoría</label>
                            <asp:DropDownList ID="ddlCategoriaProductoActualizar" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <label for="ddlProveedorActualizar">Proveedor</label>
                            <asp:DropDownList ID="ddlProveedorActualizar" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnActualizarProducto" runat="server" Text="Actualizar Producto" CssClass="btn btn-warning" OnClick="btnActualizarProducto_Click" onkeydown="return event.key != 'Enter';" />
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divActualizarProducto')">Cerrar</button>
                        </div>
                    </div>
                </div>
                <div id="divAgregarStock" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Agregar Stock</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged"></asp:DropDownList>
                            <asp:Label ID="lblResultado" runat="server"></asp:Label>
                            <div class="form-group mb-3">
                                <label for="txtCantidad">Cantidad</label>
                                <asp:TextBox ID="txtCantidad" runat="server" class="form-control" Placeholder="Cantidad a agregar" />
                            </div>
                            <div
                                class="d-flex justify-content-center">
                                <asp:Button ID="btnConfirmarAgregarStock" runat="server" Text="Agregar Stock" CssClass="btn btn-success" OnClick="btnConfirmarAgregarStock_Click" onkeydown="return event.key != 'Enter';" />
                            </div>
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divAgregarStock')">Cerrar</button>
                        </div>
                    </div>
                </div>
                <div id="divRegistrarVenta" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Registrar Venta</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <asp:DropDownList ID="ddlProductosVenta" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProductosVenta_SelectedIndexChanged"></asp:DropDownList>
                            <asp:Label ID="lblResultadoVenta" runat="server"></asp:Label>
                        </div>
                        <div class="form-group mb-3">
                            <label for="txtCantidadVenta">Cantidad Vendida</label>
                            <asp:TextBox ID="txtCantidadVenta" runat="server" class="form-control" Placeholder="Cantidad a vender" />
                        </div>
                        <div class="form-group mb-3">
                            <label for="txtNombreProdVenta">Nombre</label>
                            <asp:TextBox ID="txtNombreProdVenta" runat="server" class="form-control" Placeholder="Nombre del producto" ReadOnly="true" BackColor="LightGray"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label for="txtDescripcionProdVenta">Descripción</label>
                            <asp:TextBox ID="txtDescripcionProdVenta" runat="server" class="form-control" Placeholder="Descripción del producto" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label for="txtMarcaProdVenta">Marca</label>
                            <asp:TextBox ID="txtMarcaProdVenta" runat="server" class="form-control" Placeholder="Marca" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label for="txtPrecioProdVenta">Precio</label>
                            <asp:TextBox ID="txtPrecioProdVenta" runat="server" class="form-control" Placeholder="Precio del producto" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label for="txtStockProdVenta">Stock</label>
                            <asp:TextBox ID="txtStockProdVenta" runat="server" class="form-control" Placeholder="Stock del producto" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <asp:Button ID="btnConfirmarVenta" runat="server" Text="Registrar Venta" CssClass="btn btn-danger" OnClick="btnConfirmarVenta_Click" Visible="false" onkeydown="return event.key != 'Enter';" />
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divRegistrarVenta')">Cerrar</button>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hiddenProductoIDEliminar" runat="server" />
                <asp:HiddenField ID="hiddenStockEliminar" runat="server" />
                <div class="container my-4">
                    <h4 id="listprod" runat="server" visible="false" class="text-center">Listado de Productos</h4>
                    <div id="divFiltros" class="row mb-3 justify-content-center" runat="server" visible="false">
                        <div class="col-md-4">
                            <asp:Label ID="lblStockFiltro" Text="Seleccione el orden" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlStockFiltro" runat="server" CssClass="form-control" Visible="false">
                                <asp:ListItem Text="Mayor - Menor" Value="DESC" />
                                <asp:ListItem Text="Menor - Mayor" Value="ASC" />
                            </asp:DropDownList>
                            <asp:Label ID="lblCateFiltro" Text="Seleccione la categoria a filtrar" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlCategoriasFiltro" runat="server" CssClass="form-control" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="lblProvFiltro" Text="Seleccione el proveedor a filtrar" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlProveedoresFiltro" runat="server" CssClass="form-control" Visible="false">
                            </asp:DropDownList>

                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" Placeholder="Buscar"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                                <asp:ListItem Text="Seleccione un filtro" Value="" />
                                <asp:ListItem Text="Nombre de producto" Value="Nombre" />
                                <asp:ListItem Text="ID de producto" Value="ID" />
                                <asp:ListItem Text="Marca" Value="Marca" />
                                <asp:ListItem Text="Categoria" Value="Categoria" />
                                <asp:ListItem Text="Proveedor" Value="Proveedores" />
                                <asp:ListItem Text="Stock" Value="Stock">
                                </asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" onkeydown="return event.key != 'Enter';" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiarFiltro_Click" onkeydown="return event.key != 'Enter';" />
                        </div>
                    </div>
                    <div class="table-responsive" style="max-height: 520px; overflow-y: auto;">
                        <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" OnRowCommand="dgvProductos_RowCommand" OnRowEditing="dgvProductos_RowEditing" OnRowDataBound="dgvProductos_RowDataBound" DataKeyNames="ProductoID,Stock">
                            <Columns>
                                <asp:BoundField DataField="ProductoID" HeaderText="ID" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="CategoriaID" HeaderText="Categoría" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="ProveedorID" HeaderText="Proveedor" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:TemplateField HeaderText="Estado" HeaderStyle-CssClass="text-center sticky-top bg-light">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opciones" HeaderStyle-CssClass="text-center sticky-top bg-light">
                                    <ItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-outline-primary" CommandName="Edit" CommandArgument='<%# Eval("ProductoID") %>' OnClientClick="document.getElementById('<%= hiddenProductoID.ClientID %>').value = this.commandArgument; mostrarFormulario('divActualizarProducto'); return false;" />
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar " CssClass="btn btn-danger mx-1" CommandName="Eliminar" CommandArgument='<%# Eval("ProductoID") + "," + Eval("Stock") %>' OnClientClick="return confirmarEliminacion();" OnRowCommand="dgvProductos_RowCommand" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <h4 id="movstock" runat="server" visible="false" class="text-center">Movimientos de Stock</h4>
                    <div id="divFiltrosMovimientos" class="row mb-3 justify-content-center" runat="server" visible="false">
                        <div class="col-sm-4">
                            <div>
                                <asp:TextBox ID="txtFiltroMov" runat="server" CssClass="form-control" Placeholder="Buscar"></asp:TextBox>
                            </div>

                            <div class="col-sm-auto">
                                <div class="align-content-between">
                                    <asp:Label ID="lblFecha" Text="Introducir a partir de que fecha desea buscar: " runat="server" Visible="false"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtFechaFiltro" TextMode="Date" Visible="false" />
                                </div>
                            </div>
                            <div>
                                <asp:Label ID="lblTipoMovimiento" Text="Tipo de movimiento" runat="server" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlTipoMovimiento" runat="server" CssClass="form-control" Visible="false">
                                    <asp:ListItem Text="Seleccione un tipo de movimiento" Value="" />
                                    <asp:ListItem Text="Agregar" Value="Agregar" />
                                    <asp:ListItem Text="Venta" Value="Venta" />
                                    <asp:ListItem Text="Ingreso de Stock" Value="Ingreso" />
                                    <asp:ListItem Text="Eliminar" Value="Eliminar" />
                                </asp:DropDownList>
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
                            <asp:DropDownList ID="ddlFiltroMov" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltroMov_SelectedIndexChanged">
                                <asp:ListItem Text="Seleccione un filtro:" Value="" />
                                <asp:ListItem Text="ID de movimiento" Value="IDmov" />
                                <asp:ListItem Text="ID de usuario" Value="IDusu" />
                                <asp:ListItem Text="ID de producto" Value="IDprod" />
                                <asp:ListItem Text="Fecha" Value="Fecha" />
                                <asp:ListItem Text="Tipo de movimiento" Value="TipoMovimiento" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnFiltrarMovimientos" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrarMovimientos_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnLimpiarFiltroMovimientos" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiarFiltroMovimientos_Click" />
                        </div>
                    </div>
                    <div class="mt-3">
                        <asp:Label ID="lblMensajeMovimientos" runat="server"></asp:Label>
                    </div>
                    <div class="table-responsive" style="max-height: 520px; overflow-y: auto;">
                        <asp:GridView ID="dgvMovimientos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered">
                            <Columns>
                                <asp:BoundField DataField="MovimientoID" HeaderText="ID" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="EmpresaID" HeaderText="Empresa" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="UsuarioID" HeaderText="Usuario" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="ProductoID" HeaderText="Producto" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="TipoMovimiento" HeaderText="Tipo de Movimiento" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                                <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="card-footer text-center">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" class="btn btn-info" OnClick="btnRegresar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

