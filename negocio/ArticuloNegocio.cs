using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Articulo> listar()
        {
            try
            {
                List<Articulo> lista = new List<Articulo>();

                datos.setearConsulta("select a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id as IDMarca, m.Descripcion as Marca, c.Id as IDCategoria, c.Descripcion as Categoria, a.Precio, a.ImagenUrl from ARTICULOS a inner join MARCAS m on a.IdMarca = m.Id inner join CATEGORIAS c on a.IdCategoria = c.Id;");
                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Articulo auxiliar = new Articulo();

                    Marca marca = new Marca();
                    Categoria categoria = new Categoria();

                    auxiliar.Id = (int)datos.Lector["Id"];
                    auxiliar.Codigo = (string)datos.Lector["Codigo"];
                    auxiliar.Nombre = (string)datos.Lector["Nombre"];
                    auxiliar.Descripcion = (string)datos.Lector["Descripcion"];
                    auxiliar.Marca = marca;
                    auxiliar.Marca.Id = (int)datos.Lector["IDMarca"];
                    auxiliar.Marca.Descripcion = (string)datos.Lector["Marca"];
                    auxiliar.Categoria = categoria;
                    auxiliar.Categoria.Id = (int)datos.Lector["IDCategoria"];
                    auxiliar.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        auxiliar.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }
                    auxiliar.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(auxiliar);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Articulo articulo)
        {
            try
            {
                datos.setearConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@codigo , @nombre, @descripcion, @idMarca, @idCategoria, @imagenUrl, @precio);");
                setearParametrosSql(articulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo articulo)
        {

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio where Id = @id");
                setearParametrosSql(articulo);
                datos.setearParametros("@id", articulo.Id);
                datos.ejecutarAccion();
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarFisico(int id)
        {
            try
            {
                datos.setearConsulta("delete from ARTICULOS where Id = " + id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string consulta = "select a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id as IDMarca, m.Descripcion as Marca, c.Id as IDCategoria, c.Descripcion as Categoria, a.Precio, a.ImagenUrl from ARTICULOS a inner join MARCAS m on a.IdMarca = m.Id inner join CATEGORIAS c on a.IdCategoria = c.Id where ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "a.Precio > " + filtro;
                            break;

                        case "Menor a":
                            consulta += "a.Precio < " + filtro;
                            break;

                        default:
                            consulta += "a.Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "a.Nombre like '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "a.Nombre like '%" + filtro + "'";
                            break;

                        default:
                            consulta += "a.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "m.Descripcion like '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "m.Descripcion like '%" + filtro + "'";
                            break;

                        default:
                            consulta += "m.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    consulta += "c.Descripcion = '" + filtro + "'";
                }


                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo auxiliar = new Articulo();

                    Marca marca = new Marca();
                    Categoria categoria = new Categoria();

                    auxiliar.Id = (int)datos.Lector["Id"];
                    auxiliar.Codigo = (string)datos.Lector["Codigo"];
                    auxiliar.Nombre = (string)datos.Lector["Nombre"];
                    auxiliar.Descripcion = (string)datos.Lector["Descripcion"];
                    auxiliar.Marca = marca;
                    auxiliar.Marca.Id = (int)datos.Lector["IDMarca"];
                    auxiliar.Marca.Descripcion = (string)datos.Lector["Marca"];
                    auxiliar.Categoria = categoria;
                    auxiliar.Categoria.Id = (int)datos.Lector["IDCategoria"];
                    auxiliar.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        auxiliar.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }
                    auxiliar.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(auxiliar);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void setearParametrosSql(Articulo articulo)
        {
            datos.setearParametros("@codigo", articulo.Codigo);
            datos.setearParametros("@nombre", articulo.Nombre);
            datos.setearParametros("@descripcion", articulo.Descripcion);
            datos.setearParametros("@idMarca", articulo.Marca.Id);
            datos.setearParametros("@idCategoria", articulo.Categoria.Id);
            datos.setearParametros("@imagenUrl", articulo.ImagenUrl);
            datos.setearParametros("@precio", articulo.Precio);
        }

    }
}
