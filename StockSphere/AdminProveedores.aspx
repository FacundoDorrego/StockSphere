<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminProveedores.aspx.cs" Inherits="StockSphere.AdminProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="dgvProveedores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" 
     OnRowDeleting="dgvProveedores_RowDeleting" DataKeyNames="ProveedorID">
     <Columns>
         <asp:BoundField DataField="ProveedorID" HeaderText="ID" SortExpression="ProveedorID" HeaderStyle-CssClass="text-center" />
         <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HeaderStyle-CssClass="text-center" />
         <asp:BoundField DataField="Direccion" HeaderText="Dirección" SortExpression="Direccion" HeaderStyle-CssClass="text-center" />
         <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" HeaderStyle-CssClass="text-center" />
         <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-CssClass="text-center" />
         <asp:BoundField DataField="EmpresaID" HeaderText="Empresa" SortExpression="EmpresaID" HeaderStyle-CssClass="text-center" />
         <asp:CommandField ShowDeleteButton="True" 
             DeleteText="Eliminar" 
             ButtonType="Button" 
             HeaderStyle-CssClass="text-center" />
     </Columns>
 </asp:GridView>
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
</asp:Content>
