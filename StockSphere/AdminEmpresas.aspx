<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminEmpresas.aspx.cs" Inherits="StockSphere.Empresas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Administrar Empresas</h2>
        <!-- Formulario para crear nueva empresa -->
       <div class="card shadow-lg mb-4">
            <div class="card-header">
                <h4>Crear Nueva Empresa</h4>
            </div>
            <div class="card-body">
                <div class="form-group mb-3">
                    <label for="txtNombreEmpresa">Nombre de la Empresa</label>
                    <asp:TextBox ID="txtNombreEmpresa" runat="server" class="form-control" Placeholder="Nombre de la empresa" />
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnCrearEmpresa" runat="server" Text="Crear Empresa"  class="btn btn-success" OnClick="btnCrearEmpresa_Click" />
                    <asp:lab
                </div>
            </div>
        </div>
    

        <!-- Lista de Empresas -->
        <h4>Listado de empresas</h4>
        <asp:GridView ID="dgvEmpresas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered" OnRowDeleting="dgvEmpresas_RowDeleting" DataKeyNames="EmpresaID">
    <Columns>
        <asp:BoundField DataField="EmpresaID" HeaderText="ID" SortExpression="EmpresaID" HeaderStyle-CssClass="text-center" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HeaderStyle-CssClass="text-center" />
        <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creación" SortExpression="FechaCreacion" HeaderStyle-CssClass="text-center" />
        <asp:CheckBoxField DataField="Activa" HeaderText="Activa" SortExpression="Activa" HeaderStyle-CssClass="text-center" />
        <asp:CommandField ShowDeleteButton="True" HeaderText="Opciones" DeleteText="Eliminar" ButtonType="Button" HeaderStyle-CssClass="text-center" />
    </Columns>
</asp:GridView>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        
    </div>
</asp:Content>
