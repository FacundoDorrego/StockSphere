<%@ Page Title="Mi Cuenta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="StockSphere.Cuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex justify-content-center align-items-center min-vh-100">
        <div class="row justify-content-center w-100">

            <div class="col-md-6 col-lg-5">
                <div class="card shadow-lg">
                    <div class="card-header text-center">
                        <h4>Mis Datos</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-3">
                            <label for="lblNombre" class="form-label">Nombre de usuario:</label>
                            <asp:Label ID="lblNombre" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group mb-3">
                            <label for="lblCorreo" class="form-label">Correo Electrónico:</label>
                            <asp:Label ID="lblCorreo" runat="server" CssClass="form-control" />
                        </div>
                        <div id="divCambioContraseña" runat="server" visible="false">
                            <label for="lblContraseña" class="form-label">Introduzca la contraseña actual:</label>
                            <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control mx-auto" />

                            <label for="lblContraseñaNueva" class="form-label">Introduzca la contraseña nueva:</label>
                            <asp:TextBox ID="txtContraseñaNueva" runat="server" CssClass="form-control mx-auto" />
                             <div class="d-flex justify-content-center">
                            <asp:Button ID="btnAceptarCambioContra" runat="server" Text="Aceptar" CssClass="btn btn-success mx-2" OnClick="btnAceptarCambioContra_Click" onkeydown="return event.key != 'Enter';" />
                                 </div>
                        </div>
                        <div id="divEditar" runat="server" visible="false">
                            <label for="lblNombreEditar" runat="server" class="form-label">Introduzca el usuario nuevo:</label>
                            <asp:TextBox ID="txtNombreEditar" runat="server" CssClass="form-control mx-auto" />
                            <label for="lblCorreoEditar" runat="server" class="form-label">Introduzca el correo nuevo:</label>
                            <asp:TextBox ID="txtCorreoEditar" runat="server" CssClass="form-control mx-auto" />
                            <label for="lblConfirmacionContra" runat="server" class="form-label">Introduzca su contraseña para confirmar</label>
                            <asp:TextBox ID="txtConfirmacionContra" runat="server" CssClass="form-control mx-auto" />
                             <div class="d-flex justify-content-center" runat="server">
                                 <asp:Button ID="btnAceptarEditar" runat="server" Text="Aceptar" CssClass="btn btn-success mx-2" OnClick="btnAceptarEditar_Click" onkeydown="return event.key != 'Enter';" />
                           </div>
                        </div>
                    </div>
                        <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
                    <div class="card-footer text-center" runat="server"> 
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnEditar" runat="server" Text="Editar Datos" CssClass="btn btn-outline-primary mx-2" OnClick="btnEditar_Click" />
                            <asp:Button ID="btnCambiarContraseña" runat="server" Text="Cambiar contraseña" CssClass="btn btn-outline-primary mx-2" OnClick="btnCambiarContraseña_Click" />
                            <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-danger mx-2" OnClick="btnCerrarSesion_Click" />
                        </div>

                    </div>
                    
                </div>
            </div>

        </div>
    </div>
</asp:Content>
