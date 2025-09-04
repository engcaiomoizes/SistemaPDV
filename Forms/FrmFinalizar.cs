using Sistema.DAO;
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
    public partial class FrmFinalizar : Form
    {
        private readonly MetodoDAO dao = new MetodoDAO();
        private List<Metodo> metodos = new List<Metodo>();

        public int metodo {  get; set; }
        public string observacoes { get; set; }
        
        public FrmFinalizar()
        {
            InitializeComponent();
        }

        private void FrmFinalizar_Load(object sender, EventArgs e)
        {
            metodos = dao.GetMetodos();

            if (metodos != null && metodos.Count > 0)
            {
                int i = 30, j = 10;
                for (int x = 0; x < metodos.Count; x++)
                {
                    Button btn = new Button();
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.White;
                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.FromArgb(211, 84, 0);
                    btn.Size = new Size(150, 40);
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Text = metodos[x].nome;
                    btn.Font = new Font(btn.Font.FontFamily, 9.0f, FontStyle.Bold);
                    btn.Tag = metodos[x].id;
                    btn.Click += metodos_Click;
                    btn.Location = new Point(i, j);
                    pnlMetodos.Controls.Add(btn);

                    i += 164;

                    if (i > 344)
                        j += 50;

                    if (i > 344)
                    {
                        i = 30;
                    }

                    if (j >= 260)
                        break;
                }
            }
        }

        public void ExibirTotal(decimal total)
        {
            txtTotal.Text = "R$ " + total.ToString("n2");
        }

        private void metodos_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            metodo = Convert.ToInt32(btn.Tag);

            foreach (Button botao in pnlMetodos.Controls)
            {
                botao.BackColor = Color.FromArgb(211, 84, 0);
            }

            btn.BackColor = Color.FromArgb(0, 156, 136);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (metodo == null || metodo == 0)
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Selecione um método de pagamento!", "Atenção");
            } else
            {
                observacoes = rtbObservacoes.Text;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
