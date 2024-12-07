<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="StockSphere.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {
            document.getElementById('divAgregarUsuario').style.display = 'none';
            document.getElementById('divModificarUsuario').style.display = 'none';
            if (idFormulario !== '') {
                document.getElementById(idFormulario).style.display = 'block';
            }
        }
        function confirmarEliminacion() {
            return confirm("¿Estás seguro de que deseas eliminar este producto?");
        }


        function cerrarFormulario(idFormulario) {
            document.getElementById(idFormulario).style.display = 'none';
            dgvProveedores.EditIndex = -1;
        }

        function abrirModalEliminar(usuarioID) {
            document.getElementById('<%= hiddenUsuarioIDEliminar.ClientID %>').value = usuarioID;
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));

            modal.show();
        }
 </script>
    <asp:HiddenField ID="hiddenUsuarioID" runat="server" />
    <div class="card shadow-lg">
        <div class="card-header bg-primary text-white text-center">
            <h2>Módulo de Gestión - Usuarios</h2>
        </div>

        <div class="card-body">
            <div class="d-flex justify-content-center my-4">
                <asp:Button ID="btnMostrarUsuarios" runat="server" Text="Usuarios Registrados" CssClass="btn btn-primary mx-2" OnClick="btnMostrarUsuarios_Click" />
                <asp:Button ID="btnMostrarAgregarUsuario" runat="server" Text="Agregar Usuario" CssClass="btn btn-outline-success" OnClientClick="mostrarFormulario('divAgregarUsuario'); return false;" />
            </div>
            <div id="divModificarUsuario" class="card shadow-lg mb-4" style="display: none;">
                <div class="card-header">
                    <h4>Modificar Usuario</h4>
                </div>
                <div class="card-body">
                    <div class="form-group mb-3">
                        <label for="txtUsuarioIDModificar">ID del usuario: </label>
                        <asp:TextBox ID="txtUsuarioIDModificar" runat="server" class="form-control" Placeholder="ID del usuario" ReadOnly="true" BackColor="LightGray" />
                        <label for="txtNombreUsuarioModificar">Nombre del usuario: </label>
                        <asp:TextBox ID="txtNombreUsuarioModificar" runat="server" class="form-control" Placeholder="Nombre del usuario" />
                        <label for="txtCorreoElectronicoModificar">Correo electronico: </label>
                        <asp:TextBox ID="txtCorreoElectronicoModificar" runat="server" class="form-control" Placeholder="Correo Electronico" />
                        <label for="txtContraseñaUsuarioModificar">Contraseña: </label>
                        <asp:TextBox ID="txtContraseñaUsuarioModificar" runat="server" class="form-control" Placeholder="Contraseña" />
                        <label for="txtIDRolModificar">ID Rol: </label>
                        <asp:DropDownList ID="ddlIDRolModificar" runat="server" class="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="d-flex justify-content-center">
                        <asp:Button ID="btnModificarUsuario" runat="server" Text="Modificar Usuario" CssClass="btn btn-warning" OnClick="btnModificarUsuario_Click" />
                    </div>
                    <div class="d-flex justify-content-center mt-3">
                        <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divModificarUsuario')">Cerrar</button>
                    </div>
                </div>
            </div>
            <div id="divAgregarUsuario" class="card shadow-lg mb-4" style="display: none;">
                <div class="card-header">
                    <h4>Agregar Usuario</h4>
                </div>
                <div class="card-body">
                    <div class="form-group mb-3">
                        <label for="txtUsuarioIDAgregar">ID del usuario: </label>
                        <asp:TextBox ID="txtUsuarioIDAgregar" runat="server" class="form-control" Placeholder="ID del usuario" ReadOnly="true" BackColor="LightGray" />
                        <label for="txtNombreUsuarioAgregar">Nombre del usuario: </label>
                        <asp:TextBox ID="txtNombreUsuarioAgregar" runat="server" class="form-control" Placeholder="Nombre del usuario" />
                        <label for="txtCorreoElectronicoAgregar">Correo electronico: </label>
                        <asp:TextBox ID="txtCorreoElectronicoAgregar" runat="server" class="form-control" Placeholder="Correo Electronico" />
                        <label for="txtContraseñaUsuarioAgregar">Contraseña: </label>
                        <asp:TextBox ID="txtContraseñaUsuarioAgregar" runat="server" class="form-control" Placeholder="Contraseña" />
                        <label for="txtIDRolAgregar">ID Rol: </label>
                        <asp:DropDownList ID="ddlIDRolAgregar" runat="server" class="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="d-flex justify-content-center">
                        <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar Usuario" CssClass="btn btn-warning" OnClick="btnAgregarUsuario_Click" />
                    </div>
                    <div class="d-flex justify-content-center mt-3">
                        <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divAgregarUsuario')">Cerrar</button>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblMensaje" runat="server" />
            <div class="container my-4">
                <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
                    <h4 id="listUsuarios" runat="server" visible="false">Lista de usuarios registrados en el sistema</h4>
                    <asp:GridView ID="dgvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" Visible="false" OnRowCommand="dgvUsuarios_RowCommand" OnRowEditing="dgvUsuarios_RowEditing" DataKeyNames="UsuarioID">
                        <Columns>
                            <asp:BoundField DataField="UsuarioID" HeaderText="ID de Usuario" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de usuario" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                            <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electronico" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                            <asp:BoundField DataField="Clave" HeaderText="Contraseña" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                            <asp:BoundField DataField="RolID" HeaderText="Rol ID" HeaderStyle-CssClass="text-center sticky-top bg-light" />
                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center sticky-top bg-light">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server" Text="Modificar" CssClass="btn btn-warning" CommandName="Edit" CommandArgument='<%# Eval("UsuarioID") %>' OnClientClick="document.getElementById('<%= hiddenUsuarioID.ClientID %>').value = this.commandArgument; mostrarFormulario('divModificarUsuario'); return false;" />
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("UsuarioID") %>' OnClientClick='<%# "abrirModalEliminar(" + Eval("UsuarioID") + "); return false;" %>' OnRowCommand="dgvUsuarios_RowCommand" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="card-footer text-center">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" class="btn btn-info" OnClick="btnRegresar_Click" />
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
                    ¿Estás seguro de que deseas eliminar este usuario?
           
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hiddenUsuarioIDEliminar" runat="server" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
