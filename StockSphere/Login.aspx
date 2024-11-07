<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StockSphere.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container d-flex justify-content-center align-items-center min-vh-100">
        <div class="row justify-content-center w-100">
            
            <div class="col-md-6 col-lg-5">
                <div class="card shadow-lg">
                    <div class="card-header text-center">
                        <h4>Iniciar sesión</h4>
                    </div>
                    <div class="card-body text-center">
                        
                        <div class="form-group mb-4">
                            <label for="txtUsername" class="form-label">Correo electrónico</label>
                            <asp:TextBox ID="txtUsername" runat="server" class="form-control mx-auto" Placeholder="Correo electrónico" />
                        </div>
                        
                        <div class="form-group mb-4">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" class="form-control mx-auto" TextMode="Password" Placeholder="Contraseña" />
                        </div>
                     
                        <div class="d-flex justify-content-center card-body">
                            <asp:Button ID="btnLogin" runat="server" class="btn btn-primary" Text="Iniciar sesión" OnClick="btnLogin_Click" />
                        </div>

                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false" class="mt-3"></asp:Label>
                    </div>
                    <div class="card-footer text-center">
                        <small>¿No tienes cuenta? <a href="Register.aspx">Regístrate aquí</a></small>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
