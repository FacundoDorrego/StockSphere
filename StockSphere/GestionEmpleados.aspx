﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionEmpleados.aspx.cs" Inherits="StockSphere.GestionEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {

            document.getElementById('divModificarEmpleado').style.display = 'none';


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

        function abrirModalEliminar(empleadoID) {
            document.getElementById('<%= hiddenEmpleadoIDEliminar.ClientID %>').value = empleadoID;
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));

            modal.show();
        }
</script>
    <asp:HiddenField ID="hiddenEmpleadoID" runat="server" />
    <!--hacer filtros--->
    <div class="card shadow-lg">
        <div class="card-header bg-primary text-white text-center">
            <h2>Módulo de Gestión - Empleados</h2>
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="false" class="mt-3"></asp:Label>
        </div>
        <div class="card-body">
            <div class="d-flex justify-content-center my-4">
                <asp:Button ID="btnMostrarEmpleados" runat="server" Text="Empleados Registrados" CssClass="btn btn-primary mx-2" OnClick="btnMostrarEmpleados_Click" />
                <asp:Button ID="btnMostrarAgregarEmpleados" runat="server" Text="Agregar Empleado" CssClass="btn btn-outline-success" OnClick="btnMostrarAgregarEmpleados_Click" />
            </div>
            <div id="divAgregarEmpleado" runat="server" class="card shadow-lg mb-4" visible="false">
                <div class="card-header">
                    <h4>Agregar Empleado</h4>
                </div>
                <div class="card-body">
                    <div class="form-group mb-4">
                        <label for="txtNombre" class="form-label">Nombre de usuario</label>
                        <asp:TextBox ID="txtNombre" runat="server" class="form-control mx-auto" Placeholder="Nombre de usuario" />
                    </div>

                    <div class="form-group mb-4">
                        <label for="txtCorreo" class="form-label">Correo electrónico</label>
                        <asp:TextBox ID="txtCorreo" runat="server" class="form-control mx-auto" Placeholder="Correo electrónico" />
                    </div>

                    <div class="form-group mb-4">
                        <label for="txtPassword" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" class="form-control mx-auto" TextMode="Password" Placeholder="Contraseña" />
                    </div>
                    <div class="d-flex justify-content-center">
                        <asp:Button ID="btnAgregarEmpleado" runat="server" Text="Agregar Empleado" CssClass="btn btn-outline-success mx-2" OnClick="btnAgregarEmpleado_Click" />
                    </div>
                    <div class="d-flex justify-content-center mt-3">

                        <asp:Button ID="btnCerrarAgregarEmpleado" class="btn btn-secondary" Text="Cerrar" runat="server" OnClick="btnCerrarAgregarEmpleado_Click" />
                    </div>
                </div>
            </div>
            <div id="divModificarEmpleado" class="card shadow-lg mb-4" style="display: none;" >
                <div class="card-header">
                    <h4>Modificar Empleado</h4>
                </div>
                <div class="card-body">
                    <div class="form-group mb-4">
                        <label for="txtIDEmpleadoMod" class="form-label">ID del empleado a modificar</label>
                        <asp:TextBox ID="txtIDEmpleadoMod" runat="server" class="form-control mx-auto" Placeholder="ID del empleado a modificar" ReadOnly="true" BackColor="LightGray" />
                    </div>
                    <div class="form-group mb-4">
                        <label for="txtNombreMod" class="form-label">Nombre de usuario</label>
                        <asp:TextBox ID="txtNombreMod" runat="server" class="form-control mx-auto" Placeholder="Nombre de usuario" />
                    </div>

                    <div class="form-group mb-4">
                        <label for="txtCorreoMod" class="form-label">Correo electrónico</label>
                        <asp:TextBox ID="txtCorreoMod" runat="server" class="form-control mx-auto" Placeholder="Correo electrónico" />
                    </div>

                    <div class="form-group mb-4">
                        <label for="txtPasswordMod" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPasswordMod" runat="server" class="form-control mx-auto" Placeholder="Contraseña" />
                    </div>
                    <div class="d-flex justify-content-center">
                        <asp:Button ID="btnModificarEmpleado" runat="server" Text="Modificar Empleado" CssClass="btn btn-outline-success mx-2" OnClick="btnModificarEmpleado_Click" />
                    </div>
                    <div class="d-flex justify-content-center mt-3">

                        <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divModificarEmpleado')">Cerrar</button>
                    </div>
                </div>
                <asp:Label ID="lblMensajeMod" runat="server" ForeColor="Red" Visible="false" class="mt-3"></asp:Label>
            </div>
            <h4 id="lblListaEmpleados" runat="server" visible="false">Lista de empleados registrados</h4>
            <asp:GridView ID="dgvEmpleados" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="dgvEmpleados_RowCommand" OnRowEditing="dgvEmpleados_RowEditing" DataKeyNames="EmpleadoID" Visible="false">
                <Columns>
                    <asp:BoundField DataField="EmpleadoID" HeaderText="ID del empleado" />
                    <asp:BoundField DataField="Usuario.UsuarioID" HeaderText="ID de usuario" />
                    <asp:BoundField DataField="Usuario.NombreUsuario" HeaderText="Nombre de usuario" />
                    <asp:BoundField DataField="Usuario.CorreoElectronico" HeaderText="Correo electrónico" />
                    <asp:BoundField DataField="Empresa.EmpresaID" HeaderText="ID de la empresa" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-outline-primary mx-2" CommandName="Edit" CommandArgument='<%# Eval("EmpleadoID") %>' />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger mx-2" CommandName="Eliminar" OnClientClick='<%# "abrirModalEliminar(" + Eval("EmpleadoID") + "); return false;" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div class="card-footer text-center">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" OnClick="btnRegresar_Click" />
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
                    ¿Estás seguro de que deseas eliminar este Empleado?
       
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hiddenEmpleadoIDEliminar" runat="server" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>


</asp:Content>
