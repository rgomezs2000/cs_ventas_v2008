using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDatos
{
    public class Conexion
    {
        //La base de datos se llama BDTutorial
        //La ubicacion de base de datos esta de modo local y en una instancia que se llama SQL2008
        //Utiliza seguridad integrada para conectarse a la base de datos
        public static string cn = "Data source=(local);DataBase=BDTutorial;Integrated Security=SSPI";
    }
}
