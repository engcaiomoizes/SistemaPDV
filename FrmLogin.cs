using FirebirdSql.Data.FirebirdClient;
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
    public partial class FrmLogin : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        
        public FrmLogin()
        {
            InitializeComponent();
        }

        private bool callLogin()
        {
            if (txtUsuario.Text.Trim() == "" || txtSenha.Text.Trim() == "")
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Confirm("Dados inválidos!", "Atenção", false);

                return false;
            }

            using (Conexao con = new Conexao())
            {
                string sql = "SELECT u.*, f.NOME FROM USUARIOS u INNER JOIN FUNCIONARIOS f ON f.ID = u.FUNCIONARIO WHERE u.LOGIN = @usuario AND u.SENHA = @senha AND u.ATIVO = 'T'";
                using (FbCommand cmd = new FbCommand(sql, con.GetConnection()))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Program.NomeUsuario = Convert.ToString(reader["NOME"]);
                            Program.LoginUsuario = Convert.ToString(reader["LOGIN"]);
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (callLogin())
            {
                FrmPrincipal frmPrincipal = new FrmPrincipal();
                frmPrincipal.Show();
                this.Hide();
            } else
            {
                FrmMessageBox mb = new FrmMessageBox();
                mb.Show("Usuário inválido!", "Atenção", false);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnEntrar.Focus();
            }
        }

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                lastLocation = e.Location;
            }
        }

        private void FrmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y
                );
                this.Update();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
