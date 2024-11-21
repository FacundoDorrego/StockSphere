<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="StockSphere.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Administrar Proveedores</h2>
        <div class="card shadow-lg mb-4">
            <div class="card-header">
                <h4>Agregar Proveedor</h4>
            </div>
            <div class="card-body">
                <div class="form-group mb-3">
                    <label for="txtNombreProv">Nombre del proveedor</label>
                    <asp:TextBox ID="txtNombreProv" runat="server" class="form-control" Placeholder="Nombre del proveedor" />
                    <label for="txtTelefonoProv">Telefono del proveedor</label>
                    <asp:TextBox ID="txtTelefonoProv" runat="server" class="form-control" Placeholder="Telefono del proveedor" />
                    <label for="txtDireccProv">Direccion del proveedor</label>
                    <asp:TextBox ID="txtDireccProv" runat="server" class="form-control" Placeholder="Direccion del proveedor" />
                    <label for="txtEmailProv">Email del proveedor</label>
                    <asp:TextBox ID="txtEmailProv" runat="server" class="form-control" Placeholder="Email del proveedor" />
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnAgregarProveedor" runat="server" Text="Agregar Proveedor" class="btn btn-success" OnClick="btnAgregarProveedor_Click" />
                </div>
            </div>
        </div>
        <h2>Lista de Proveedores</h2>
        <asp:GridView ID="dgvProveedores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" OnRowDeleting="dgvProveedores_RowDeleting" DataKeyNames="ProveedorID">
            <Columns>
                <asp:BoundField DataField="ProveedorID" HeaderText="ID" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="text-center" />
                <asp:CommandField ShowDeleteButton="True" HeaderText="Opciones" DeleteText="Eliminar proveedor" ButtonType="Button" HeaderStyle-CssClass="text-center" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" OnClick="btnRegresar_Click" />
    </div>
</asp:Content>
