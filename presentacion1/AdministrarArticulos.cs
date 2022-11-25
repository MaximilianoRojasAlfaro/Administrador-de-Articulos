using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using dominio;
using negocio;

namespace presentacion1
{
    public partial class frmAdminArticulos : Form
    {
        private Font fuenteTitulo = new Font("Microsoft YaHei", 12.0f);
        private Font fuente = new Font("Microsoft YaHei", 10.0f);

        //Posicion en Y del contenedor
        private int contenedorY = 26;

        //Contador general para crear artículos
        private int contador = 0;

        //Objetos de ayuda para conectar con los datos de la Base de datos
        private Articulo aux = new Articulo();
        private AccesoDatos datos = new AccesoDatos();
        private ArticuloNegocio articuloNegocio = new ArticuloNegocio();


        private List<Articulo> listaArticulos = new List<Articulo>();
        private List<GroupBox> listaContenedores = new List<GroupBox>();
        //Los contenedores(artículos) relacionados al filtro
        private List<GroupBox> listaContenedoresFiltro = new List<GroupBox>();

        //Guardar botones de cada articulo
        private List<Button> listaBotonesDetalles = new List<Button>();
        private List<Button> listaBotonesModificar = new List<Button>();
        private List<Button> listaBotonesEliminar = new List<Button>();

        //Guardar labels de cada articulo
        private List<Label> listaLabelNombre = new List<Label>();
        private List<Label> listaLabelMarca = new List<Label>();
        private List<Label> listaLabelPrecio = new List<Label>();


        //Posiciones en X e Y antes de que cambie al momento de filtrar
        List<int> listaX = new List<int>();
        List<int> listaY = new List<int>();


        public frmAdminArticulos()
        {
            InitializeComponent();
        }



