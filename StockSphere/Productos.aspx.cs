using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;
using Repositorios;

namespace StockSphere
{
    public partial class Productos : System.Web.UI.Page
    {
        private int empresaID;
        private int usuarioID;
        private int usuarioRolID;
        private RepositorioProducto repoProducto = new RepositorioProducto();
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
                    Usuario usuario = (Usuario)Session["usuario"];

                    if (usuario.RolID == 3)
                    {
                        Empleado empleado = (Empleado)Session["empleado"];
                        usuarioID = empleado.Usuario.UsuarioID;
                        usuarioRolID = empleado.Usuario.RolID;
                        OcultarBotonesEmpleados();
                    }
                    else
                    {
                        usuarioID = usuario.UsuarioID;
                        usuarioRolID = usuario.RolID;
                    }
                    empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
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
        protected void OcultarBotonesEmpleados()
        {
            Empleado empleado = (Empleado)Session["empleado"];
            if (empleado.Usuario.RolID == 3)
            {
                btnActualizarProducto.Visible = false;
                btnMostrarMovimientos.Visible = false;
                dgvProductos.Columns[9].Visible = false;
            }
        }
        private void CargarDivs()
        {
            CargarProductos();
            CargarMovimientos();
            CargarProveedores();
            CargarCategorias();
            CargarProveedoresActualizar();
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
                RepositorioCategoria repoCategoria = new RepositorioCategoria();
                List<Categoria> categorias = repoCategoria.ObtenerCategorias();
                List<Categoria> categoriasEmpresa = new List<Categoria>();
                foreach (Categoria categoria in categorias)
                {
                    if (categoria.EmpresaID == Convert.ToInt32(Request.QueryString["empresaID"]))
                    {
                        categoriasEmpresa.Add(categoria);
                    }
                }
                ddlCategoriaProducto.DataSource = categoriasEmpresa;
                ddlCategoriaProducto.DataTextField = "Nombre";
                ddlCategoriaProducto.DataValueField = "CategoriaID";
                ddlCategoriaProducto.DataBind();
                ddlCategoriaProductoActualizar.DataSource = categoriasEmpresa;
                ddlCategoriaProductoActualizar.DataTextField = "Nombre";
                ddlCategoriaProductoActualizar.DataValueField = "CategoriaID";
                ddlCategoriaProductoActualizar.DataBind();
                ddlCategoriasFiltro.DataSource = categoriasEmpresa;
                ddlCategoriasFiltro.DataTextField = "Nombre";
                ddlCategoriasFiltro.DataValueField = "CategoriaID";
                ddlCategoriasFiltro.DataBind();
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
                ddlProveedoresFiltro.DataSource = proveedores;
                ddlProveedoresFiltro.DataTextField = "Nombre";
                ddlProveedoresFiltro.DataValueField = "ProveedorID";
                ddlProveedoresFiltro.DataBind();

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
            Empresa empresaSelec = (Empresa)Session["empresaSelec"];
            try
            {
                List<Producto> productos = repoProducto.ObtenerProductos();
                List<Producto> productosxEmpresa = new List<Producto>();
                ddlProductos.Items.Clear();
                ddlProductosVenta.Items.Clear();

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


                if (dgvProductos.DataSource != null)
                {
                    if (usuarioRolID == 1)
                    {

                        if (empresaSelec == null || empresaSelec.UsuarioID != usuarioID)
                        {
                            Response.Redirect("AdminEmpresas.aspx");

                        }
                    }
                    ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
                    ddlProductos.DataSource = dgvProductos.DataSource;
                    ddlProductos.DataTextField = "Nombre";
                    ddlProductos.DataValueField = "ProductoID";
                    ddlProductos.DataBind();
                    ddlProductosVenta.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
                    ddlProductosVenta.DataSource = dgvProductos.DataSource;
                    ddlProductosVenta.DataTextField = "Nombre";
                    ddlProductosVenta.DataValueField = "ProductoID";
                    ddlProductosVenta.DataBind();
                }
                else
                {
                    ddlProductos.Items.Add(new ListItem("No hay productos disponibles", "0"));
                    ddlProductosVenta.Items.Add(new ListItem("No hay productos disponibles", "0"));
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
                string marca = txtMarca.Text;
                Producto nuevoProducto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,
                    CategoriaID = categoriaID,
                    ProveedorID = proveedorID,
                    EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"]),
                    Marca = txtMarca.Text
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
                CargarDivs();
                dgvProductos.DataBind();
                dgvMovimientos.DataBind();
                txtNombreProducto.Text = "";
                txtDescripcionProducto.Text = "";
                txtPrecioProducto.Text = "";
                txtStockProducto.Text = "";
                txtMarca.Text = "";
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
                string marca = txtMarcaActualizar.Text;
                Producto productoEditado = new Producto
                {
                    ProductoID = productoID,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock,
                    CategoriaID = categoriaID,
                    ProveedorID = proveedorID,
                    EmpresaID = Convert.ToInt32(Request.QueryString["empresaID"]),
                    Marca = txtMarcaActualizar.Text
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
                CargarDivs();
                dgvProductos.DataBind();
                dgvMovimientos.DataBind();
                txtNombreProductoActualizar.Text = "";
                txtDescripcionProductoActualizar.Text = "";
                txtPrecioProductoActualizar.Text = "";
                txtStockProductoActualizar.Text = "";
                txtMarcaActualizar.Text = "";
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
            txtMarcaActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[2].Text;
            txtDescripcionProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[3].Text;
            txtPrecioProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[4].Text;
            txtStockProductoActualizar.Text = dgvProductos.Rows[e.NewEditIndex].Cells[5].Text;
            ddlCategoriaProductoActualizar.SelectedValue = dgvProductos.Rows[e.NewEditIndex].Cells[6].Text;
            ddlProveedorActualizar.SelectedValue = dgvProductos.Rows[e.NewEditIndex].Cells[7].Text;
            hiddenProductoIDEliminar.Value = productoID.ToString();
            dgvProductos.EditIndex = -1;
            ClientScript.RegisterStartupScript(this.GetType(), "MostrarActualizar", "mostrarFormulario('divActualizarProducto');", true);
            CargarDivs();
            dgvProductos.DataBind();
            dgvMovimientos.DataBind();

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
                    repoProducto.EliminarProducto(productoID);
                    int stockNegativo = stock * -1;
                    int empresaIDEliminar = Convert.ToInt32(Request.QueryString["empresaID"]);
                    Usuario usuario = (Usuario)Session["usuario"];
                    int usuarioIDEliminar = usuario.UsuarioID;
                    DateTime fechamov = DateTime.Now;
                    RepositorioMovimientoInventario repoMovInv = new RepositorioMovimientoInventario();
                    repoMovInv.MovimientoInventario(productoID, stockNegativo, "Eliminar", "Producto eliminado", fechamov, usuarioIDEliminar, empresaIDEliminar);
                    CargarDivs();
                    dgvProductos.DataBind();
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
                divFiltros.Visible = false;
                listprod.Visible = false;
                dgvProductos.Visible = false;
            }
            else
            {
                divFiltros.Visible = true;
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
                divFiltrosMovimientos.Visible = false;
            }
            else
            {
                movstock.Visible = true;
                dgvMovimientos.Visible = true;
                divFiltrosMovimientos.Visible = true;
                txtFechaFiltro.Text= DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            int empresaIDVolver = Convert.ToInt32(Request.QueryString["empresaID"]);
            Response.Redirect("GestionEmpresa.aspx?empresaID=" + empresaIDVolver);
        }





        protected void btnConfirmarAgregarStock_Click(object sender, EventArgs e)
        {
            try
            {

                string productoID = ddlProductos.SelectedValue;
                string nombreProducto = ddlProductos.SelectedItem.Text;
                if (string.IsNullOrEmpty(txtCantidad.Text))
                {
                    lblMensaje.Text = "Por favor, ingrese una cantidad válida.";
                    return;
                }
                int cantidadIngresada = int.Parse(txtCantidad.Text);

                if (productoID != "0")
                {
                    if (cantidadIngresada <= 0)
                    {
                        lblMensaje.Text = "Por favor, ingrese una cantidad válida.";
                        return;
                    }
                    else
                    {

                        RepositorioMovimientoInventario repoMovInv = new RepositorioMovimientoInventario();
                        int stockAnterior = repoProducto.ObtenerStock(int.Parse(productoID));
                        int stockActual = stockAnterior + cantidadIngresada;
                        repoProducto.ActualizarStock(int.Parse(productoID), stockActual);
                        int empresaIDStock = Convert.ToInt32(Request.QueryString["empresaID"]);
                        Usuario usuario = (Usuario)Session["usuario"];
                        int usuarioIDStock = usuario.UsuarioID;
                        DateTime fechamov = DateTime.Now;
                        repoMovInv.MovimientoInventario(int.Parse(productoID), cantidadIngresada, "Ingreso de Stock", "Stock agregado", fechamov, usuarioIDStock, empresaIDStock);
                        lblResultado.Text = $"Producto seleccionado: {nombreProducto} (ID: {productoID}) (Stock Anterior: {stockAnterior}) (Stock Actual: {stockActual})";
                        lblMensaje.Text = "Stock Agregado con exito";


                        CargarDivs();
                        dgvProductos.DataBind();
                        dgvMovimientos.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al agregar el stock." + ex;
            }
        }

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                string productoID = ddlProductosVenta.SelectedValue;
                string nombreProducto = ddlProductosVenta.SelectedItem.Text;
                if (string.IsNullOrEmpty(txtCantidadVenta.Text))
                {
                    lblMensaje.Text = "Por favor, ingrese una cantidad válida.";
                    return;
                }
                int cantidadVendida = int.Parse(txtCantidadVenta.Text);
                if (productoID != "0")
                {
                    if (cantidadVendida <= 0)
                    {
                        lblMensaje.Text = "Por favor, ingrese una cantidad válida.";
                        return;
                    }
                    else
                    {
                        if(usuarioRolID==3)
                        {
                            lblMensaje.Text = "No tiene permisos para realizar ventas.";
                            return;
                        }
                        RepositorioProducto repoProducto = new RepositorioProducto();
                        RepositorioMovimientoInventario repoMovInv = new RepositorioMovimientoInventario();
                        RepositorioVenta repoVenta = new RepositorioVenta();
                        RepositorioCategoria repoCategoria = new RepositorioCategoria();
                        RepositorioUsuario repoUsuario = new RepositorioUsuario();
                        RepositorioEmpresa repoEmpresa = new RepositorioEmpresa();
                        Usuario usuarioVenta = (Usuario)Session["usuario"];
                        Producto productoSeleccionado = repoProducto.ObtenerProductoxID(int.Parse(productoID));
                        Empresa empresaVenta = repoEmpresa.ObtenerEmpresaxID(productoSeleccionado.EmpresaID);
                        Categoria categoriaVenta = repoCategoria.ObtenerCategoriaxID(productoSeleccionado.CategoriaID);
                        int stockAnterior = productoSeleccionado.Stock;
                        if (stockAnterior < cantidadVendida)
                        {
                            lblMensaje.Text = "No hay suficiente stock para realizar la venta.";
                            return;
                        }
                        int stockActual = stockAnterior - cantidadVendida;
                        int stockVendido = cantidadVendida * -1;
                        repoProducto.ActualizarStock(int.Parse(productoID), stockActual);
                        int empresaIDVenta = Convert.ToInt32(Request.QueryString["empresaID"]);
                        int usuarioIDVenta = usuarioVenta.UsuarioID;
                        DateTime fechaVenta = DateTime.Now;
                        repoVenta.AgregarVenta(new Venta
                        {
                            Monto = productoSeleccionado.Precio * cantidadVendida,
                            Cantidad = cantidadVendida,
                            FechaVenta = fechaVenta,
                            Empresa = empresaVenta,
                            Usuario = usuarioVenta,
                            Producto = productoSeleccionado,
                            Categoria = categoriaVenta
                        });
                        repoMovInv.MovimientoInventario(int.Parse(productoID), stockVendido, "Venta", "Producto vendido", fechaVenta, usuarioIDVenta, empresaIDVenta);
                        lblResultadoVenta.Text = $"Producto seleccionado: {nombreProducto} (ID: {productoID}) (Stock Anterior: {stockAnterior}) (Stock Actual: {stockVendido})";
                        lblMensaje.Text = "Venta registrada con éxito";
                        btnConfirmarVenta.Visible = false;
                        CargarDivs();
                    }

                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al vender el producto." + ex;
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string productoID = ddlProductos.SelectedValue;
            string nombreProducto = ddlProductos.SelectedItem.Text;

            int stockAnterior = repoProducto.ObtenerStock(int.Parse(productoID));
            lblResultado.Text = $"Producto seleccionado: {nombreProducto} (ID: {productoID}) (Stock Anterior: {stockAnterior})";
            ClientScript.RegisterStartupScript(this.GetType(), "AgregarStock", "mostrarFormulario('divAgregarStock');", true);
        }

        protected void ddlProductosVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productoId = int.Parse(ddlProductosVenta.SelectedValue);

            Producto productoSeleccionado = repoProducto.ObtenerProductoxID(productoId);
            if (productoSeleccionado != null)
            {
                btnConfirmarVenta.Visible = true;
                txtNombreProdVenta.Text = productoSeleccionado.Nombre;
                txtDescripcionProdVenta.Text = productoSeleccionado.Descripcion;
                txtMarcaProdVenta.Text = productoSeleccionado.Marca;
                txtPrecioProdVenta.Text = productoSeleccionado.Precio.ToString("F2");
                txtStockProdVenta.Text = productoSeleccionado.Stock.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "RegistrarVenta", "mostrarFormulario('divRegistrarVenta');", true);
            }
            if (ddlProductosVenta.Items.Count == 1)
            {
                btnConfirmarVenta.Visible = true;

            }
        }

        protected void dgvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int stock = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Stock"));
                Label lblEstado = (Label)e.Row.FindControl("lblEstado");

                if (stock == 0)
                {
                    e.Row.BorderColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.BorderStyle = BorderStyle.Double;
                    lblEstado.Text = "Sin Stock";
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                }
                else if (stock <= 5)
                {

                    e.Row.BorderColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    lblEstado.Text = "Stock Bajo - Restockear";
                    e.Row.BorderStyle = BorderStyle.Double;
                    lblEstado.ForeColor = System.Drawing.Color.Red;

                }
                else if (stock <= 10)
                {
                    e.Row.BorderColor = System.Drawing.Color.Orange;
                    e.Row.ForeColor = System.Drawing.Color.Orange;
                    lblEstado.Text = "Stock Medio";

                    lblEstado.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    lblEstado.Text = "Normal";
                    e.Row.BackColor = System.Drawing.Color.Transparent;
                    lblEstado.ForeColor = System.Drawing.Color.Green;
                    e.Row.BorderStyle = BorderStyle.Double;
                    e.Row.BorderColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = ddlFiltro.SelectedValue;
            int empresaID = Convert.ToInt32(Request.QueryString["empresaID"]);
            try
            {

                if (!string.IsNullOrEmpty(filtro))
                {
                    if (filtro == "Nombre")
                    {
                        string nombre = txtFiltro.Text;
                        if(string.IsNullOrEmpty(nombre))
                        {
                            lblMensaje.Text = "Debe ingresar un nombre para filtrar.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        List<Producto> productos = repoProducto.ObtenerProductos();
                        List<Producto> productosxEmpresa = new List<Producto>();
                        foreach (Producto producto in productos)
                        {
                            if (producto.EmpresaID == empresaID)
                            {
                                productosxEmpresa.Add(producto);
                            }
                        }
                        List<Producto> productosFiltrados = productosxEmpresa.Where(producto => producto.Nombre.Contains(nombre)).ToList();
                        if (productosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No hay productos con ese nombre.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        dgvProductos.DataSource = productosFiltrados;
                        dgvProductos.DataBind();

                    } else if (filtro == "Marca")
                    {
                        string marca = txtFiltro.Text;
                        if (string.IsNullOrEmpty(marca))
                        {
                            lblMensaje.Text = "Debe ingresar un nombre para filtrar.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        List<Producto> productos = repoProducto.ObtenerProductos();
                        List<Producto> productosxEmpresa = new List<Producto>();
                        foreach (Producto producto in productos)
                        {
                            if (producto.EmpresaID == empresaID)
                            {
                                productosxEmpresa.Add(producto);
                            }
                        }
                        List<Producto> productosFiltrados = productosxEmpresa.Where(producto => producto.Marca.Contains(marca)).ToList();
                        if (productosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No hay productos con esa marca.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        dgvProductos.DataSource = productosFiltrados;
                        dgvProductos.DataBind();

                    }
                    else if (filtro == "ID")
                    {
                        int id = Convert.ToInt32(txtFiltro.Text);
                        if (id<=0)
                        {
                            lblMensaje.Text = "Debe ingresar un ID para filtrar.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        List<Producto> productos = repoProducto.ObtenerProductos();
                        List<Producto> productosxEmpresa = new List<Producto>();
                        foreach (Producto producto in productos)
                        {
                            if (producto.EmpresaID == empresaID)
                            {
                                productosxEmpresa.Add(producto);
                            }
                        }
                        List<Producto> productosFiltrados = productosxEmpresa.Where(producto => producto.ProductoID == id).ToList();
                        if (productosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No hay productos con ese ID.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        dgvProductos.DataSource = productosFiltrados;
                        dgvProductos.DataBind();
                    }
                    else if (filtro == "Categoria")
                    {
                        int categoriaID = int.Parse(ddlCategoriasFiltro.SelectedValue);
                        List<Producto> productos = repoProducto.ObtenerProductos();
                        List<Producto> productosxEmpresa = new List<Producto>();
                        foreach (Producto producto in productos)
                        {
                            if (producto.EmpresaID == empresaID)
                            {
                                productosxEmpresa.Add(producto);
                            }
                        }
                        List<Producto> productosFiltrados = productosxEmpresa.Where(producto => producto.CategoriaID == categoriaID).ToList();
                        if (productosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No hay productos con esa categoria.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        dgvProductos.DataSource = productosFiltrados;
                        dgvProductos.DataBind();
                    }
                    else if(filtro == "Proveedores")
                    {
                        int proveedoresID = int.Parse(ddlProveedoresFiltro.SelectedValue);
                        List<Producto> productos = repoProducto.ObtenerProductos();
                        List<Producto> productosxEmpresa = new List<Producto>();
                        foreach (Producto producto in productos)
                        {
                            if (producto.EmpresaID == empresaID)
                            {
                                productosxEmpresa.Add(producto);
                            }
                        }
                        List<Producto> productosFiltrados = productosxEmpresa.Where(producto => producto.ProveedorID == proveedoresID).ToList();
                        if(productosFiltrados.Count == 0)
                        {
                            lblMensaje.Text = "No hay productos con ese proveedor.";
                            lblMensaje.Visible = true;
                            lblMensaje.CssClass = "alert alert-danger";
                            return;
                        }
                        dgvProductos.DataSource = productosFiltrados;
                        dgvProductos.DataBind();
                    }
                    else if (filtro == "Stock")
                    {
                        string orden = ddlStockFiltro.SelectedValue;
                        List<Producto> productos = repoProducto.ObtenerProductos();
                        List<Producto> productosxEmpresa = new List<Producto>();
                        foreach (Producto producto in productos)
                        {
                            if (producto.EmpresaID == empresaID)
                            {
                                productosxEmpresa.Add(producto);
                            }
                        }
                        if (orden == "ASC")
                        {
                            List<Producto> productosOrdenadosAsc = productosxEmpresa.OrderBy(producto => producto.Stock).ToList();
                            dgvProductos.DataSource = productosOrdenadosAsc;
                            dgvProductos.DataBind();

                        } else if (orden == "DESC")
                        {
                            List<Producto> productosOrdenadosDesc = productosxEmpresa.OrderByDescending(producto => producto.Stock).ToList();
                            dgvProductos.DataSource = productosOrdenadosDesc;
                            dgvProductos.DataBind();
                        }

                    }
                }
                else
                {
                    lblMensaje.Text = "Debe seleccionar un filtro.";
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al filtrar las empresas: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            ddlFiltro.SelectedIndex = 0;
            ddlCategoriasFiltro.Visible = false;
            ddlProveedoresFiltro.Visible = false;
            lblMensaje.Visible = false;
            lblStockFiltro.Visible = false;
            ddlStockFiltro.Visible = false;
            txtFiltro.Visible = true;

            CargarProductos();
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFiltro.SelectedIndex == 4)
            {
                ddlCategoriasFiltro.Visible = true;
                ddlProveedoresFiltro.Visible = false;
                lblCateFiltro.Visible = true;
                lblProvFiltro.Visible = false;
                txtFiltro.Visible = false;
                ddlStockFiltro.Visible = false;
                lblStockFiltro.Visible = false;
            }
            else if (ddlFiltro.SelectedIndex == 5)
            {
                lblCateFiltro.Visible = false;
                lblProvFiltro.Visible = true;
                ddlCategoriasFiltro.Visible = false;
                ddlProveedoresFiltro.Visible = true;
                txtFiltro.Visible = false;
                ddlStockFiltro.Visible = false;
                lblStockFiltro.Visible = false;
            } else if (ddlFiltro.SelectedIndex == 6)
            {
                lblCateFiltro.Visible = false;
                lblProvFiltro.Visible = false;
                ddlCategoriasFiltro.Visible = false;
                ddlProveedoresFiltro.Visible = false;
                txtFiltro.Visible = false;
                ddlStockFiltro.Visible = true;
                lblStockFiltro.Visible = true;
            }
            else
            {
                lblStockFiltro.Visible = false;
                lblCateFiltro.Visible = false;
                lblProvFiltro.Visible = false;
                ddlStockFiltro.Visible = false;
                ddlCategoriasFiltro.Visible = false;
                ddlProveedoresFiltro.Visible = false;
                txtFiltro.Visible = true;
            }
        }

        protected void btnFiltrarMovimientos_Click(object sender, EventArgs e)
        {

        }

        protected void btnLimpiarFiltroMovimientos_Click(object sender, EventArgs e)
        {

        }
    }
}

