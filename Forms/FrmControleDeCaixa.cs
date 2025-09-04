using Sistema.DAO;
using Sistema.Model;
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
    public partial class FrmControleDeCaixa : Form
    {
        private Caixa caixa;
        private readonly CaixaDAO dao = new CaixaDAO();
        private readonly MetodoDAO metodoDAO = new MetodoDAO();
        private readonly List<Metodo> metodos;
        private Size size = new Size(425, 30);

        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.FromArgb(60, 80, 150);
        //private Color borderColor = Color.FromArgb(224, 224, 224);

        public FrmControleDeCaixa()
        {
            InitializeComponent();
            this.metodos = metodoDAO.GetMetodos();
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000;
                return cp;
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ControlRegionAndBorder(Control control, float radius, Graphics graph, Color borderColor)
        {
            using (GraphicsPath roundPath = GetRoundedPath(control.ClientRectangle, radius))
            using (Pen penBorder = new Pen(borderColor, 1))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                control.Region = new Region(roundPath);
                graph.DrawPath(penBorder, roundPath);
            }
        }

        private void FrmControleDeCaixa_Load(object sender, EventArgs e)
        {
            caixa = dao.ObterCaixaAberto();
            lblPeriodo.Text = "De ";
            lblPeriodo.Text += caixa.data_abertura.ToString();
            lblPeriodo.Text += " até ";
            if (caixa.data_fechamento != null)
            {
                lblPeriodo.Text += caixa.data_fechamento.ToString();
            } else
            {
                lblPeriodo.Text += "o momento";
            }

            lblTotalVendas.Text = dao.ObterNumeroTotalMovimentos().ToString();
            lblValorTotal.Text = "R$ " + dao.ObterValorTotalMovimentos().ToString("n2");

            PreencherPainel();
        }

        private void PreencherPainel()
        {            
            int i = 15, j = 40;

            int panelHeight = panel5.Height;

            panelHeight += (40 * dao.ObterQuantidadeMetodosDistintos());

            panel5.Size = new Size(panel5.Width, panelHeight);

            foreach (Metodo metodo in metodos)
            {
                decimal total = dao.ObterValorTotalPorMetodo(metodo.id);

                if (total > 0)
                {
                    //panel5.Size = new Size(panel5.Width, panel5.Height + 40);

                    Panel pnl = new Panel();
                    pnl.Size = size;
                    pnl.Location = new Point(i, j + 40);
                    //pnl.BackColor = Color.White;
                    pnl.Paint += panels_Paint;
                    pnl.Padding = new Padding(10, 0, 0, 0);
                    panel5.Controls.Add(pnl);

                    Label lbl = new Label();
                    lbl.Dock = DockStyle.Left;
                    lbl.AutoSize = false;
                    lbl.Width = (pnl.Width / 2) - 20;
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    lbl.Font = new Font(lbl.Font.FontFamily, 10, FontStyle.Bold);
                    lbl.ForeColor = Color.White;
                    lbl.Text = metodo.nome;
                    pnl.Controls.Add(lbl);

                    Label lbl2 = new Label();
                    lbl2.Dock = DockStyle.Right;
                    lbl2.TextAlign = ContentAlignment.MiddleRight;
                    lbl2.Font = new Font(lbl2.Font.FontFamily, 10, FontStyle.Bold);
                    lbl2.ForeColor = Color.White;
                    lbl2.AutoSize = false;
                    lbl.Width = (pnl.Width / 2) - 20;
                    lbl2.Text = "R$ " + total.ToString("n2");
                    pnl.Controls.Add(lbl2);

                    j += 40;
                }
            }
        }

        private void pnlResumoVendas_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(pnlResumoVendas, borderRadius, e.Graphics, borderColor);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(panel3, 10, e.Graphics, borderColor);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(panel4, 10, e.Graphics, borderColor);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(panel5, 10, e.Graphics, borderColor);
        }

        private int tempIndex;

        private Color SelectColor()
        {
            Random random = new Random();
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void panels_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = (Panel)sender;
            if (pnl.BorderStyle == BorderStyle.None)
            {
                int thickness = 4;
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(SelectColor(), thickness))
                {
                    e.Graphics.DrawLine(p, halfThickness, 0, halfThickness, pnl.Size.Height);
                }
            }
        }

        private void btnFecharCaixa_Click(object sender, EventArgs e)
        {
            FrmMessageBox mb = new FrmMessageBox();
            mb.Confirm("Deseja fechar o caixa atual?", "Controle de Caixa", true);
            if (mb.DialogResult == DialogResult.Yes)
            {
                if (dao.FecharCaixa())
                {
                    FrmMessageBox mb2 = new FrmMessageBox();
                    mb2.Show("Caixa fechado com sucesso!", "Fechamento de Caixa", true);
                    Close();
                }
            }
        }
    }
}