        //Eventos
        private void frmAdminArticulos_Load(object sender, EventArgs e)
        {
            try
            {
                tbxFiltro.Enabled = false;
                btnBuscar.Enabled = false;

                crearArticulosIniciales();
                cbxCampo.Items.Add("Nombre");
                cbxCampo.Items.Add("Marca");
                cbxCampo.Items.Add("Categoria");
                cbxCampo.Items.Add("Precio");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void detalles_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (Button boton in listaBotonesDetalles)
                {
                    if (listaBotonesDetalles.ElementAtOrDefault(i) == sender)
                    {
                        aux = listaArticulos[i];
                    }
                    i++;
                }

                Detalles detalleArticulos = new Detalles(aux);
                detalleArticulos.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        
        private void modificar_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (Button boton in listaBotonesModificar)
                {
                    if (boton == sender)
                    {
                        aux = listaArticulos[i];
                        break;
                    }
                    i++;
                }
                AgregarArticulo modificarArticulo = new AgregarArticulo(aux);
                modificarArticulo.ShowDialog();

                listaArticulos = articuloNegocio.listar();

                if (aux != listaArticulos[i])
                {
                    listaLabelNombre[i].Text = listaArticulos[i].Nombre;
                    listaLabelMarca[i].Text = listaArticulos[i].Marca.Descripcion;
                    double precioDouble = (double)listaArticulos[i].Precio;
                    listaLabelPrecio[i].Text = "$" + precioDouble.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (Button boton in listaBotonesEliminar)
                {
                    if (boton == sender)
                    {
                        aux = listaArticulos[i];
                        break;
                    }
                    i++;
                }

                DialogResult respuesta = MessageBox.Show("¿Estás seguro que quieres eliminar el artículo " + aux.Nombre + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    contador--;

                    eliminarArticulos(aux.Id, listaArticulos.IndexOf(aux));
                    listaArticulos = articuloNegocio.listar();
                    articuloNegocio.eliminarFisico(aux.Id);
                    listaArticulos = articuloNegocio.listar();
                    listaBotonesDetalles.RemoveAt(i);
                    listaBotonesModificar.RemoveAt(i);
                    listaBotonesEliminar.RemoveAt(i);
                    listaLabelNombre.RemoveAt(i);
                    listaLabelMarca.RemoveAt(i);
                    listaLabelPrecio.RemoveAt(i);
                    unirArticuloIdConControles();

                    if (btnAgregar.Enabled)
                    {
                        reordenarArticulos(listaContenedores);

                    }
                    else 
                    {
                        btnQuitarFiltro.Text = "Quitar Filtro y Reordenar";
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                AgregarArticulo agregarArticulo = new AgregarArticulo();
                agregarArticulo.ShowDialog();

                int a = listaArticulos.Count;
                listaArticulos = articuloNegocio.listar();

                if (listaArticulos.Count > a)
                {
                    añadirArticulo(listaArticulos.Last());
                    reordenarArticulos(listaContenedores);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxFiltro.Enabled = false;
            btnBuscar.Enabled = false;
            try
            {
                if (cbxCampo.SelectedItem.ToString() == "Precio")
                {
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Menor a");
                    cbxCriterio.Items.Add("Mayor a");
                    cbxCriterio.Items.Add("Igual a");
                }
                else if (cbxCampo.SelectedItem.ToString() == "Categoria")
                {
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Celulares");
                    cbxCriterio.Items.Add("Televisores");
                    cbxCriterio.Items.Add("Media");
                    cbxCriterio.Items.Add("Audio");
                }
                else
                {
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Empieza con");
                    cbxCriterio.Items.Add("Termina con");
                    cbxCriterio.Items.Add("Contiene");
                }
                validarOpcionesFiltro();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            } 
        }

        private void cbxCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxFiltro.Enabled = false;
            btnBuscar.Enabled = false;
            validarOpcionesFiltro();
        }

        private void tbxFiltro_TextChanged(object sender, EventArgs e)
        {
            if (cbxCampo.SelectedItem.ToString() == "Precio" && !(soloNumeros(tbxFiltro.Text)))
            {
                tbxFiltro.ForeColor = Color.Red;
                btnBuscar.Enabled = false;
            }
            else
            {
                tbxFiltro.ForeColor = Color.Black;
                btnBuscar.Enabled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                tbxFiltro.Enabled = false;
                cbxCampo.Enabled = false;
                cbxCriterio.Enabled = false;
                btnBuscar.Enabled = false;
                btnAgregar.Enabled = false;

                listaArticulos = articuloNegocio.listar();
                List<Articulo> listaFiltro;

                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();       
                string filtro = tbxFiltro.Text;
                if (cbxCampo.SelectedItem.ToString() == "Categoria")
                {
                    filtro = criterio;

                }
                if (filtro == string.Empty)
                {
                    filtro = "0";
                }

                listaFiltro = articuloNegocio.filtrar(campo, criterio, filtro);

                esconderArticulos();

                //Busca en la lista con todos los articulos los que cumplan con el filtro y los hace visibles
                for (int i = 0; i < listaFiltro.Count; i++)
                {
                    for (int j = 0; j < listaArticulos.Count; j++)
                    {
                        if (listaFiltro[i].Id == listaArticulos[j].Id)
                        {
                            listaContenedores[j].Visible = true;
                            listaContenedoresFiltro.Add(listaContenedores[j]);
                        }
                    }
                }

                btnQuitarFiltro.Visible = true;

                foreach (GroupBox contenedor in listaContenedores)
                {
                    listaX.Add(contenedor.Location.X);
                    listaY.Add(contenedor.Location.Y);
                }

                if (listaContenedoresFiltro.Count > 0)
                {
                    reordenarPrimerArticulo(listaContenedoresFiltro.First());
                }

                reordenarArticulos(listaContenedoresFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnQuitarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                tbxFiltro.Text = string.Empty;

                panel1.AutoScrollPosition = new Point(0, 0);

                listaContenedoresFiltro.Clear();
                int i = 0;
                foreach (GroupBox contenedor in listaContenedores)
                {
                    contenedor.Visible = true;
                    setearLocation(contenedor, listaX[i], listaY[i]);
                    i++;
                }

                btnQuitarFiltro.Visible = false;
                if (!(cbxCampo.SelectedItem.ToString() == "Categoria"))
                {
                    tbxFiltro.Enabled = true;
                }
                cbxCampo.Enabled = true;
                cbxCriterio.Enabled = true;
                btnBuscar.Enabled = true;
                btnAgregar.Enabled = true;

                btnQuitarFiltro.Text = "Quitar Filtro";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            } 
        }



        //Creación de los Articulos dentro del Panel
        private void crearArticulosIniciales()
        {
            try
            {
                listaArticulos = articuloNegocio.listar();

                foreach (Articulo articulo in listaArticulos)
                {
                    añadirArticulo(listaArticulos[contador]);
                }
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        private void crearContenedores(Panel panel = null)
        {
            try
            {
                //Crear Contenedor
                listaContenedores.Add(new GroupBox());
                setearControl(listaContenedores[contador], 578, 172, 0, contenedorY, "gbxContenedor" + contador.ToString());
                if (panel == null)
                {
                    panel1.Controls.Add(listaContenedores[contador]);
                }
                else
                {
                    panel.Controls.Add(listaContenedores[contador]);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void crearBotones(Articulo articulo)
        {
            try
            {
                Button detalles = new Button();// size: 104; 31 || location: 452; 26 
                Button modificar = new Button();// size: 104; 31 || location: 452; 66 
                Button eliminar = new Button();// size: 104; 31 || location: 452; 106

                //Crear botón para ver detalles
                setearControl(detalles, 104, 31, 432, 26, contador.ToString(), "Detalles");
                listaBotonesDetalles.Add(detalles);
                detalles.Font = fuente;
                //detalles.MouseHover += new EventHandler(btnAgregar_MouseHover);
                listaContenedores[contador].Controls.Add(detalles);
                detalles.Click += new EventHandler(detalles_Click);

                //Crear botón modificar
                setearControl(modificar, 104, 31, 432, 66, contador.ToString(), "Modificar");
                listaBotonesModificar.Add(modificar);
                modificar.Font = fuente;
                modificar.Click += new EventHandler(modificar_Click);
                listaContenedores[contador].Controls.Add(modificar);

                //Crear botón eliminar
                setearControl(eliminar, 104, 31, 432, 106, contador.ToString(), "Eliminar");
                listaBotonesEliminar.Add(eliminar);
                eliminar.Font = fuente;
                eliminar.Click += new EventHandler(eliminar_Click);
                listaContenedores[contador].Controls.Add(eliminar);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void CrearArticulo( string nombreArticulo, string nombreMarca, decimal cantidad, string categoria="", string imagen = "",  string codigo = "", string descripcion = "")
        {

            PictureBox imgArticulo = new PictureBox();//size: 176; 149 || location: 9; 16 || sizemode: strechimage
            Label articulo = new Label();//size: 235; 31 || location: 201; 19 || Font:Microsoft YaHei 12
            Label marca = new Label();//size: 71; 31 || location: 201; 67 
            Label marcaNombre = new Label();//size: 148; 31 || location: 313; 67 
            Label precio = new Label();//size: 71; 31 || location: 201; 108 
            Label precioCantidad = new Label();//size: 148; 31 || location: 313; 108 
            double cantidadDouble = (double)cantidad;

            try
            { 
                //Crear imagen del artículo
                setearControl(imgArticulo, 176, 149, 9, 16, "pcbArticulo" + contador.ToString());
                imgArticulo.SizeMode = PictureBoxSizeMode.StretchImage;
                listaContenedores[contador].Controls.Add(imgArticulo);

                //Crear nombre del artículo
                setearControl(articulo, 215, 31, 201, 19, "lblArticulo" + contador.ToString(), nombreArticulo);
                articulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                articulo.Font = fuenteTitulo;
                listaLabelNombre.Add(articulo);
                listaContenedores[contador].Controls.Add(articulo);

                //Crear label marca:
                setearControl(marca, 61, 31, 201, 67, "lblMarca" + contador.ToString(), "Marca:");
                marca.Font = fuente;
                marca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                listaContenedores[contador].Controls.Add(marca);

                //Crear label nombre de la marca
                setearControl(marcaNombre, 148, 31, 273, 67, "lblMarcaNombre" + contador.ToString(), nombreMarca);
                marcaNombre.Font = fuente;
                marcaNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                listaLabelMarca.Add(marcaNombre);
                listaContenedores[contador].Controls.Add(marcaNombre);

                //Crear label precio:
                setearControl(precio, 61, 31, 201, 108, "lblPrecio" + contador.ToString(), "Precio:");
                precio.Font = fuente;
                precio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                listaContenedores[contador].Controls.Add(precio);

                //Crear label precio del artículo
                setearControl(precioCantidad, 148, 31, 273, 108, "lblCantidad" + contador.ToString(), "$" + cantidadDouble.ToString());
                precioCantidad.Font = fuente;
                precioCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                listaLabelPrecio.Add(precioCantidad);
                listaContenedores[contador].Controls.Add(precioCantidad);

                //Cargar Imagen
                imgArticulo.Load(imagen);

            }
            catch (Exception)
            {
                imgArticulo.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");
            }
            finally
            {
                contenedorY += 180;
                contador++;
            }  
        }
        
        private void setearControl(Control control, int width, int height, int x, int y,  string name, string text = null)
        {
            control.Height = height;
            control.Width = width;
            control.Location = new Point(x, y);
            control.Name = name;
            control.Text = text;
        }



        //Funciones de ayuda

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    if (!(caracter.Equals('.')))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void validarOpcionesFiltro()
        {
            if (cbxCampo.SelectedItem != null && cbxCriterio.SelectedItem != null)
            {
                if (cbxCampo.SelectedItem.ToString() == "Categoria")
                {
                    btnBuscar.Enabled = true;
                    tbxFiltro.Enabled = false;
                }
                else
                {
                    tbxFiltro.Enabled= true;            
                }
            }
        }

        private void eliminarArticulos(int idArticulo, int listaArticulosId)
        {
            try
            {
                int nombreContenedor = int.Parse(listaContenedores[listaArticulosId].Name);

                if (nombreContenedor == idArticulo)
                {
                    listaContenedores[listaArticulosId].Dispose();
                    panel1.Controls.Remove(listaContenedores[listaArticulosId]);
                    listaContenedores.Remove(listaContenedores[listaArticulosId]);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void reordenarArticulos(List<GroupBox> lista)
        {
            try
            {
                int distancia;

                for (int j = 0; j < (lista.Count - 1); j++)
                {
                    distancia = lista[j + 1].Location.Y - lista[j].Location.Y;

                    if (distancia != 180)
                    {
                        lista[j + 1].Location = new Point(0, (lista[j].Location.Y + 180));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            
        }

        private void reordenarPrimerArticulo(GroupBox contenedor)
        {
            try
            {
                int distancia = contenedor.Location.Y - panel1.Location.Y;

                if (distancia != 14)
                {
                    contenedor.Location = new Point(0, 26);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            
        }

        private void añadirArticulo(Articulo a)
        {
            crearContenedores();
            crearBotones(a);
            unirArticuloIdConControles(contador);
            CrearArticulo(a.Nombre, a.Marca.Descripcion, a.Precio, a.Categoria.Descripcion, a.ImagenUrl, a.Codigo, a.Descripcion);
        }

        private void esconderArticulos()
        {
            foreach (GroupBox contenedor in listaContenedores)
            {
                contenedor.Visible = false;
            }
        }

        private void unirArticuloIdConControles(int indice = -1)
        {
            try
            {
                if (indice != -1)
                {
                    listaBotonesDetalles[indice].Name = listaArticulos[indice].Id.ToString();
                    listaBotonesModificar[indice].Name = listaArticulos[indice].Id.ToString();
                    listaBotonesEliminar[indice].Name = listaArticulos[indice].Id.ToString();
                    listaContenedores[indice].Name = listaArticulos[indice].Id.ToString();
                }
                else
                {
                    int i = 0;
                    foreach (Articulo articulo in listaArticulos)
                    {
                        listaContenedores[i].Name = listaArticulos[i].Id.ToString();
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void setearLocation(Control control, int x, int y)
        {
            control.Location = new Point(x, y);
        }

    }
}
