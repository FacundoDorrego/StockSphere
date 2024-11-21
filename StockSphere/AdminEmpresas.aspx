<%@ Page Title="AdminEmpresas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminEmpresas.aspx.cs" Inherits="StockSphere.Empresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {
            
            document.getElementById('divCrearEmpresa').style.display = 'none';
            document.getElementById('divActualizarEmpresa').style.display = 'none';

            
            if (idFormulario !== '') {
                document.getElementById(idFormulario).style.display = 'block';
            }


        }

       
        function cerrarFormulario(idFormulario) {
            document.getElementById(idFormulario).style.display = 'none';
        }

        function abrirModalEliminar(empresaID) {
            document.getElementById('<%= hiddenEmpresaIDEliminar.ClientID %>').value = empresaID;
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));
            modal.show();
        }
</script>

    <div class="container">
        <asp:HiddenField ID="hiddenEmpresaID" runat="server" />
        <h2>Administrar Empresas</h2>

        <div id="divCrearEmpresa" class="card shadow-lg mb-4" style="display: none;">
            <div class="card-header">
                <h4>Crear Nueva Empresa</h4>
            </div>
            <div class="card-body">
                <div class="form-group mb-3">
                    <label for="txtNombreEmpresa">Nombre de la Empresa</label>
                    <asp:TextBox ID="txtNombreEmpresa" runat="server" class="form-control" Placeholder="Nombre de la empresa" />
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnCrearEmpresa" runat="server" Text="Crear Empresa" CssClass="btn btn-success" OnClick="btnCrearEmpresa_Click" />
                </div>
                <div class="d-flex justify-content-center mt-3">
                    <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divCrearEmpresa')">Cerrar</button>
                </div>
            </div>
        </div>

        <div id="divActualizarEmpresa" class="card shadow-lg mb-4" style="display: none;">
            <div class="card-header">
                <h4>Actualizar Empresa</h4>
            </div>
            <div class="card-body">
                <div class="form-group mb-3">

                    <label for="txtNombreEmpresaActualizar">Nuevo Nombre de la Empresa</label>
                    <asp:TextBox ID="txtNombreEmpresaActualizar" runat="server" class="form-control" Placeholder="Nuevo nombre de la empresa" />
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnActualizarEmpresa" runat="server" Text="Actualizar Empresa" CssClass="btn btn-warning" OnClick="btnActualizarEmpresa_Click" />
                </div>
                <div class="d-flex justify-content-center mt-3">
                    <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divActualizarEmpresa')">Cerrar</button>

                </div>
            </div>
        </div>


        <div class="container text-center">
            <div class="row">
                <div class="col">
                    <asp:Button ID="btnMostrarCrear" runat="server" Text="Agregar Empresa" CssClass="btn btn-primary" OnClientClick="mostrarFormulario('divCrearEmpresa'); return false;" />
                </div>
            </div>
        </div>



        <h4>Listado de empresas</h4>
        <asp:GridView ID="dgvEmpresas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" OnRowCommand="dgvEmpresas_RowCommand" OnRowEditing="dgvEmpresas_RowEditing" DataKeyNames="EmpresaID">
            <Columns>
                <asp:BoundField DataField="EmpresaID" HeaderText="ID" SortExpression="EmpresaID" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creación" SortExpression="FechaCreacion" HeaderStyle-CssClass="text-center" />
                <asp:CheckBoxField DataField="Activa" HeaderText="Activa" SortExpression="Activa" HeaderStyle-CssClass="text-center" />

                <asp:TemplateField HeaderText="Gestion Empresa" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:Button ID="btnGestionEmpresa" runat="server" Text="Gestionar Empresa"
                            CommandName="GestionEmpresa" CommandArgument='<%# Eval("EmpresaID") %>' class="btn btn-info" />

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Opciones" HeaderStyle-CssClass="text-center">
                    <ItemTemplate>
                       
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar"
                            CommandName="Edit" CssClass="btn btn-warning"
                            CommandArgument='<%# Eval("EmpresaID") %>'
                            OnClientClick="document.getElementById('<%= hiddenEmpresaID.ClientID %>').value = this.commandArgument; mostrarFormulario('divActualizarEmpresa'); return false;" />


                       
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                            OnClientClick='<%# "abrirModalEliminar(" + Eval("EmpresaID") + "); return false;" %>' />
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
                        ¿Estás seguro de que deseas eliminar esta empresa?
           
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hiddenEmpresaIDEliminar" runat="server" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                    </div>
                </div>
            </div>
        </div>


        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </div>
</asp:Content>
