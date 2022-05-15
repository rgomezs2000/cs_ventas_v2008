using System;
using System.Collections.Generic;
using System.Text;
//Impotaciones necesarias
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Venta
    {
        private int var_codigoVenta;
        private string var_cliente;
        private DateTime var_fecha;
       
        //Constructor vacio
        public Venta()
        {
            
        }

        //Constructor con parametros
        public Venta(int codigoVenta,string cliente,DateTime fecha)
        {
            this.var_codigoVenta=codigoVenta;
            this.var_cliente=cliente;
            this.var_fecha=fecha;
        }

        //Metodo utilizado para insertar un Venta
        public string Insertar(Venta varVenta, List<DetalleVenta> detalles)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            
            //try
            //{
                //1. Establecer la cadena de conexion
                sqlCon.ConnectionString = Conexion.cn;
                //2. Abrir la conexion de la BD
                sqlCon.Open();
                //3. Establecer la transaccion
                SqlTransaction sqlTra = sqlCon.BeginTransaction();
                //4. Establecer el comando
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTra;
                sqlCmd.CommandText = "spI_Venta";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //5. Agregar los parametros al comando
                //Establecemos los valores para el parametro @codigoVenta del Procedimiento Almacenado
                SqlParameter sqlParcodigoVenta = new SqlParameter();
                sqlParcodigoVenta.ParameterName = "@codigoVenta";
                sqlParcodigoVenta.SqlDbType = SqlDbType.Int;
                sqlParcodigoVenta.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(sqlParcodigoVenta); //Agregamos el parametro al comando
                //Establecemos los valores para el parametro @cliente del Procedimiento Almacenado
                SqlParameter sqlParcliente = new SqlParameter();
                sqlParcliente.ParameterName = "@cliente";
                sqlParcliente.SqlDbType = SqlDbType.VarChar;
                sqlParcliente.Size = 100;
                sqlParcliente.Value = varVenta.cliente;
                sqlCmd.Parameters.Add(sqlParcliente); //Agregamos el parametro al comando
                //6. Ejecutamos el commando
                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se inserto el detalle de venta de forma correcta";
                if (rpta.Equals("OK"))
                {
                    //Obtenemos el codigo de la venta que se genero por la base de datos
                    this.codigoVenta=Convert.ToInt32(sqlCmd.Parameters["@codigoVenta"].Value);
                    foreach(DetalleVenta det in detalles){
                        //Establecemos el codigo de la venta que se autogenero
                        det.codigoVenta = this.codigoVenta;
                        //Llamamos al metodo insertar de la clase DetalleVenta
                        //y le pasamos la conexion y la transaccion que debe de usar
                        rpta = det.Insertar(det, ref sqlCon, ref sqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            //Si ocurre un error al insertar un detalle de venta salimos del for
                            break;
                        }
                    }
                }
                if (rpta.Equals("OK"))
                {
                    //Se inserto todo los detalles y confirmamos la transaccion
                    sqlTra.Commit();
                }
                else
                {
                    //Algun detalle no se inserto y negamos la transaccion
                    sqlTra.Rollback();
                }

            //}
            //catch (Exception ex)
            //{
            //    rpta = ex.Message;
            //}
            //finally
            //{
            //    //6. Cerramos la conexion con la BD
            //    if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            //}
            return rpta;                
        }

        //Obtenemos la venta por el codigo generado
        public DataTable ObtenerVenta(int codigoVenta)
        {
            DataTable dtVenta = new DataTable("Venta");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                //1. Establecer la cadena de conexion
                sqlCon.ConnectionString = Conexion.cn;

                //2. Establecer el comando
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;//La conexion que va a usar el comando
                sqlCmd.CommandText = "spF_Venta_One";//El comando a ejecutar
                sqlCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3. Agregar los parametros al comando
                //Establecemos los valores para el parametro @codigoVenta del Procedimiento Almacenado
                SqlParameter sqlParcodigoVenta = new SqlParameter();
                sqlParcodigoVenta.ParameterName = "@codigoVenta";
                sqlParcodigoVenta.SqlDbType = SqlDbType.Int;
                sqlParcodigoVenta.Value = codigoVenta;
                sqlCmd.Parameters.Add(sqlParcodigoVenta); //Agregamos el parametro al comando

                //4. El DataAdapter que va a ejecutar el comando y es el encargado de llena el DataTable
                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(dtVenta);//Llenamos el DataTable
            }
            catch (Exception )
            {
                dtVenta = null;
            }
            return dtVenta;
        }

        //Obtener todas las ventas
        public DataTable ObtenerVenta()
        {
            DataTable dtVenta = new DataTable("Venta");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                //1. Establecer la cadena de conexion
                sqlCon.ConnectionString = Conexion.cn;

                //2. Establecer el comando
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;//La conexion que va a usar el comando
                sqlCmd.CommandText = "spF_Venta_All";//El comando a ejecutar
                sqlCmd.CommandType = CommandType.StoredProcedure;//Decirle al comando que va a ejecutar una sentencia SQL

                //3. No hay parametros

                //4. El DataAdapter que va a ejecutar el comando y es el encargado de llena el DataTable
                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(dtVenta);//Llenamos el DataTable
            }
            catch (Exception )
            {
                dtVenta = null;
            }
            return dtVenta;
        }
        
        #region Metodos Get y Set
        public int codigoVenta
        {
            get { return var_codigoVenta; }
            set { var_codigoVenta = value; }
        }
        public string cliente
        {
            get { return var_cliente; }
            set { var_cliente = value; }
        }
        public DateTime fecha
        {
            get { return var_fecha; }
            set { var_fecha = value; }
        }
        #endregion

    }
}