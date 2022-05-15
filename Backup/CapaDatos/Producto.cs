

using System;
using System.Collections.Generic;
using System.Text;
//Impotaciones necesarias
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Producto
    {
        private int var_codigoProducto;
        private string var_nombre;
        private decimal var_precio;

        //Constructor vacio
        public Producto()
        {

        }

        //Constructor con parametros
        public Producto(
            int codigoProducto,
            string nombre,
            decimal precio
        )
        {
            this.var_codigoProducto = codigoProducto;
            this.var_nombre = nombre;
            this.var_precio = precio;
        }

        //Metodo utilizado para insertar un Producto
        public string Insertar(Producto varProducto)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                //1. Establecer la cadena de conexion
                sqlCon.ConnectionString = Conexion.cn;
                //2. Abrir la conexion de la BD
                sqlCon.Open();
                //3. Establecer el comando
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spI_Producto";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //4. Agregar los parametros al comando
                //Establecemos los valores para el parametro @codigoProducto del Procedimiento Almacenado
                SqlParameter sqlParcodigoProducto = new SqlParameter();
                sqlParcodigoProducto.ParameterName = "@codigoProducto";
                sqlParcodigoProducto.SqlDbType = SqlDbType.Int;
                sqlParcodigoProducto.Direction = ParameterDirection.InputOutput;
                sqlParcodigoProducto.Value = varProducto.codigoProducto;
                sqlCmd.Parameters.Add(sqlParcodigoProducto); //Agregamos el parametro al comando
                //Establecemos los valores para el parametro @nombre del Procedimiento Almacenado
                SqlParameter sqlParnombre = new SqlParameter();
                sqlParnombre.ParameterName = "@nombre";
                sqlParnombre.SqlDbType = SqlDbType.VarChar;
                sqlParnombre.Size = 100;
                sqlParnombre.Value = varProducto.nombre;
                sqlCmd.Parameters.Add(sqlParnombre); //Agregamos el parametro al comando
                //Establecemos los valores para el parametro @precio del Procedimiento Almacenado
                SqlParameter sqlParprecio = new SqlParameter();
                sqlParprecio.ParameterName = "@precio";
                sqlParprecio.SqlDbType = SqlDbType.Decimal;
                sqlParprecio.Precision=18;
                sqlParprecio.Scale=2;
                sqlParprecio.Value = varProducto.precio;
                sqlCmd.Parameters.Add(sqlParprecio); //Agregamos el parametro al comando
               
                //5. Ejecutamos el commando
                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se inserto el producto de forma correcta";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                //6. Cerramos la conexion con la BD
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rpta;                
        }


        //Metodo utilizado para actualizar un Producto
        public string Actualizar(Producto varProducto)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                //1. Establecer la cadena de conexion
                sqlCon.ConnectionString = Conexion.cn;
                //2. Abrir la conexion de la BD
                sqlCon.Open();
                //3. Establecer el comando
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spU_Producto";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //4. Agregar los parametros al comando
                //Establecemos los valores para el parametro @codigoProducto del Procedimiento Almacenado
                SqlParameter sqlParcodigoProducto = new SqlParameter();
                sqlParcodigoProducto.ParameterName = "@codigoProducto";
                sqlParcodigoProducto.SqlDbType = SqlDbType.Int;
                sqlParcodigoProducto.Value = varProducto.codigoProducto;
                sqlCmd.Parameters.Add(sqlParcodigoProducto); //Agregamos el parametro al comando
                //Establecemos los valores para el parametro @nombre del Procedimiento Almacenado
                SqlParameter sqlParnombre = new SqlParameter();
                sqlParnombre.ParameterName = "@nombre";
                sqlParnombre.SqlDbType = SqlDbType.VarChar;
                sqlParnombre.Size = 100;
                sqlParnombre.Value = varProducto.nombre;
                sqlCmd.Parameters.Add(sqlParnombre); //Agregamos el parametro al comando
                //Establecemos los valores para el parametro @precio del Procedimiento Almacenado
                SqlParameter sqlParprecio = new SqlParameter();
                sqlParprecio.ParameterName = "@precio";
                sqlParprecio.SqlDbType = SqlDbType.Decimal;
                sqlParprecio.Precision = 18;
                sqlParprecio.Scale = 2;
                sqlParprecio.Value = varProducto.precio;
                sqlCmd.Parameters.Add(sqlParprecio); //Agregamos el parametro al comando
                
                //5. Ejecutamos el commando
                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se actualizo el producto de forma correcta";

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                //6. Cerramos la conexion con la BD
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rpta;                
        }

       //Metodo utilizado para obtener todos los productos de la base de datos
       public DataTable ObtenerProducto()
       {
            DataTable dtProducto = new DataTable("Producto");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                //1. Establecer la cadena de conexion
                sqlCon.ConnectionString = Conexion.cn;

                //2. Establecer el comando
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;//La conexion que va a usar el comando
                sqlCmd.CommandText = "spF_Producto_All";//El comando a ejecutar
                sqlCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3. No hay parametros

                //4. El DataAdapter que va a ejecutar el comando y es el encargado de llena el DataTable
                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(dtProducto);//Llenamos el DataTable
            }
            catch (Exception )
            {
                dtProducto = null;
            }
            return dtProducto;
        }

        #region Metodos Get y Set
        public int codigoProducto
        {
            get { return var_codigoProducto; }
            set { var_codigoProducto = value; }
        }
        public string nombre
        {
            get { return var_nombre; }
            set { var_nombre = value; }
        }
        public decimal precio
        {
            get { return var_precio; }
            set { var_precio = value; }
        }
        #endregion

    }
}