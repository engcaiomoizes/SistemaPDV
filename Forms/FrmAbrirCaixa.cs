using Sistema.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms
{
    public partial class FrmAbrirCaixa : Form
    {
        private decimal valor;

        private readonly CaixaDAO dao = new CaixaDAO();
        
        public FrmAbrirCaixa()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (panel2.BorderStyle == BorderStyle.None)
            {
                int thickness = 2;
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.White, thickness))
                {
                    e.Graphics.DrawLine(p, 0, 40, 510, 40);
                }
            }
        }

        private void PreencherValor()
        {
            txtValor.Text = valor.ToString("n2");
        }

        private void FrmAbrirCaixa_Load(object sender, EventArgs e)
        {
            valor = 0;
            PreencherValor();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            valor += 2;
            PreencherValor();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            valor += 5;
            PreencherValor();
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            valor += 10;
            PreencherValor();
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            valor += 20;
            PreencherValor();
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            valor += 50;
            PreencherValor();
        }

        private void btn100_Click(object sender, EventArgs e)
        {
            valor += 100;
            PreencherValor();
        }

        private void pnlApelido_Paint(object sender, PaintEventArgs e)
        {
            if (pnlApelido.BorderStyle == BorderStyle.None)
            {
                int thickness = 2;
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.White, thickness))
                {
                    e.Graphics.DrawLine(p, 0, 24, 510, 24);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtApelido.Text) || string.IsNullOrEmpty(txtValor.Text))
            {
                //
            } else
            {
                if (dao.AbrirCaixa(txtApelido.Text, valor))
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
