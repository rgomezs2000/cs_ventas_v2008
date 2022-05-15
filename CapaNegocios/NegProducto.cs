using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//Para que se comunique con la Capa de Datos
using CapaDatos;

namespace CapaNegocios
{
    public class NegProducto
    {
        //Metodo que llama al metodo Insertar de la Capa de Datos 
        //de la clase Producto
        public static string Insertar(string Nombre, decimal Precio)
        {
            Producto pro = new Producto();
            pro.nombre = Nombre;
            pro.precio = Precio;
            return pro.Insertar(pro);
        }
        //Metodo que llama al metodo Actualizar de la Capa de Datos 
        //de la clase Producto
        public static string Actualizar(int CodigoProducto, string Nombre, decimal Precio)
        {
            Producto pro = new Producto();
            pro.codigoProducto = CodigoProducto;
            pro.nombre = Nombre;
            pro.precio = Precio;
            return pro.Actualizar(pro);
        }
        //Metodo que se encarga de llamar al metodo ObtenerProducto
        //de la clase Producto
        public static DataTable ObtenerProducto()
        {
            return new Producto().ObtenerProducto();
        }
    }
}
