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
    public partial class frmConsultarVenta : Form
    {
        public frmConsultarVenta()
        {
            InitializeComponent();
        }

        private void frmConsultarVenta_Load(object sender, EventArgs e)
        {
            this.dgvVentas.AutoGenerateColumns = false;
            this.dgvVentas.DataSource = NegVenta.ObtenerVenta();
        }
    }
}
