<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="StockSphere.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {
            document.getElementById('divAgregarProveedor').style.display = 'none';
            document.getElementById('divActualizarProveedor').style.display = 'none';
            if (idFormulario !== '') {
                document.getElementById(idFormulario).style.display = 'block';
            }
        }

        function cerrarFormulario(idFormulario) {
            document.getElementById(idFormulario).style.display = 'none';
            dgvProveedores.EditIndex = -1;
        }

        function abrirModalEliminar(proveedorID) {
            document.getElementById('<%= hiddenProveedorIDEliminar.ClientID %>').value = proveedorID;
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));

            modal.show();
        }
    </script>

    <div class="container">
        <asp:HiddenField ID="hiddenProveedorID" runat="server" />
        <div class="container mt-4">
            <div class="card shadow-lg">
                <div class="card-header bg-primary text-white text-center">
                    <h2>Módulo de Gestión - Proveedores</h2>
                </div>
                <div id="divAgregarProveedor" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Agregar Proveedor</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <label for="txtNombreProveedor">Nombre del proveedor</label>
                            <asp:TextBox ID="txtNombreProv" runat="server" class="form-control" Placeholder="Nombre del proveedor" />
                            <label for="txtTelefonoProv">Teléfono</label>
                            <asp:TextBox ID="txtTelefonoProv" runat="server" class="form-control" Placeholder="Teléfono" />
                            <label for="txtDireccProv">Dirección</label>
                            <asp:TextBox ID="txtDireccProv" runat="server" class="form-control" Placeholder="Dirección" />
                            <label for="txtEmailProv">Email</label>
                            <asp:TextBox ID="txtEmailProv" runat="server" class="form-control" Placeholder="Email" />
                        </div>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnAgregarProveedor" runat="server" Text="Agregar Proveedor" CssClass="btn btn-success" OnClick="btnAgregarProveedor_Click" onkeydown="return event.key != 'Enter';" />
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divAgregarProveedor')">Cerrar</button>
                        </div>
                    </div>
                </div>


                <div id="divActualizarProveedor" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Actualizar Proveedor</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <label for="txtNombreProveedorActualizar">Nombre del proveedor</label>
                            <asp:TextBox ID="txtNombreProveedorActualizar" runat="server" class="form-control" Placeholder="Nuevo nombre del proveedor" />
                            <label for="txtTelefonoProveedorActualizar">Teléfono</label>
                            <asp:TextBox ID="txtTelefonoProveedorActualizar" runat="server" class="form-control" Placeholder="Nuevo teléfono" />
                            <label for="txtDireccionProveedorActualizar">Dirección</label>
                            <asp:TextBox ID="txtDireccionProveedorActualizar" runat="server" class="form-control" Placeholder="Nueva dirección" />
                            <label for="txtEmailProveedorActualizar">Email</label>
                            <asp:TextBox ID="txtEmailProveedorActualizar" runat="server" class="form-control" Placeholder="Nuevo email" />
                        </div>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnActualizarProveedor" runat="server" Text="Actualizar Proveedor" CssClass="btn btn-success" OnClick="btnActualizarProveedor_Click" onkeydown="return event.key != 'Enter';" />
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divActualizarProveedor')">Cerrar</button>
                        </div>
                    </div>
                </div>


                <div class="d-flex justify-content-center my-4">
                    <asp:Button ID="btnMostrarAgregar" runat="server" Text="Agregar Proveedor" CssClass="btn btn-success mx-2" OnClientClick="mostrarFormulario('divAgregarProveedor'); return false;" />
                    <asp:Button ID="btnMostrarListado" runat="server" Text="Listado de Proveedores" CssClass="btn btn-outline-primary mx-2" OnClick="btnMostrarListado_Click" />
                </div>
                <div class="mt-4">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
                </div>
                <div id="divDgvProveedores" class="container my-4" runat="server" visible="false">
                    <h4 class="text-center">Listado de Proveedores</h4>
                    <div id="divFiltros" class="row mb-3 justify-content-center" runat="server">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" Placeholder="Buscar"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlFiltro" class="btn btn-secondary dropdown-toggle" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Seleccione un filtro" Value="" />
                                <asp:ListItem Text="Nombre del proveedor" Value="Nombre" />
                                <asp:ListItem Text="ID del proveedor" Value="ID">
                                </asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiarFiltro_Click" />
                        </div>
                    </div>
                    <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
                        <asp:GridView ID="dgvProveedores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" OnRowCommand="dgvProveedores_RowCommand" OnRowEditing="dgvProveedores_RowEditing" DataKeyNames="ProveedorID">
                            <Columns>
                                <asp:BoundField DataField="ProveedorID" HeaderText="ID" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="text-center" />
                                <asp:TemplateField HeaderText="Opciones" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-outline-primary mx-2" CommandName="Edit" CommandArgument='<%# Eval("ProveedorID") %>' OnClientClick="document.getElementById('<%= hiddenProveedorID.ClientID %>').value = this.commandArgument; mostrarFormulario('divActualizarProveedor'); return false;" />
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClientClick='<%# "abrirModalEliminar(" + Eval("ProveedorID") + "); return false;" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="card-footer text-center">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" class="btn btn-info" OnClick="btnRegresar_Click1" />
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLabel">Confirmar Eliminación</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        ¿Estás seguro de que deseas eliminar este proveedor?
                   
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hiddenProveedorIDEliminar" runat="server" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
