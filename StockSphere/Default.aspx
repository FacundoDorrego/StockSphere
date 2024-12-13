<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StockSphere._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center mt-5">
        <div class="card shadow-lg p-5">
            <div class="icon-container mb-4">
                <asp:Image ID="imgIcono" runat="server" ImageUrl="~/Icono.png" CssClass="img-fluid" />
            </div>
            <h1 class="display-4">StockSphere</h1>
            <h2 class="display-4">Gestion de inventario</h2>
            <p class="lead">Tu stock bajo una misma esfera</p>
            <div class="mt-4">
                <a href="Login.aspx" class="btn btn-outline-info">Entrar</a>
            </div>
        </div>
        <div class="card mt-4">
            <div class="card-body">
                <blockquote class="blockquote mb-0">
                    <p>Un programa adaptado a vos y a tus necesidades.</p>
                    <footer class="blockquote-footer">StockSphere <cite title="Source Title">2024</cite></footer>
                </blockquote>
            </div>
        </div>
    </div>

</asp:Content>
