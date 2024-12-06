using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockSphere
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                lnkLogin.Visible = false;
                btnCerrar.Visible = true;
                
                var usuario = (Usuario)Session["usuario"];
                if (usuario != null && usuario.RolID == 1)
                {
                    phAdminMenu.Visible = true; 
                }
                else
                {
                    phAdminMenu.Visible = false; 
                }
            }
            else
            {
                btnCerrar.Visible = false;
                lnkLogin.Visible = true;
                phAdminMenu.Visible = false; 
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }
    }
}