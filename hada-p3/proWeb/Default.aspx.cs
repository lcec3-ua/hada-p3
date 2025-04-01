using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using library;

namespace proWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack) // Solo se ejecuta la primera vez que se carga la página
            try
            {
                using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString))
                {
                    connection.Open();
                    lblMessage.Text = "✅ ¡Conexión a BD exitosa!";
                    CargarCategorias();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "❌ Error de conexión: " + ex.Message;
            }

        }

        // Carga las categorías desde la BD al DropDownList
        private void CargarCategorias()
        {
            CADCategory cadCategory = new CADCategory();
            var categorias = cadCategory.ReadAll();

            ddlCategory.Items.Clear();
            foreach (var categoria in categorias)
            {
                ddlCategory.Items.Add(new ListItem(categoria.Name, categoria.Id.ToString()));
            }
        }

        // Limpia los campos del formulario
        private void LimpiarFormulario()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtAmount.Text = "0";
            txtPrice.Text = "0.00";
            ddlCategory.SelectedIndex = 0;
            txtCreationDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            lblMessage.Text = "";
        }

        // Botón CREAR
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                ENProduct product = new ENProduct
                {
                    Code = txtCode.Text,
                    Name = txtName.Text,
                    Amount = int.Parse(txtAmount.Text),
                    Price = float.Parse(txtPrice.Text),
                    Category = int.Parse(ddlCategory.SelectedValue),
                    CreationDate = DateTime.Parse(txtCreationDate.Text)
                };

                if (product.Create())
                {
                    lblMessage.Text = "✅ Producto creado correctamente";
                    LimpiarFormulario();
                }
                else
                {
                    lblMessage.Text = "❌ Error al crear el producto (¿Código duplicado?)";
                }
            }
        }

        // Botón LEER
        protected void btnRead_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "⚠️ Ingresa un código";
                return;
            }

            ENProduct product = new ENProduct { Code = txtCode.Text };
            if (product.Read())
            {
                txtName.Text = product.Name;
                txtAmount.Text = product.Amount.ToString();
                txtPrice.Text = product.Price.ToString("0.00");
                ddlCategory.SelectedValue = product.Category.ToString();
                txtCreationDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                lblMessage.Text = "✅ Producto encontrado";
            }
            else
            {
                lblMessage.Text = "❌ Producto no encontrado";
            }
        }

        // Botón ACTUALIZAR
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                ENProduct product = new ENProduct
                {
                    Code = txtCode.Text,
                    Name = txtName.Text,
                    Amount = int.Parse(txtAmount.Text),
                    Price = float.Parse(txtPrice.Text),
                    Category = int.Parse(ddlCategory.SelectedValue),
                    CreationDate = DateTime.Parse(txtCreationDate.Text)
                };

                if (product.Update())
                {
                    lblMessage.Text = "✅ Producto actualizado correctamente";
                }
                else
                {
                    lblMessage.Text = "❌ Error al actualizar (¿El producto existe?)";
                }
            }
        }

        // Botón ELIMINAR
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "⚠️ Ingresa un código";
                return;
            }

            ENProduct product = new ENProduct { Code = txtCode.Text };
            if (product.Delete())
            {
                lblMessage.Text = "✅ Producto eliminado";
                LimpiarFormulario();
            }
            else
            {
                lblMessage.Text = "❌ Error al eliminar (¿El producto existe?)";
            }
        }

        // Botones de Navegación (PRIMERO, ANTERIOR, SIGUIENTE)
        protected void btnReadFirst_Click(object sender, EventArgs e)
        {
            ENProduct product = new ENProduct();
            if (product.ReadFirst())
            {
                CargarProductoEnFormulario(product);
                lblMessage.Text = "✅ Primer producto cargado";
            }
            else
            {
                lblMessage.Text = "❌ No hay productos en la BD";
            }
        }

        protected void btnReadPrev_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "⚠️ Ingresa un código primero";
                return;
            }

            ENProduct product = new ENProduct { Code = txtCode.Text };
            if (product.ReadPrev())
            {
                CargarProductoEnFormulario(product);
                lblMessage.Text = "✅ Producto anterior cargado";
            }
            else
            {
                lblMessage.Text = "❌ No hay producto anterior";
            }
        }

        protected void btnReadNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblMessage.Text = "⚠️ Ingresa un código primero";
                return;
            }

            ENProduct product = new ENProduct { Code = txtCode.Text };
            if (product.ReadNext())
            {
                CargarProductoEnFormulario(product);
                lblMessage.Text = "✅ Producto siguiente cargado";
            }
            else
            {
                lblMessage.Text = "❌ No hay producto siguiente";
            }
        }

        // Métodos auxiliares
        private void CargarProductoEnFormulario(ENProduct product)
        {
            txtCode.Text = product.Code;
            txtName.Text = product.Name;
            txtAmount.Text = product.Amount.ToString();
            txtPrice.Text = product.Price.ToString("0.00");
            ddlCategory.SelectedValue = product.Category.ToString();
            txtCreationDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtCode.Text) || txtCode.Text.Length > 16)
            {
                lblMessage.Text = "⚠️ Código inválido (1-16 caracteres)";
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text.Length > 32)
            {
                lblMessage.Text = "⚠️ Nombre inválido (máx 32 caracteres)";
                return false;
            }

            if (!int.TryParse(txtAmount.Text, out int amount) || amount < 0 || amount > 9999)
            {
                lblMessage.Text = "⚠️ Cantidad inválida (0-9999)";
                return false;
            }

            if (!float.TryParse(txtPrice.Text, out float price) || price < 0 || price > 9999.99)
            {
                lblMessage.Text = "⚠️ Precio inválido (0-9999.99)";
                return false;
            }

            if (!DateTime.TryParse(txtCreationDate.Text, out _))
            {
                lblMessage.Text = "⚠️ Fecha inválida (dd/MM/yyyy HH:mm:ss)";
                return false;
            }

            return true;
        }
    }
}