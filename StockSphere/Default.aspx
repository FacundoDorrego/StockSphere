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
        </div>
        <div class="mt-4">
            <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar" CssClass="btn btn-primary" OnClick="btnIniciarSesion_Click" />
        </div>
    </div>
    <!-- Ver como poder implementar la tostada || No anda con asp:button, solo con button-->
    <button type="button" class="btn btn-primary" id="liveToastBtn">Show live toast</button>

        <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <div id="liveToast" class="toast hide" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
    <img src="Icono.png" class="rounded me-2" width="25" alt="...">
    <strong class="me-auto">StockSphere</strong>
    <small><%= DateTime.Now.ToString("HH:mm:ss") %></small>
    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
</div>
            <div class="toast-body">
                Probando la tostada de Bootstrap
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        // Activar el toast al hacer clic en el botón
        document.addEventListener('DOMContentLoaded', function () {
            const toastTrigger = document.getElementById('liveToastBtn');
            const toastLiveExample = document.getElementById('liveToast');

            if (toastTrigger) {
                toastTrigger.addEventListener('click', function () {
                    const toast = new bootstrap.Toast(toastLiveExample);
                    toast.show();
                });
            }
        });
    </script>
</asp:Content>
