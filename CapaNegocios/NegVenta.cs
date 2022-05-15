using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//Para que se comunique con la Capa de Datos
using CapaDatos;


namespace CapaNegocios
{
    public class NegVenta
    {
        //Metodo que llama al metodo Insertar de la Capa de Datos 
        //de la clase Venta
        public static string Insertar(string cliente, DataTable dtDetalles)
        {
            Venta venta = new Venta();
            venta.cliente = cliente;
            List<DetalleVenta> detalles=new List<DetalleVenta>();
            foreach (DataRow row in dtDetalles.Rows)
            {
                DetalleVenta detalle = new DetalleVenta();
                detalle.codigoProducto = Convert.ToInt32(row["codigoProducto"].ToString());
                detalle.cantidad = Convert.ToDecimal(row["cantidad"].ToString());
                detalle.descuento = NegDetalleVenta.ObtenerDescuento(detalle.cantidad, Convert.ToDecimal(row["PU"].ToString()));
                detalles.Add(detalle);
            }
            return venta.Insertar(venta, detalles);
        }
        //Metodo que se encarga de llamar al metodo ObtenerProducto
        //de la clase Venta
        public static DataTable ObtenerVenta()
        {
            return new Venta().ObtenerVenta();
        }

        //Metodo que se encarga de llamar al metodo ObtenerProducto 
        //por codigo de la clase Venta
        public static DataTable ObtenerVenta(int codigoVenta)
        {
            return new Venta().ObtenerVenta(codigoVenta);
        }
    }
}
