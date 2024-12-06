<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="StockSphere.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {
            document.getElementById('divAgregarUsuario').style.display = 'none';
            document.getElementById('divActualizarUsuario').style.display = 'none';
            if (idFormulario !== '') {
                document.getElementById(idFormulario).style.display = 'block';
            }
        }

        function cerrarFormulario(idFormulario) {
            document.getElementById(idFormulario).style.display = 'none';
            dgvProveedores.EditIndex = -1;
        }

        function abrirModalEliminar(proveedorID) {
            document.getElementById('<%= hiddenUsuarioIDEliminar.ClientID %>').value = proveedorID;
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));

            modal.show();
        }
 </script>
    <div class="card shadow-lg">
        <div class="card-header bg-primary text-white text-center">
            <h2>Módulo de Gestión - Usuarios</h2>
        </div>




        <div class="card-body">
            <div class="d-flex justify-content-center my-4">
                <asp:Button ID="btnMostrarUsuarios" runat="server" Text="Usuarios Registrados" CssClass="btn btn-primary mx-2" OnClick="btnMostrarUsuarios_Click" />
            </div>

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
