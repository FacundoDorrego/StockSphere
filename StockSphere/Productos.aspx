<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="StockSphere.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {
            document.getElementById('divAgregarProducto').style.display = 'none';
            document.getElementById('divActualizarProducto').style.display = 'none';
            if (idFormulario !== '') {
                document.getElementById(idFormulario).style.display = 'block';
            }
        }

        function cerrarFormulario(idFormulario) {
            document.getElementById(idFormulario).style.display = 'none';
        }

        function abrirModalEliminar(productoID) {
            document.getElementById('<%= hiddenProductoIDEliminar.ClientID %>').value = productoID;
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));

            modal.show();
        }
    </script>

    <div class="container">
        <h2>Productos</h2>
        <div class="d-flex justify-content-center my-4">

            <asp:Button ID="btnMostrarAgregar" runat="server" Text="Agregar Producto" CssClass="btn btn-primary" OnClientClick="mostrarFormulario('divAgregarProducto'); return false;" OnClick="btnMostrarAgregar_Click" />
        </div>

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
                    <label for="txtPrecioProducto">Precio</label>
                    <asp:TextBox ID="txtPrecioProducto" runat="server" class="form-control" Placeholder="Precio del producto" />
                    <label for="txtStockProducto">Stock</label>
                    <asp:TextBox ID="txtStockProducto" runat="server" class="form-control" Placeholder="Stock del producto" />
                    <label for="ddlCategoriaProducto">Categoría</label>
                    <asp:DropDownList ID="ddlCategoriaProducto" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto" CssClass="btn btn-success" OnClick="btnAgregarProducto_Click" />
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
                    <label for="txtPrecioProductoActualizar">Precio</label>
                    <asp:TextBox ID="txtPrecioProductoActualizar" runat="server" class="form-control" Placeholder="Precio del producto" />
                    <label for="txtStockProductoActualizar">Stock</label>
                    <asp:TextBox ID="txtStockProductoActualizar" runat="server" class="form-control" Placeholder="Stock del producto" />
                    <label for="ddlCategoriaProductoActualizar">Categoría</label>
                    <asp:DropDownList ID="ddlCategoriaProductoActualizar" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnActualizarProducto" runat="server" Text="Actualizar Producto" CssClass="btn btn-warning" OnClick="btnActualizarProducto_Click" />
                </div>
                <div class="d-flex justify-content-center mt-3">
                    <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divActualizarProducto')">Cerrar</button>
                </div>
            </div>
        </div>

        <h4>Lista de Productos</h4>
        <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" OnRowCommand="dgvProductos_RowCommand" OnRowEditing="dgvProductos_RowEditing" DataKeyNames="ProductoID">
            <Columns>
                <asp:BoundField DataField="ProductoID" HeaderText="ID" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="CategoriaID" HeaderText="Categoría" HeaderStyle-CssClass="text-center" />
                <asp:TemplateField HeaderText="Opciones" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning" CommandName="Edit" CommandArgument='<%# Eval("ProductoID") %>' OnClientClick="document.getElementById('<%= hiddenProductoID.ClientID %>').value = this.commandArgument; mostrarFormulario('divActualizarProducto'); return false;" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClientClick='<%# "abrirModalEliminar(" + Eval("ProductoID") + "); return false;" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLabel">Confirmar Eliminación</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        ¿Estás seguro de que deseas eliminar este producto?
                   
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hiddenProductoIDEliminar" runat="server" />
                        <asp:HiddenField ID="hiddenStockProductoEliminar" runat="server" />

                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                    </div>
                </div>
            </div>
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
    </div>
</asp:Content>

