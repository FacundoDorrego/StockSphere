<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionEmpleados.aspx.cs" Inherits="StockSphere.GestionEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group mb-4">
        <label for="txtNombre" class="form-label">Nombre de usuario</label>
        <asp:TextBox ID="txtNombre" runat="server" class="form-control mx-auto" Placeholder="Nombre de usuario" />
    </div>

    <div class="form-group mb-4">
        <label for="txtUsername" class="form-label">Correo electrónico</label>
        <asp:TextBox ID="txtCorreo" runat="server" class="form-control mx-auto" Placeholder="Correo electrónico" />
    </div>

    <div class="form-group mb-4">
        <label for="txtPassword" class="form-label">Contraseña</label>
        <asp:TextBox ID="txtPassword" runat="server" class="form-control mx-auto" TextMode="Password" Placeholder="Contraseña" />
    </div>
    <asp:Button ID="btnAgregarEmpleado" runat="server" Text="Agregar Empleado" CssClass="btn btn-outline-success mx-2" OnClick="btnAgregarEmpleado_Click" />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="false" class="mt-3"></asp:Label>
</asp:Content>
