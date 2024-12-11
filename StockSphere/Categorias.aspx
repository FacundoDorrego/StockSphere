<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="StockSphere.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function mostrarFormulario(idFormulario) {
            document.getElementById('divAgregarCategoria').style.display = 'none';
            document.getElementById('divActualizarCategoria').style.display = 'none';
            if (idFormulario !== '') {
                document.getElementById(idFormulario).style.display = 'block';
            }
        }

        function cerrarFormulario(idFormulario) {
            document.getElementById(idFormulario).style.display = 'none';
            dgvProveedores.EditIndex = -1;
        }

        function abrirModalEliminar(proveedorID) {
            document.getElementById('<%= hiddenCategoriaIDEliminar.ClientID %>').value = proveedorID;
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));

            modal.show();
        }
    </script>

    <div class="container">
        <asp:HiddenField ID="hiddenCategoriaID" runat="server" />
        <div class="container mt-4">
            <div class="card shadow-lg">
                <div class="card-header bg-primary text-white text-center">
                    <h2>Módulo de Gestión - Categorias</h2>
                </div>


                <div id="divAgregarCategoria" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Agregar Categoria</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <label for="txtNombreCategoria">Nombre de la proveedor</label>
                            <asp:TextBox ID="txtNombreCategoria" runat="server" class="form-control" Placeholder="Nombre de la Categoria" />
                            <label for="txtDescCategoria">Descripcion</label>
                            <asp:TextBox ID="txtDescCategoria" runat="server" class="form-control" Placeholder="Descripcion" />

                        </div>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar Categoria" CssClass="btn btn-success" OnClick="btnAgregarCategoria_Click" />
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divAgregarProveedor')">Cerrar</button>
                        </div>
                    </div>
                </div>


                <div id="divActualizarCategoria" class="card shadow-lg mb-4" style="display: none;">
                    <div class="card-header">
                        <h4>Actualizar Categoria</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <label for="txtNombreCatActualizar">Nuevo nombre de la categoria</label>
                            <asp:TextBox ID="txtNombreCatActualizar" runat="server" class="form-control" Placeholder="Nombre de la Categoria" />
                            <label for="txtDescCatActualizar">Descripcion Nueva</label>
                            <asp:TextBox ID="txtDescCatActualizar" runat="server" class="form-control" Placeholder="Descripcion" />
                        </div>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnActualizarCategoria" runat="server" Text="Actualizar Categoria" CssClass="btn btn-warning" OnClick="btnActualizarCategoria_Click" />
                        </div>
                        <div class="d-flex justify-content-center mt-3">
                            <button type="button" class="btn btn-secondary" onclick="cerrarFormulario('divActualizarCategoria')">Cerrar</button>
                        </div>
                    </div>
                </div>


                <div class="d-flex justify-content-center my-4">
                    <asp:Button ID="btnMostrarAgregar" runat="server" Text="Agregar Categoria" CssClass="btn btn-success mx-2" OnClientClick="mostrarFormulario('divAgregarCategoria'); return false;" />
                    <asp:Button ID="btnMostrarListado" runat="server" Text="Listado de Categorias" CssClass="btn btn-outline-primary mx-2" OnClick="btnMostrarListado_Click" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
                <div id="divDgvCategorias" class="container my-4" runat="server" visible="false">
                    <h4 class="text-center">Listado de Categorias</h4>
                    <div id="divFiltros" class="row mb-3 justify-content-center" runat="server">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" Placeholder="Buscar"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlFiltro" class="btn btn-secondary dropdown-toggle" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Seleccione un filtro" Value="" />
                                <asp:ListItem Text="Nombre de la Categoria" Value="Nombre" />
                                <asp:ListItem Text="ID de la Categoria" Value="ID">
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
                        <asp:GridView ID="dgvCategorias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" OnRowCommand="dgvCategorias_RowCommand" OnRowEditing="dgvCategorias_RowEditing" DataKeyNames="CategoriaID">
                            <Columns>
                                <asp:BoundField DataField="CategoriaID" HeaderText="ID" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="EmpresaID" HeaderText="EmpresaID" HeaderStyle-CssClass="text-center" />
                                <asp:TemplateField HeaderText="Opciones" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-outline-primary mx-2"
                                            CommandName="Edit" CommandArgument='<%# Eval("CategoriaID") %>'
                                            OnClientClick='<%# "document.getElementById(\"" + hiddenCategoriaID.ClientID + "\").value = \"" + Eval("CategoriaID") + "\"; mostrarFormulario(\"divActualizarCategoria\"); return false;" %>' />
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClientClick='<%# "abrirModalEliminar(" + Eval("CategoriaID") + "); return false;" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="card-footer text-center">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" class="btn btn-info" OnClick="btnRegresar_Click" />
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
                        ¿Estás seguro de que deseas eliminar esta categoria?
                   
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hiddenCategoriaIDEliminar" runat="server" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
