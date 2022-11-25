using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using negocio;
using dominio;
using System.IO;
using System.Configuration;

namespace presentacion1
{
    public partial class AgregarArticulo : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;

        public AgregarArticulo()
        {
            InitializeComponent();
        }

        public AgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";

        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                cbxMarca.DataSource = marcaNegocio.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";

                cbxCategoria.DataSource = categoriaNegocio.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    tbxCodigo.Text = articulo.Codigo;
                    tbxNombre.Text = articulo.Nombre;
                    tbxDescripcion.Text = articulo.Descripcion;
                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                    tbxImagenUrl.Text = articulo.ImagenUrl;
                    double precioDouble = double.Parse(articulo.Precio.ToString());
                    tbxPrecio.Text = precioDouble.ToString();
                    if (soloNumeros(tbxPrecio.Text, true))
                    {
                        tbxPrecio.ForeColor = Color.Black;
                        btnAceptar.Enabled = true;
                    }

                    pbxImagenAgregar.Load(tbxImagenUrl.Text);
                }

                if (pbxImagenAgregar.Image == null)
                {
                    pbxImagenAgregar.BorderStyle = BorderStyle.FixedSingle;
                }

            }
            catch (Exception)
            {
                pbxImagenAgregar.Load("https://storage.googleapis.com/proudcity/mebanenc/uploads/2021/03/placeholder-image.png");
            }
        }     

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                {
                    articulo = new Articulo();
                }

                articulo.Codigo = tbxCodigo.Text.ToString();
                articulo.Nombre = tbxNombre.Text.ToString();
                articulo.Descripcion = tbxDescripcion.Text.ToString();
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Marca.Descripcion = cbxMarca.SelectedValue.ToString();
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                articulo.Categoria.Descripcion = cbxCategoria.SelectedValue.ToString();
                articulo.ImagenUrl = tbxImagenUrl.Text.ToString();

                if (tbxPrecio.Text == string.Empty)
                {
                    articulo.Precio = 0;
                }
                else
                {
                    articulo.Precio = decimal.Parse(tbxPrecio.Text);
                }

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Artículo Modificado!");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Artículo agregado!");
                }

                if (archivo != null && !(tbxImagenUrl.Text.ToLower().Contains("http")))
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["ImagenesArticulos"] + archivo.SafeFileName, true);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Close();
            }
        }

        private void tbxImagenUrl_Leave(object sender, EventArgs e)
        {
            try
            {

                pbxImagenAgregar.Load(tbxImagenUrl.Text);

            }
            catch (Exception)
            {

                pbxImagenAgregar.Load("https://storage.googleapis.com/proudcity/mebanenc/uploads/2021/03/placeholder-image.png");
            }
            finally
            {
                pbxImagenAgregar.BorderStyle = BorderStyle.None;
            }
        }

        private bool soloNumeros(string cadena, bool modificar = false)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)) && !modificar)
                {
                    if (!(caracter.Equals('.')))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void tbxPrecio_TextChanged(object sender, EventArgs e)
        {
            if (!(soloNumeros(tbxPrecio.Text)))
            {
                tbxPrecio.ForeColor = Color.Red;
                btnAceptar.Enabled = false;
            }
            else
            {
                tbxPrecio.ForeColor = Color.Black;
                btnAceptar.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                archivo = new OpenFileDialog();
                archivo.Filter = "jpg|*.jpg;|png|*.png";
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    tbxImagenUrl.Text = archivo.FileName;
                    pbxImagenAgregar.Load(archivo.FileName);
                }
            }
            catch (Exception)
            {

                pbxImagenAgregar.Load("https://storage.googleapis.com/proudcity/mebanenc/uploads/2021/03/placeholder-image.png");
            }

           
        }
    }
}
