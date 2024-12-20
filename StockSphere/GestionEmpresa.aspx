﻿<%@ Page Title="GestionEmpresa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionEmpresa.aspx.cs" Inherits="StockSphere.VerDetalleEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg">
            <div class="card-header bg-primary text-white text-center">
                <h2>Gestion de la empresa</h2>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <h5 class="card-title">ID de la Empresa:</h5>
                    <p class="card-text">
                        <asp:Label ID="lblEmpresaID" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div class="mb-3">
                    <h5 class="card-title">Nombre de la Empresa:</h5>
                    <p class="card-text">
                        <asp:Label ID="lblEmpresaNombre" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div class="mb-3">
                    <h5 class="card-title">Fecha de Creación:</h5>
                    <p class="card-text">
                        <asp:Label ID="lblEmpresaFechaCreacion" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div id="divActivo" class="mb-3" runat="server" >
                    <h5 class="card-title">Activo:</h5>
                    <p class="card-text">
                        <asp:Label ID="lblActivo" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
            <div class="card-footer text-center">
                <asp:Button ID="btnProductos" runat="server" Text="Gestion de productos" CssClass="btn btn-outline-dark mx-2" OnClick="btnProductos_Click" />
                <asp:Button ID="btnGestionEmpleados" runat="server" Text="Gestion de empleados" CssClass="btn btn-outline-dark mx-2" OnClick="btnGestionEmpleados_Click" />
                <asp:Button ID="btnProveedores" runat="server" Text="Gestion de proveedores" CssClass="btn btn-outline-dark mx-2" OnClick="btnProveedores_Click" />
                <asp:Button ID="btnCategorias" runat="server" Text="Gestion de categorias" CssClass="btn btn-outline-dark mx-2" OnClick="btnCategorias_Click" />
                <asp:Button ID="btnDashboardReportes" runat="server" Text="Dashboard y reportes" CssClass="btn btn-outline-info mx-2" OnClick="btnDashboardReportes_Click" />
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" OnClick="btnRegresar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
