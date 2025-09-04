using Sistema.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms
{
    public partial class FrmDesconto : Form
    {
        public decimal valor {  get; set; }
        public decimal subtotal { get; set; }
        
        public FrmDesconto()
        {
            InitializeComponent();
        }

        private void FrmDesconto_Load(object sender, EventArgs e)
        {
            //
        }

        public void Desconto(decimal subtotal, decimal desconto)
        {
            lblTitulo.Text = "Desconto";
            lblTipo.Text = "Desconto:";
            this.subtotal = subtotal;
            lblSubtotal.Text = "R$ " + this.subtotal.ToString("n2");
            txtDesconto.Text = desconto.ToString("n2");
        }

        public void Acrescimo(decimal subtotal, decimal acrescimo)
        {
            lblTitulo.Text = "Acréscimo";
            lblTipo.Text = "Acréscimo:";
            this.subtotal = subtotal;
            lblSubtotal.Text = "R$ " + this.subtotal.ToString("n2");
            txtDesconto.Text = acrescimo.ToString("n2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rdbPorcentagem.Checked)
            {
                valor = Convert.ToDecimal(txtDesconto.Text) / 100 * subtotal;
            } else
            {
                valor = Convert.ToDecimal(txtDesconto.Text);
            }
        }

        private void rdbValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbValor.Checked)
            {
                lblDesconto.Text = "R$";
            } else
            {
                //
            }
        }

        private void rdbPorcentagem_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPorcentagem.Checked)
            {
                lblDesconto.Text = "%";
            } else
            {
                //
            }
        }

        private void FrmDesconto_Shown(object sender, EventArgs e)
        {
            txtDesconto.Focus();
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOK.Focus();
            }
        }
    }
}
