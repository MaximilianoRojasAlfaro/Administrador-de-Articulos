using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace presentacion1
{
    public partial class Detalles : Form
    {
        private Articulo articulo;
        public Detalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }


        private void Detalles_Load(object sender, EventArgs e)
        {
            double cantidadDouble = (double)articulo.Precio;

            try
            {
                lblArticulo.Text = articulo.Nombre;
                lblCodigo.Text = articulo.Codigo;
                lblNombreMarca.Text = articulo.Marca.Descripcion;
                lblCategoria.Text = articulo.Categoria.Descripcion;
                lblDescripcion.Text = articulo.Descripcion;
                lblPrecio.Text = "$" + cantidadDouble.ToString();
                pbxDetalleArticulo.Load(articulo.ImagenUrl);
            }
            catch (Exception)
            {

                pbxDetalleArticulo.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");
            }
        }
    }
}
