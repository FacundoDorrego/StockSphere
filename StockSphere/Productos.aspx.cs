using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorio;

namespace StockSphere
{
    public partial class Productos : System.Web.UI.Page
    {
        private int empresaID;

        // Cargar los productos de la empresa al cargar la página
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
                    CargarProductos();
                    CargarCategorias();
                    dgvProductos.Visible = true;
                    dgvProductos.EditIndex = -1;
                }
            }
        }


        private void CargarCategorias()
        {
            try
            {
                RepositorioCategoria repoCategoria = new RepositorioCategoria();
                List<Categoria> categorias = repoCategoria.ObtenerCategorias();
                ddlCategoriaProducto.DataSource = categorias;
                ddlCategoriaProducto.DataTextField = "Nombre";
                ddlCategoriaProducto.DataValueField = "CategoriaID";
                ddlCategoriaProducto.DataBind();
                ddlCategoriaProductoActualizar.DataSource = categorias;
                ddlCategoriaProductoActualizar.DataTextField = "Nombre";
                ddlCategoriaProductoActualizar.DataValueField = "CategoriaID";
                ddlCategoriaProductoActualizar.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al cargar las categorías." + ex;
            }
        }

        // Cargar productos de la empresa específica
        private void CargarProductos()
        {
            int empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            try
            {
                RepositorioProducto repoProducto = new RepositorioProducto();
                List<Producto> productos = repoProducto.ObtenerProductos();
                List<Producto> productosxEmpresa = new List<Producto>();

                foreach (Producto producto in productos)
                {
                    if (producto.EmpresaID == empresaID)
                    {
                        productosxEmpresa.Add(producto);
                    }
                }
                if (productosxEmpresa.Count != 0)
                {

                    dgvProductos.DataSource = productosxEmpresa;
                    dgvProductos.DataBind();
                    dgvProductos.Visible = true;
                    dgvProductos.EditIndex = -1;
                }
                else
                {
                    lblMensaje.Text = "No hay productos para mostrar.";
                }

            }
            catch (Exception ex)
            {
                dgvProductos.EditIndex = -1;
                lblMensaje.Text = "Hubo un error al cargar los productos." + ex;
                dgvProductos.Visible = false;
            }
        }

        // Evento para agregar un nuevo producto
        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {

                string nombre = txtNombreProducto.Text;
                string descripcion = txtDescripcionProducto.Text;
                decimal precio = decimal.Parse(txtPrecioProducto.Text);
                int stock = int.Parse(txtStockProducto.Text);
                int categoriaID = int.Parse(ddlCategoriaProducto.SelectedValue);

                Producto nuevoProducto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,
                    CategoriaID = categoriaID,
                    EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"])
                };
                RepositorioProducto repoProducto = new RepositorioProducto();
                repoProducto.AgregarProducto(nuevoProducto);
                lblMensaje.Text = "Producto agregado correctamente.";
                CargarProductos();
            }
            catch (Exception ex)


            {
                lblMensaje.Text = "Hubo un error al agregar el producto." + ex;
            }
            dgvProductos.EditIndex = -1;
            // Limpiar los campos del formulario
            txtNombreProducto.Text = "";
            txtDescripcionProducto.Text = "";
            txtPrecioProducto.Text = "";
            txtStockProducto.Text = "";
        }

        // Evento para actualizar un producto (editar)
        protected void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            try
            {

                int productoID = Convert.ToInt32(hiddenProductoIDEliminar.Value);
                string nombre = txtNombreProductoActualizar.Text;
                string descripcion = txtDescripcionProductoActualizar.Text;
                decimal precio = decimal.Parse(txtPrecioProductoActualizar.Text);
                int stock = int.Parse(txtStockProductoActualizar.Text);
                int categoriaID = int.Parse(ddlCategoriaProductoActualizar.SelectedValue);

                Producto productoEditado = new Producto
                {
                    ProductoID = productoID,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,
                    CategoriaID = categoriaID,
                    EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"])
                };

                RepositorioProducto repoProducto = new RepositorioProducto();
                repoProducto.ActualizarProducto(productoEditado);



                lblMensaje.Text = "Producto actualizado correctamente.";
                CargarProductos();
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al actualizar el producto." + ex;
            }

            dgvProductos.EditIndex = -1;
            // Limpiar los campos del formulario
            txtNombreProductoActualizar.Text = "";
            txtDescripcionProductoActualizar.Text = "";
            txtPrecioProductoActualizar.Text = "";
            txtStockProductoActualizar.Text = "";
        }

        // Evento para eliminar un producto
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                int productoID = Convert.ToInt32(hiddenProductoIDEliminar.Value);


                RepositorioProducto repoProducto = new RepositorioProducto();
                repoProducto.EliminarProducto(productoID);


                lblMensaje.Text = "Producto eliminado correctamente.";
                repoProducto.MovimientoInventario(productoID, 0, "Eliminar", "Producto eliminado");
                CargarProductos();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al eliminar el producto." + ex;
            }

        }

        // Evento para seleccionar un producto para editar
        protected void dgvProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int productoID = Convert.ToInt32(dgvProductos.DataKeys[e.NewEditIndex].Value);

            // Cargar los datos del producto en el formulario de edición
            txtNombreProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[1].Text;
            txtDescripcionProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[2].Text;
            txtPrecioProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[3].Text;
            txtStockProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[4].Text;
            ddlCategoriaProductoActualizar.SelectedValue = dgvProductos.Rows[e.NewEditIndex].Cells[5].Text;
            hiddenProductoIDEliminar.Value = productoID.ToString();
            dgvProductos.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarActualizar", "mostrarFormulario('divActualizarProducto');", true);
            CargarCategorias();

        }

        // Evento para eliminar un producto
        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int productoID = Convert.ToInt32(e.CommandArgument);

                abrirModalEliminar(productoID);
            }
        }

        // Función para abrir el modal de eliminación
        private void abrirModalEliminar(int productoID)
        {
            hiddenProductoIDEliminar.Value = productoID.ToString();

            var modal = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            modal.InnerHtml = "<script>$('#modalConfirmarEliminar').modal('show');</script>";
            Page.Controls.Add(modal);
        }

        protected void btnMostrarAgregar_Click(object sender, EventArgs e)
        {
            CargarCategorias();
        }
    }
}
