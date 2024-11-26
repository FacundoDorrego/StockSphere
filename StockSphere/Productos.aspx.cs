using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorio;
using Repositorios;

namespace StockSphere
{
    public partial class Productos : System.Web.UI.Page
    {
        private int empresaID;
        private int usuarioID;

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
                    Usuario usuario = (Usuario)Session["usuario"];
                    usuarioID = usuario.UsuarioID;
                    CargarProductos();
                    CargarMovimientos();
                    CargarProveedores();
                    CargarCategorias();
                    dgvProductos.Visible = false;
                    dgvMovimientos.Visible = false;
                    dgvProductos.EditIndex = -1;
                }
            }
        }

        private void CargarMovimientos()
        {
            int empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            try
            {
                RepositorioMovimientoInventario repomov = new RepositorioMovimientoInventario();
                List<MovimientoInventario> movimientoInventarios = repomov.ObtenerMovimientos();
                List<MovimientoInventario> movimientosxEmpresa = new List<MovimientoInventario>();

                foreach (MovimientoInventario movimiento in movimientoInventarios)
                {
                    if (movimiento.EmpresaID == empresaID)
                    {
                        movimientosxEmpresa.Add(movimiento);
                    }
                }
                if (movimientosxEmpresa.Count != 0)
                {

                    dgvMovimientos.DataSource = movimientosxEmpresa;
                    dgvMovimientos.DataBind();
                }
                else
                {
                    lblMensaje.Text = "No hay Movimientos para mostrar.";
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al cargar los Movimientos." + ex;
                dgvMovimientos.Visible = false;
            }
        }
        private void CargarCategorias()
        {
            try
            {
                //Agregar que las categorias se relaciones mediante el ID de la empresa
                RepositorioCategoria repoCategoria = new RepositorioCategoria();
                List<Categoria> categorias = repoCategoria.ObtenerCategorias();
                List<Categoria> categoriasEmpresa = new List<Categoria>();
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

        private void CargarProveedores()
        {
            try
            {
                int empresaIDprov = Convert.ToInt32(Request.QueryString["empresaID"]);
                RepositorioProveedor repoProveedor = new RepositorioProveedor();
                List<Proveedor> proveedores = repoProveedor.ObtenerProveedoresxEmpresa(empresaIDprov);
                if (proveedores.Count == 0)
                {
                    ddlProveedor.DataSource = null;
                    ddlProveedor.Items.Insert(0, new ListItem("No hay proveedores", "0"));
                    ddlProveedor.DataBind();
                    return;
                }
                ddlProveedor.DataSource = proveedores;
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "ProveedorID";
                ddlProveedor.DataBind();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al cargar los proveedores." + ex;
            }
        }

        private void CargarProveedoresActualizar()
        {
            try
            {
                int empresaIDAct = Convert.ToInt32(Request.QueryString["empresaID"]);
                RepositorioProveedor repoProveedor = new RepositorioProveedor();
                List<Proveedor> proveedores = repoProveedor.ObtenerProveedoresxEmpresa(empresaIDAct);
                if (proveedores.Count == 0)
                {
                    ddlProveedorActualizar.DataSource = null;
                    ddlProveedorActualizar.Items.Insert(0, new ListItem("No hay proveedores", "0"));
                    ddlProveedorActualizar.DataBind();
                    return;
                }
                ddlProveedorActualizar.DataSource = proveedores;
                ddlProveedorActualizar.DataTextField = "Nombre";
                ddlProveedorActualizar.DataValueField = "ProveedorID";
                ddlProveedorActualizar.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al cargar los proveedores." + ex;
            }
        }
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


        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {

                string nombre = txtNombreProducto.Text;
                string descripcion = txtDescripcionProducto.Text;
                decimal precio = decimal.Parse(txtPrecioProducto.Text);
                int stock = int.Parse(txtStockProducto.Text);
                int categoriaID = int.Parse(ddlCategoriaProducto.SelectedValue);
                int proveedorID = int.Parse(ddlProveedor.SelectedValue);

                Producto nuevoProducto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,
                    CategoriaID = categoriaID,
                    ProveedorID = proveedorID,
                    EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"]
                    )
                };
                RepositorioProducto repoProducto = new RepositorioProducto();
                repoProducto.AgregarProducto(nuevoProducto);
                lblMensaje.Text = "Producto agregado correctamente.";

                DateTime fechamov = DateTime.Now;
                int ultimoid = repoProducto.ObtenerUltimoIdProducto();
                int empresaIDAgregar = Convert.ToInt32(Request.QueryString["empresaID"]);
                Usuario usuario = (Usuario)Session["usuario"];
                int usuarioIDAgregar = usuario.UsuarioID;
                RepositorioMovimientoInventario repoMovInv = new RepositorioMovimientoInventario();
                repoMovInv.MovimientoInventario(ultimoid, stock, "Agregar", "Producto agregado", fechamov, usuarioIDAgregar, empresaIDAgregar);
                CargarProductos();
                dgvProductos.DataBind();
                CargarMovimientos();
                dgvMovimientos.DataBind();
                CargarProveedores();
                CargarCategorias();
                txtNombreProducto.Text = "";
                txtDescripcionProducto.Text = "";
                txtPrecioProducto.Text = "";
                txtStockProducto.Text = "";
            }
            catch (Exception ex)


            {
                lblMensaje.Text = "Hubo un error al agregar el producto." + ex;
            }
        }


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
                int proveedorID = int.Parse(ddlProveedorActualizar.SelectedValue);
                Producto productoEditado = new Producto
                {
                    ProductoID = productoID,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,
                    CategoriaID = categoriaID,
                    ProveedorID = proveedorID,
                    EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"])
                };

                RepositorioProducto repoProducto = new RepositorioProducto();
                repoProducto.ActualizarProducto(productoEditado);
                DateTime fechamov = DateTime.Now;
                RepositorioMovimientoInventario repoMovInv = new RepositorioMovimientoInventario();
                int empresaIDMod = Convert.ToInt32(Request.QueryString["empresaID"]);
                Usuario usuario = (Usuario)Session["usuario"];
                int usuarioIDMod = usuario.UsuarioID;
                repoMovInv.MovimientoInventario(productoID, stock, "Actualizar", "Producto actualizado", fechamov, usuarioIDMod, empresaIDMod);
                dgvProductos.EditIndex = -1;
                CargarProductos();
                dgvProductos.DataBind();
                CargarMovimientos();
                dgvMovimientos.DataBind();
                CargarCategorias();
                CargarProveedoresActualizar();
                txtNombreProductoActualizar.Text = "";
                txtDescripcionProductoActualizar.Text = "";
                txtPrecioProductoActualizar.Text = "";
                txtStockProductoActualizar.Text = "";
                lblMensaje.Text = "Producto actualizado correctamente.";


            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al actualizar el producto." + ex;
            }
        }





        protected void dgvProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int productoID = Convert.ToInt32(dgvProductos.DataKeys[e.NewEditIndex].Value);

            txtNombreProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[1].Text;
            txtDescripcionProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[2].Text;
            txtPrecioProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[3].Text;
            txtStockProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[4].Text;
            ddlCategoriaProductoActualizar.SelectedValue = dgvProductos.Rows[e.NewEditIndex].Cells[5].Text;
            ddlProveedorActualizar.SelectedValue = dgvProductos.Rows[e.NewEditIndex].Cells[6].Text;
            hiddenProductoIDEliminar.Value = productoID.ToString();
            dgvProductos.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarActualizar", "mostrarFormulario('divActualizarProducto');", true);
            CargarCategorias();
            CargarProductos();
            dgvProductos.DataBind();
            CargarMovimientos();
            dgvMovimientos.DataBind();
            CargarProveedoresActualizar();

        }
        protected void btnMostrarAgregar_Click(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarProveedores();
        }

        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {

                string[] args = e.CommandArgument.ToString().Split(',');

                if (args.Length == 2)
                {
                    int productoID = Convert.ToInt32(args[0]);
                    int stock = Convert.ToInt32(args[1]);

                    Debug.WriteLine($"CommandArgument recibido: ProductoID = {productoID}, Stock = {stock}");


                    RepositorioProducto repoProducto = new RepositorioProducto();
                    repoProducto.EliminarProducto(productoID);
                    int stockNegativo = stock * -1;

                    int empresaIDEliminar = Convert.ToInt32(Request.QueryString["empresaID"]);
                    Usuario usuario = (Usuario)Session["usuario"];
                    int usuarioIDEliminar = usuario.UsuarioID;
                    DateTime fechamov = DateTime.Now;
                    RepositorioMovimientoInventario repoMovInv = new RepositorioMovimientoInventario();
                    repoMovInv.MovimientoInventario(productoID, stockNegativo, "Eliminar", "Producto eliminado", fechamov, usuarioIDEliminar, empresaIDEliminar);
                    CargarProductos();
                    dgvProductos.DataBind();
                    CargarMovimientos();
                    CargarProveedores();
                    CargarCategorias();
                    lblMensaje.Text = "Producto eliminado correctamente.";
                }
                else
                {
                    lblMensaje.Text = "Error al eliminar el producto.";
                }
            }
        }

        protected void btnMostrarListado_Click(object sender, EventArgs e)
        {
            if (dgvProductos.Visible == true)
            {
                listprod.Visible = false;
                dgvProductos.Visible = false;
            }
            else
            {
                listprod.Visible = true;
                dgvProductos.Visible = true;
            }
            
        }

        protected void btnMostrarMovimientos_Click(object sender, EventArgs e)
        {
            if (dgvMovimientos.Visible == true)
            {
                movstock.Visible = false;
                dgvMovimientos.Visible = false;
            }
            else
            {
                movstock.Visible = true;
                dgvMovimientos.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            int empresaIDVolver = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaIDVolver);
        }
    }
}

