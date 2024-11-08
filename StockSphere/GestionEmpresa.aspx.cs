﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorio;
namespace StockSphere
{
    public partial class VerDetalleEmpresa : System.Web.UI.Page
    {
        private RepositorioEmpresa repositorioEmpresa = new RepositorioEmpresa();
        private int empresaID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);

                // Cargar los detalles de la empresa

                CargarDetallesEmpresa(empresaID);
            }
        }
        private void CargarDetallesEmpresa(int empresaID)
        {
            if (empresaID == 0)
            {
                Response.Redirect("AdminEmpresas.aspx");
            }
            Usuario usuario = (Usuario)Session["usuario"];

            var empresaSelec = repositorioEmpresa.ObtenerEmpresa(empresaID);
            if (empresaSelec == null || empresaSelec.UsuarioID != usuario.UsuarioID)
            {
                Response.Redirect("AdminEmpresas.aspx");
            }
            else
            {
                lblEmpresaID.Text = empresaSelec.EmpresaID.ToString();
                lblActivo.Text = empresaSelec.Activa ? "Sí" : "No";
                lblEmpresaNombre.Text = empresaSelec.Nombre;
                lblEmpresaFechaCreacion.Text = empresaSelec.FechaCreacion.ToString("dd/MM/yyyy");
                Session.Add("empresaSelec", empresaSelec);
            }

           
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEmpresas.aspx");
        }

        protected void btnProveedores_Click(object sender, EventArgs e)
        {

            Response.Redirect("Proveedores.aspx?empresaID=" + lblEmpresaID.Text);

        }
    }



}