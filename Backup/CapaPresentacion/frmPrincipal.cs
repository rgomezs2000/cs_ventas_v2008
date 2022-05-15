using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void abrirFormulario(Form f)
        {
            f.MdiParent = this;
            f.Icon = this.Icon;
            f.Show();
        }

        private void mantenimientoProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.abrirFormulario(new frmMantenimientoProducto());
        }

        private void registrarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.abrirFormulario(new frmRegistrarVenta());
        }

        private void reporteVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.abrirFormulario(new frmConsultarVenta());
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
