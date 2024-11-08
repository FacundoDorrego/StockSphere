<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="StockSphere.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Lista de Proveedores</h2>
        <asp:GridView ID="dgvProveedores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered">
            <Columns>
                <asp:BoundField DataField="ProveedorID" HeaderText="ID" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" HeaderStyle-CssClass="text-center" />
                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="text-center" />
            </Columns>
        </asp:GridView>
        <asp:label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:label>
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" OnClick="btnRegresar_Click" />
    </div>
</asp:Content>
