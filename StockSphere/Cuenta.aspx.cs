using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorios;

namespace StockSphere
{
    public partial class Cuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Usuario usuario = (Usuario)Session["usuario"];
                lblNombre.Text = usuario.NombreUsuario;
                lblCorreo.Text = usuario.CorreoElectronico;

            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if(divEditar.Visible == false)
            {
                divEditar.Visible = true;
                lblMensaje.Visible = false;
            }
            else
            {
                lblMensaje.Visible = false;
                divEditar.Visible = false;
            }
            txtNombreEditar.Text = lblNombre.Text;
            txtCorreoEditar.Text = lblCorreo.Text;

        }

        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            if (divCambioContraseña.Visible == false)
            {
                divCambioContraseña.Visible = true;
                lblMensaje.Visible = false;
            }
            else
            {
                lblMensaje.Visible = false;
                divCambioContraseña.Visible = false;
            }

        }

        protected void btnAceptarCambioContra_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if(txtConfirmacionContra.Text == "" || txtContraseña.Text == "" || txtContraseñaNueva.Text == "")
                {
                    lblMensaje.Text = "Debe completar todos los campos";
                    lblMensaje.Visible = true;
                }
                if (txtContraseña.Text == usuario.Clave)
                {
                    RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
                    usuario.Clave = txtContraseñaNueva.Text;
                    repositorioUsuario.ModificarUsuario(usuario);
                    lblMensaje.Text = "Contraseña cambiada con éxito";
                    lblMensaje.CssClass = "alert alert-success";
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Text = "Contraseña actual incorrecta";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void btnAceptarEditar_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtNombreEditar.Text == "" || txtCorreoEditar.Text == "" || txtConfirmacionContra.Text=="")
                {
                    lblMensaje.Text = "Debe completar todos los campos";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }
                if (!Regex.IsMatch(txtCorreoEditar.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensaje.Text = "Por favor, ingrese un correo valido.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }

                else
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    if (txtConfirmacionContra.Text == usuario.Clave)
                    {
                        lblMensaje.Visible = false;
                        usuario.NombreUsuario = txtNombreEditar.Text;
                        usuario.CorreoElectronico = txtCorreoEditar.Text;
                        RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
                        repositorioUsuario.ModificarUsuario(usuario);
                        lblMensaje.Text = "Usuario modificado con éxito";
                        lblMensaje.CssClass = "alert alert-success";
                        lblMensaje.Visible = true;
                    }
                    else
                    {
                        lblMensaje.Text = "Contraseña incorrecta";
                        lblMensaje.Visible = true;
                        lblMensaje.CssClass = "alert alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }


        }
    }
}