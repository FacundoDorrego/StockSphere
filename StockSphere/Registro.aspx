<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="StockSphere.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container d-flex justify-content-center align-items-center min-vh-100">
        <div class="row justify-content-center w-100">

            <div class="col-md-6 col-lg-5">
                <div class="card shadow-lg">
                    <div class="card-header text-center">
                        <h4>Registrarse</h4>
                    </div>
                    <div class="card-body text-center">

                        <div class="form-group mb-4">
                            <label for="txtNombre" class="form-label">Nombre de usuario</label>
                            <asp:TextBox ID="txtNombre" runat="server" class="form-control mx-auto" Placeholder="Nombre de usuario" />
                        </div>

                        <div class="form-group mb-4">
                            <label for="txtUsername" class="form-label">Correo electrónico</label>
                            <asp:TextBox ID="txtCorreoElectronico" runat="server" class="form-control mx-auto" Placeholder="Ejemplo@gmail.com" />
                        </div>

                        <div class="form-group mb-4">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" class="form-control mx-auto" TextMode="Password" Placeholder="Contraseña" />
                        </div>

                        <div class="d-flex justify-content-center card-body">
                            <asp:Button ID="btnRegistrar" runat="server" class="btn btn-primary" Text="Registrarse" OnClick="btnRegistrar_Click" />
                        </div>
                        <asp:Label ID="lblMensaje" runat="server" Visible="false" class="mt-3"></asp:Label>
                    </div>
                    <div id="divRedireccion" class="card-footer text-center" runat="server" visible="false">
                        <small id="lblRedireccion" runat="server" visible="false">Ya puedes loguearte. <a href="Login.aspx">Click aqui para redirigirte</a></small>
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
