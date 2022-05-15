using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Comunicarse con la Capa de Negocios
using CapaNegocios;

namespace CapaPresentacion
{
    public partial class frmRegistrarVenta : Form
    {
        //DataTable que se encargara de guardar el detalle de la venta
        //de forma temporal
        private DataTable dtDetalle;
        //Codigo del producto seleccionado
        internal int codigoProductoSeleccionado = -1;
        //Variable que almacena el total de la venta
        private decimal totalPagar = 0;
        //El constructor de la clase
        public frmRegistrarVenta()
        {
            InitializeComponent();
        }
        //Metodo que se ejecuta al cargar el formulario
        private void frmRegistrarVenta_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.crearTabla();
            this.WindowState = FormWindowState.Maximized;
        }
        //Limpia todos los controles del formulario
        private void limpiarControles()
        {
            this.txtCliente.Text = string.Empty;
            this.codigoProductoSeleccionado = -1;
            this.txtProducto.Text = string.Empty;
            this.txtPrecio.Text = string.Empty;
            this.nudCantidad.Value = 1;
            this.crearTabla();
            this.lblTotalPagar.Text = "Total Pagar: S/. 0.00";
            
        }
        //Crea la tabla de Detalle 
        private void crearTabla()
        {
            //Crea la tabla con el nombre de Detalle
            this.dtDetalle = new DataTable("Detalle");
            //Agrega las columnas que tendra la tabla
            this.dtDetalle.Columns.Add("codigoProducto", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("Producto", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("cantidad", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("PU", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("Descuento", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("subTotal", System.Type.GetType("System.Decimal"));
            //Relacionamos nuestro datagridview con nuestro datatable
            this.dgvDetalle.DataSource = this.dtDetalle;

        }
        //Para mostrar mensaje de error
        private void mError(string mensaje)
        {
            MessageBox.Show(this, mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //Para mostrar mensaje de confirmación
        private void mOk(string mensaje)
        {
            MessageBox.Show(this, mensaje, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Evento del clic del boton btnBuscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Creamos una variable del tipo del formulario que deseamos abrir
            frmSeleccionarProducto frame = new frmSeleccionarProducto();
            //Le pasamos como datos la información de nuestro formulario
            frame.estableceFormulario(this);
            //Mostrar el formulario que tiene los productos que hemos seleccionado
            frame.ShowDialog();
        }
        //Evento clic del boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Valida que hemos seleccionado algun producto
            if (this.codigoProductoSeleccionado == -1)
            {
                this.mError("No ha seleccionado aun ningun producto");
            }
            else
            {
                //Variable que va a indicar si podemos registrar el detalle
                bool registrar = true;
                foreach (DataRow row in dtDetalle.Rows)
                {
                    if (Convert.ToInt32(row["codigoProducto"]) == this.codigoProductoSeleccionado)
                    {
                        registrar = false;
                        this.mError("Ya se encuentra el producto en el detalle");
                    }
                }
                //Si podemos registrar el producto en el detalle
                if (registrar)
                {
                    //Calculamos el sub total del detalle sin descuento
                    decimal subTotal = Convert.ToDecimal(this.txtPrecio.Text) * nudCantidad.Value;
                    //Obtenemos el descuento
                    decimal descuento = NegDetalleVenta.ObtenerDescuento(
                                            nudCantidad.Value,
                                            Convert.ToDecimal(this.txtPrecio.Text));
                    //Actualizamos el sub total con el descuento correspondiente
                    subTotal = subTotal - descuento;
                    //Aumentamos el total a pagar
                    this.totalPagar += subTotal;
                    this.lblTotalPagar.Text = "Total Pagar: S/." + totalPagar.ToString("#0.00#");
                    //Agregamos al fila a nuestro datatable
                    DataRow row = this.dtDetalle.NewRow();
                    row["codigoProducto"] = this.codigoProductoSeleccionado;
                    row["Producto"] = this.txtProducto.Text;
                    row["cantidad"] = this.nudCantidad.Value;
                    row["PU"] = this.txtPrecio.Text ;
                    row["Descuento"] = descuento;
                    row["subTotal"] = subTotal;
                    this.dtDetalle.Rows.Add(row);
                }
            }
        }
        //Evento click del boton quitar
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                //Indice dila actualmente seleccionado y que vamos a eliminar
                int indiceFila = this.dgvDetalle.CurrentCell.RowIndex;
                //Fila que vamos a eliminar
                DataRow row = this.dtDetalle.Rows[indiceFila];
                //Disminuimos el total a pagar
                this.totalPagar = this.totalPagar - Convert.ToDecimal(row["subTotal"].ToString());
                this.lblTotalPagar.Text = "Total Pagar: S/." + totalPagar.ToString("#0.00#"); 
                //Removemos la fila
                this.dtDetalle.Rows.Remove(row);
            }
            catch (Exception )
            {
                mError("No hay fila para remover");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Debe de tener por almenos un detalle para poder registrar
            if (this.dtDetalle.Rows.Count > 0)
            {
                string rpta = NegVenta.Insertar(this.txtCliente.Text, this.dtDetalle);
                if (rpta.Equals("OK"))
                {
                    mOk("Se inserto de manera correcta la venta");
                    this.limpiarControles();
                }
                else
                {
                    mError(rpta);
                }
            }
            else
            {
                mError("No agregado ningun detalle");
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                //Creamos el documento 
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt=new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Obtenemos el documento que se encuentra en nustra carpeta bin\debug\crReporte.rpt
                rpt.Load( Application.StartupPath + "\\crReporte.rpt");
                //Lleanamos el reporte con la información que obtenemos de la base de datos
                rpt.SetDataSource(NegVenta.ObtenerVenta(Convert.ToInt32(this.txtCodigoVenta.Text)));
                //Establecemos los datos al reporte
                this.crvReporte.ReportSource=rpt;
                //Refrescamos nuestro reporte
                this.crvReporte.RefreshReport();
            }
            catch (Exception )
            {
            }
        }
        
        
    }
}
