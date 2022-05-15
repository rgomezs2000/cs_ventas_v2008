using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios
{
    public class NegDetalleVenta
    {
        //Metodo utilizado para obtener el descuento del detalle de la venta
        //si la venta supera los 50 soles, dolares, euros, etc
        //se le hace un descuento del 5% del detalle de la venta
        public static decimal ObtenerDescuento(decimal cantidad, decimal pu)
        {
            if ((cantidad * pu) > 50)
            {
                decimal porcentaje = Convert.ToDecimal(0.05);
                decimal descuento = ((cantidad * pu) * porcentaje);
                return descuento;
            }
            else
            {
                return 0;
            }
        }
    }
}
