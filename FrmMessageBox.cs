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

namespace Sistema
{
    public partial class FrmMessageBox : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        private readonly ConfigDAO configDAO = new ConfigDAO();

        public string cpf {  get; set; }

        public FrmMessageBox()
        {
            InitializeComponent();
            //this.ShowDialog();
        }

        public void Show(string Msg, string Title)
        {
            lblTexto.Visible = true;
            lblTexto.Text = Msg.ToString();
            lblTitulo.Text = Title.ToString();
            btnOK.Visible = true;
            btnYes.Visible = false;
            btnNo.Visible = false;
            lblCliente.Visible = false;
            txtCliente.Visible = false;
            this.ShowDialog();
        }

        public void Show(string Msg, string Title, bool blur)
        {
            lblTexto.Visible = true;
            lblTexto.Text = Msg.ToString();
            lblTitulo.Text = Title.ToString();
            btnOK.Visible = true;
            btnYes.Visible = false;
            btnNo.Visible = false;
            lblCliente.Visible = false;
            txtCliente.Visible = false;
            if (!blur)
                this.ShowDialog();
            else
                MainClass.BlurBackground(this);
        }

        public void Confirm(string Msg, string Title, bool blur)
        {
            lblTexto.Visible = true;
            lblTexto.Text = Msg.ToString();
            lblTitulo.Text = Title.ToString();
            btnOK.Visible = false;
            btnYes.Visible = true;
            btnNo.Visible = true;
            lblCliente.Visible = false;
            txtCliente.Visible = false;
            if (!blur)
                this.ShowDialog();
            else
                MainClass.BlurBackground(this);
        }

        public void Cliente(string Title, bool blur)
        {
            lblCliente.Visible = true;
            txtCliente.Visible = true;
            btnOK.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
            lblTexto.Visible = false;
            lblTitulo.Text = Title.ToString();
            //this.ShowDialog();
            if (!blur)
                this.ShowDialog();
            else
                MainClass.BlurBackground(this);
            this.txtCliente.Focus();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                lastLocation = e.Location;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y
                );
                this.Update();
            }
        }

        private void lblTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                lastLocation = e.Location;
            }
        }

        private void lblTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void lblTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y
                );
                this.Update();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                FrmMessageBox mb = new FrmMessageBox();
                ClienteDAO clienteDAO = new ClienteDAO();

                if (txtCliente.Text.Trim() == "")
                {
                    if (configDAO.VerificarCliente())
                    {
                        mb.Show("Informe um cliente.", "Atenção", true);
                        return;
                    } else
                    {
                        cpf = null;
                        this.DialogResult = DialogResult.OK;
                    }
                } else
                {
                    if (clienteDAO.ExisteCliente(txtCliente.Text))
                    {
                        cpf = txtCliente.Text;
                        this.DialogResult = DialogResult.OK;
                    } else
                    {
                        mb.Show("Informe um cliente.", "Atenção", true);
                        return;
                    }
                }
            } else if (e.KeyChar == 27)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
