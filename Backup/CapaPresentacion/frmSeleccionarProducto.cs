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
    public partial class frmSeleccionarProducto : Form
    {
        //El formulario padre
        private frmRegistrarVenta frame;
        //El constructor del formulario
        public frmSeleccionarProducto()
        {
            InitializeComponent();
        }
        //Establece los valores del formulario padre
        public void estableceFormulario(frmRegistrarVenta frame)
        {
            this.frame = frame;
        }
        //Evento que se ejecuta cuando se muestra el formulario
        private void frmSeleccionarProducto_Load(object sender, EventArgs e)
        {
            //Que no se genere las columnas de forma automatica
            this.dgvProducto.AutoGenerateColumns = false;
            //Obtiene todos los productos y lo asigana al DataGridView
            this.dgvProducto.DataSource = NegProducto.ObtenerProducto();
        }
        //Evento double clic del DataGridView
        private void dgvProducto_DoubleClick(object sender, EventArgs e)
        {
            //Estableciendo los datos a las cajas de texto del formulario padre
            this.frame.codigoProductoSeleccionado = Convert.ToInt32(this.dgvProducto.CurrentRow.Cells["codigoProducto"].Value);
            this.frame.txtProducto.Text = Convert.ToString(this.dgvProducto.CurrentRow.Cells["nombre"].Value);
            this.frame.txtPrecio.Text = Convert.ToString(this.dgvProducto.CurrentRow.Cells["precio"].Value);
            //Cerrando el formulario
            this.Hide();
        }

    }
}
