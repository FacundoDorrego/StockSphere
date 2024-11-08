<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StockSphere._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center mt-5">
        <div class="card shadow-lg p-5">
            <!-- Espacio para el ícono -->
            <div class="icon-container mb-4">
                <asp:Image ID="imgIcono" runat="server" ImageUrl="~/Icono.png" CssClass="img-fluid" />
            </div>

            <!-- Título principal -->
            <h1 class="display-4">StockSphere</h1>
            <h2 class="display-4">Gestion de inventario</h2>
            <p class="lead">Tu stock bajo una misma esfera</p>
        </div>
        <!-- Botón de iniciar sesión -->
        <div class="mt-4">
            <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar sesión" CssClass="btn btn-primary" OnClick="btnIniciarSesion_Click" />
        </div>
    </div>
</asp:Content>
