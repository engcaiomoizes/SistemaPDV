namespace Sistema.Forms
{
    partial class FrmConfiguracoes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlBtnDados = new System.Windows.Forms.Panel();
            this.btnDados = new System.Windows.Forms.Button();
            this.pnlBtnGeral = new System.Windows.Forms.Panel();
            this.btnGeral = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.pnlBtnTema = new System.Windows.Forms.Panel();
            this.btnTema = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.pnlBtnDados.SuspendLayout();
            this.pnlBtnGeral.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlBtnTema.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 30);
            this.panel1.TabIndex = 8;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(480, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "CONFIGURAÇÕES";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.pnlMenu.Controls.Add(this.pnlBtnTema);
            this.pnlMenu.Controls.Add(this.pnlBtnDados);
            this.pnlMenu.Controls.Add(this.pnlBtnGeral);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenu.Location = new System.Drawing.Point(0, 30);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(480, 40);
            this.pnlMenu.TabIndex = 9;
            // 
            // pnlBtnDados
            // 
            this.pnlBtnDados.Controls.Add(this.btnDados);
            this.pnlBtnDados.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBtnDados.Location = new System.Drawing.Point(100, 0);
            this.pnlBtnDados.Name = "pnlBtnDados";
            this.pnlBtnDados.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.pnlBtnDados.Size = new System.Drawing.Size(100, 40);
            this.pnlBtnDados.TabIndex = 3;
            // 
            // btnDados
            // 
            this.btnDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDados.FlatAppearance.BorderSize = 0;
            this.btnDados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDados.ForeColor = System.Drawing.Color.White;
            this.btnDados.Location = new System.Drawing.Point(0, 2);
            this.btnDados.Name = "btnDados";
            this.btnDados.Size = new System.Drawing.Size(100, 36);
            this.btnDados.TabIndex = 0;
            this.btnDados.Text = "Dados";
            this.btnDados.UseVisualStyleBackColor = true;
            this.btnDados.Click += new System.EventHandler(this.btnDados_Click);
            // 
            // pnlBtnGeral
            // 
            this.pnlBtnGeral.Controls.Add(this.btnGeral);
            this.pnlBtnGeral.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBtnGeral.Location = new System.Drawing.Point(0, 0);
            this.pnlBtnGeral.Name = "pnlBtnGeral";
            this.pnlBtnGeral.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.pnlBtnGeral.Size = new System.Drawing.Size(100, 40);
            this.pnlBtnGeral.TabIndex = 2;
            // 
            // btnGeral
            // 
            this.btnGeral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGeral.FlatAppearance.BorderSize = 0;
            this.btnGeral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeral.ForeColor = System.Drawing.Color.White;
            this.btnGeral.Location = new System.Drawing.Point(0, 2);
            this.btnGeral.Name = "btnGeral";
            this.btnGeral.Size = new System.Drawing.Size(100, 36);
            this.btnGeral.TabIndex = 0;
            this.btnGeral.Text = "Geral";
            this.btnGeral.UseVisualStyleBackColor = true;
            this.btnGeral.Click += new System.EventHandler(this.btnGeral_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 70);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(480, 450);
            this.pnlContainer.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.btnSalvar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 520);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(480, 80);
            this.panel2.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(252, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 21;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.btnSalvar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(368, 26);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.TabIndex = 20;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // pnlBtnTema
            // 
            this.pnlBtnTema.Controls.Add(this.btnTema);
            this.pnlBtnTema.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBtnTema.Location = new System.Drawing.Point(200, 0);
            this.pnlBtnTema.Name = "pnlBtnTema";
            this.pnlBtnTema.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.pnlBtnTema.Size = new System.Drawing.Size(100, 40);
            this.pnlBtnTema.TabIndex = 4;
            // 
            // btnTema
            // 
            this.btnTema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTema.FlatAppearance.BorderSize = 0;
            this.btnTema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTema.ForeColor = System.Drawing.Color.White;
            this.btnTema.Location = new System.Drawing.Point(0, 2);
            this.btnTema.Name = "btnTema";
            this.btnTema.Size = new System.Drawing.Size(100, 36);
            this.btnTema.TabIndex = 0;
            this.btnTema.Text = "Tema";
            this.btnTema.UseVisualStyleBackColor = true;
            this.btnTema.Click += new System.EventHandler(this.btnTema_Click);
            // 
            // FrmConfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmConfiguracoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmConfiguracoes";
            this.Load += new System.EventHandler(this.FrmConfiguracoes_Load);
            this.panel1.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            this.pnlBtnDados.ResumeLayout(false);
            this.pnlBtnGeral.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlBtnTema.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlBtnGeral;
        private System.Windows.Forms.Button btnGeral;
        private System.Windows.Forms.Panel pnlBtnDados;
        private System.Windows.Forms.Button btnDados;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Panel pnlBtnTema;
        private System.Windows.Forms.Button btnTema;
    }
}