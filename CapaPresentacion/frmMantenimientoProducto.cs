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
    public partial class frmMantenimientoProducto : Form
    {
        //Variable que nos indica si vamos a insertar un nuevo producto
        private bool nuevo = false;
        //Variable que nos indica si vamos a modificar un producto
        private bool modificar = false;
        //Constructor del formulario
        public frmMantenimientoProducto()
        {
            InitializeComponent();
        }
        //Evento que se lanza cuando se va a mostrar el formulario
        private void frmMantenimientoProducto_Load(object sender, EventArgs e)
        {
            //Para ubicar al formulario en la parte superior del contenedor
            this.Top = 0;
            this.Left = 0;
            //Le decimos al DataGridView que no auto genere las columnas
            this.dgvProductos.AutoGenerateColumns = false;
            //Llenamos el DataGridView con la informacion de todos nuestros 
            //productos
            this.dgvProductos.DataSource = NegProducto.ObtenerProducto();
            //Deshabilita los controles
            this.habilitar(false);
            //Establece los botones
            this.botones();
        }
        //Para mostrar mensaje de confirmacion
        private void mOK(string men)
        {
            MessageBox.Show(men, "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Para mostrar mensaje de error
        private void mError(string men)
        {
            MessageBox.Show(men, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //Limpia los controles del formulario
        private void limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.nudPrecio.Value = 0;
        }
        //Habilita los controles de los formularios
        private void habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.nudPrecio.Enabled = valor; 
        }
        //Habilita los botones
        private void botones()
        {
            if (this.nuevo || this.modificar)
            {
                this.habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnModificar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnModificar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }
        //Evento clic del boton btnNuevo
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.nuevo=true;
            this.modificar=false;
            this.botones();
            this.limpiar();
            this.txtCodigo.Text = string.Empty;
            this.txtNombre.Focus();
        }
        //Evento clic del boton btnGuardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //La variable que almacena si se inserto o se modifico la tabla
            string rpta = "";
            if(this.nuevo)
            {
                //Vamos a insertar un producto 
                rpta=NegProducto.Insertar(this.txtNombre.Text.Trim().ToUpper(),
                                          this.nudPrecio.Value);
                
            }else
            {
                //Vamos a modificar un producto
                rpta=NegProducto.Actualizar(Convert.ToInt32(this.txtCodigo.Text),
                                            this.txtNombre.Text.Trim().ToUpper(),
                                            this.nudPrecio.Value);
            }
            //Si la respuesta fue OK, fue porque se modifico o inserto el Producto
            //de forma correcta
            if (rpta.Equals("OK"))
            {
                if (this.nuevo)
                {
                    this.mOK("Se inserto de forma correcta al Producto");
                }
                else 
                {
                    this.mOK("Se actualizo de forma correcta al Producto");
                }
                
            }
            else
            {
                //Mostramos el mensaje de error
                this.mError(rpta);
            }
            this.nuevo=false;
            this.modificar=false;
            this.botones();
            this.limpiar();
            this.dgvProductos.DataSource = NegProducto.ObtenerProducto();
            this.txtCodigo.Text="";
        }
        //Evento clic del boton btnModificar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Si no ha seleccionado un producto no puede modificar
            if(!this.txtCodigo.Text.Equals(""))
            {
                this.modificar=true;
                this.botones();
            }
            else
            {
                this.mError("Debe de buscar un producto para Modificar");
            }
        }
        //Evento clic del boton btnCancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.nuevo=false;
            this.modificar=false;
            this.botones();
            this.limpiar();
            this.txtCodigo.Text=string.Empty;
        }
        //Evento double clic del DataGridView de Productos
        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            this.txtCodigo.Text = Convert.ToString(this.dgvProductos.CurrentRow.Cells["codigoProducto"].Value);
            this.txtNombre.Text = Convert.ToString(this.dgvProductos.CurrentRow.Cells["nombre"].Value);
            this.nudPrecio.Value = Convert.ToDecimal(this.dgvProductos.CurrentRow.Cells["precio"].Value);
            this.tabControl.SelectedIndex = 1;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
