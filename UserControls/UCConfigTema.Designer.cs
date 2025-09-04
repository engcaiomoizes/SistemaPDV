namespace Sistema.UserControls
{
    partial class UCConfigTema
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCorPrimaria = new System.Windows.Forms.Panel();
            this.pnlCorSecundaria = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.selectColor = new System.Windows.Forms.ColorDialog();
            this.pnlBgTitulos = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPadraoPrimaria = new System.Windows.Forms.Button();
            this.btnPadraoSecundaria = new System.Windows.Forms.Button();
            this.btnPadraoTitulosBg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(117, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cor Primária";
            // 
            // pnlCorPrimaria
            // 
            this.pnlCorPrimaria.BackColor = System.Drawing.Color.White;
            this.pnlCorPrimaria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCorPrimaria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCorPrimaria.Location = new System.Drawing.Point(61, 49);
            this.pnlCorPrimaria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCorPrimaria.Name = "pnlCorPrimaria";
            this.pnlCorPrimaria.Size = new System.Drawing.Size(33, 30);
            this.pnlCorPrimaria.TabIndex = 3;
            this.pnlCorPrimaria.Click += new System.EventHandler(this.pnlCorPrimaria_Click);
            // 
            // pnlCorSecundaria
            // 
            this.pnlCorSecundaria.BackColor = System.Drawing.Color.White;
            this.pnlCorSecundaria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCorSecundaria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCorSecundaria.Location = new System.Drawing.Point(61, 103);
            this.pnlCorSecundaria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCorSecundaria.Name = "pnlCorSecundaria";
            this.pnlCorSecundaria.Size = new System.Drawing.Size(33, 30);
            this.pnlCorSecundaria.TabIndex = 5;
            this.pnlCorSecundaria.Click += new System.EventHandler(this.pnlCorSecundaria_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(117, 108);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cor Secundária";
            // 
            // pnlBgTitulos
            // 
            this.pnlBgTitulos.BackColor = System.Drawing.Color.White;
            this.pnlBgTitulos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBgTitulos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlBgTitulos.Location = new System.Drawing.Point(61, 158);
            this.pnlBgTitulos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBgTitulos.Name = "pnlBgTitulos";
            this.pnlBgTitulos.Size = new System.Drawing.Size(33, 30);
            this.pnlBgTitulos.TabIndex = 7;
            this.pnlBgTitulos.Click += new System.EventHandler(this.pnlBgTitulos_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(117, 162);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Background Títulos";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 517);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(640, 37);
            this.label4.TabIndex = 8;
            this.label4.Text = "É necessário reiniciar o sistema para atualizar o tema.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPadraoPrimaria
            // 
            this.btnPadraoPrimaria.AutoSize = true;
            this.btnPadraoPrimaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPadraoPrimaria.ForeColor = System.Drawing.Color.White;
            this.btnPadraoPrimaria.Location = new System.Drawing.Point(445, 49);
            this.btnPadraoPrimaria.Name = "btnPadraoPrimaria";
            this.btnPadraoPrimaria.Size = new System.Drawing.Size(130, 30);
            this.btnPadraoPrimaria.TabIndex = 9;
            this.btnPadraoPrimaria.Text = "Restaurar padrão";
            this.btnPadraoPrimaria.UseVisualStyleBackColor = true;
            this.btnPadraoPrimaria.Click += new System.EventHandler(this.btnPadraoPrimaria_Click);
            // 
            // btnPadraoSecundaria
            // 
            this.btnPadraoSecundaria.AutoSize = true;
            this.btnPadraoSecundaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPadraoSecundaria.ForeColor = System.Drawing.Color.White;
            this.btnPadraoSecundaria.Location = new System.Drawing.Point(445, 103);
            this.btnPadraoSecundaria.Name = "btnPadraoSecundaria";
            this.btnPadraoSecundaria.Size = new System.Drawing.Size(130, 30);
            this.btnPadraoSecundaria.TabIndex = 10;
            this.btnPadraoSecundaria.Text = "Restaurar padrão";
            this.btnPadraoSecundaria.UseVisualStyleBackColor = true;
            this.btnPadraoSecundaria.Click += new System.EventHandler(this.btnPadraoSecundaria_Click);
            // 
            // btnPadraoTitulosBg
            // 
            this.btnPadraoTitulosBg.AutoSize = true;
            this.btnPadraoTitulosBg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPadraoTitulosBg.ForeColor = System.Drawing.Color.White;
            this.btnPadraoTitulosBg.Location = new System.Drawing.Point(445, 158);
            this.btnPadraoTitulosBg.Name = "btnPadraoTitulosBg";
            this.btnPadraoTitulosBg.Size = new System.Drawing.Size(130, 30);
            this.btnPadraoTitulosBg.TabIndex = 11;
            this.btnPadraoTitulosBg.Text = "Restaurar padrão";
            this.btnPadraoTitulosBg.UseVisualStyleBackColor = true;
            this.btnPadraoTitulosBg.Click += new System.EventHandler(this.btnPadraoTitulosBg_Click);
            // 
            // UCConfigTema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.btnPadraoTitulosBg);
            this.Controls.Add(this.btnPadraoSecundaria);
            this.Controls.Add(this.btnPadraoPrimaria);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlBgTitulos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlCorSecundaria);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlCorPrimaria);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UCConfigTema";
            this.Size = new System.Drawing.Size(640, 554);
            this.Load += new System.EventHandler(this.UCConfigTema_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCorPrimaria;
        private System.Windows.Forms.Panel pnlCorSecundaria;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog selectColor;
        private System.Windows.Forms.Panel pnlBgTitulos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPadraoPrimaria;
        private System.Windows.Forms.Button btnPadraoSecundaria;
        private System.Windows.Forms.Button btnPadraoTitulosBg;
    }
}
