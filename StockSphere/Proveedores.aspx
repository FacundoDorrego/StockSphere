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
        <!--Hacer diseño y filtros--->
        <asp:HiddenField ID="hiddenProveedorID" runat="server" />
        <h2>Administrar Proveedores</h2>

        
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
                    <asp:Button ID="Button1" runat="server" Text="Agregar Proveedor" CssClass="btn btn-success" OnClick="btnAgregarProveedor_Click" />
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
                    <asp:Button ID="btnActualizarProveedor" runat="server" Text="Actualizar Proveedor" CssClass="btn btn-warning" OnClick="btnActualizarProveedor_Click" />
                </div>
                <div class="d-flex justify-content-center mt-3">
                    <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divActualizarProveedor')">Cerrar</button>
                </div>
            </div>
        </div>

        
        <div class="d-flex justify-content-center my-4">
            <asp:Button ID="btnMostrarAgregar" runat="server" Text="Agregar Proveedor" CssClass="btn btn-primary" OnClientClick="mostrarFormulario('divAgregarProveedor'); return false;" />
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        
        <h4>Lista de Proveedores</h4>
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
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" class="btn btn-info" OnClick="btnRegresar_Click1" />
        
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
