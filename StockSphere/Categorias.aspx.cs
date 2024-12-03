using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorio;

namespace StockSphere
{
    public partial class Categorias : System.Web.UI.Page
    {
        public int empresaID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");

            }
            else
            {
                empresaID= Convert.ToInt32(Request.QueryString["empresaID"]);
                CargarCategorias(empresaID);
            }
        }

        private void CargarCategorias(int empresaID)
        {
            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            List<Categoria> categorias = repositorioCategoria.ObtenerCategorias();
            List<Categoria> categoriasEmpresa = new List<Categoria>();
            foreach (Categoria categoria in categorias)
            {
                if (categoria.EmpresaID == empresaID)
                {
                    categoriasEmpresa.Add(categoria);
                }
            }
            Usuario usuario = (Usuario)Session["usuario"];
            Empresa empresaSelec = (Empresa)Session["empresaSelec"];
            if (categoriasEmpresa.Count == 0 || categoriasEmpresa == null)
            {
                lblMensaje.Text = "No hay categorias para esta empresa";
                lblMensaje.Visible = true;
                dgvCategorias.Visible = false;
            }
            else if (empresaSelec == null || empresaSelec.UsuarioID != usuario.UsuarioID)
            {
                Response.Redirect("AdminEmpresas.aspx");

            }
            else
            {
                dgvCategorias.DataSource = categoriasEmpresa;
                dgvCategorias.DataBind();
                dgvCategorias.Visible = true;
                lblMensaje.Visible = false;
            }
        }
        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtDescCategoria.Text == ""||txtNombreCategoria.Text=="")
                {
                    lblMensaje.Text = "Debe completar todos los campos";
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Visible = false;
                    RepositorioCategoria repoCategoria = new RepositorioCategoria();
                    Categoria auxCategoria = new Categoria();
                    auxCategoria.Nombre = txtNombreCategoria.Text;
                    auxCategoria.Descripcion = txtDescCategoria.Text;
                    auxCategoria.EmpresaID = empresaID;
                    repoCategoria.AgregarCategoria(auxCategoria);

                }
            } 

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
            finally
            {
                CargarCategorias(empresaID);
            }
        }

        protected void btnActualizarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescCatActualizar.Text) || string.IsNullOrEmpty(txtNombreCatActualizar.Text))
                {
                    lblMensaje.Text = "Debe completar todos los campos.";
                    lblMensaje.Visible = true;
                    return;
                }
                else
                {
                    int categoriaID = Convert.ToInt32(hiddenCategoriaID.Value);
                    lblMensaje.Visible = false;
                    RepositorioCategoria repoCategoria = new RepositorioCategoria();
                    Categoria auxCategoria = new Categoria
                    {
                        CategoriaID = categoriaID,
                        Nombre = txtNombreCatActualizar.Text,
                        Descripcion = txtDescCatActualizar.Text,
                        EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"])
                    };
                    repoCategoria.ActualizarCategoria(auxCategoria);
                    lblMensaje.Text = "¡Actualizado!";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
            finally
            {
                CargarCategorias(empresaID);
            }
        }

        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void dgvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int categoriaID = Convert.ToInt32(dgvCategorias.DataKeys[e.NewEditIndex].Value);
            txtNombreCatActualizar.Text = dgvCategorias.Rows[e.NewEditIndex].Cells[1].Text;
            txtDescCatActualizar.Text = dgvCategorias.Rows[e.NewEditIndex].Cells[2].Text;
            hiddenCategoriaID.Value = categoriaID.ToString();
            dgvCategorias.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarActualizar", "mostrarFormulario('divActualizarCategoria');", true);

        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int categoriaID = Convert.ToInt32(hiddenCategoriaIDEliminar.Value);
            try
            {
                RepositorioCategoria repoCategoria = new RepositorioCategoria();
                repoCategoria.EliminarCategoria(categoriaID);
                lblMensaje.Text = "¡Eliminado!";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
            finally
            {
                CargarCategorias(empresaID);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaID);
        }
    }
}