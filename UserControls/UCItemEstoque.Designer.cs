namespace Sistema.UserControls
{
    partial class UCItemEstoque
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblProduto = new System.Windows.Forms.Label();
            this.lblEstoque = new System.Windows.Forms.Label();
            this.lblNovoValor = new System.Windows.Forms.Label();
            this.lblEstoqueFinal = new System.Windows.Forms.Label();
            this.btnExcluir = new System.Windows.Forms.PictureBox();
            this.btnEditar = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcluir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditar)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.Controls.Add(this.btnExcluir, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEstoqueFinal, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNovoValor, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblProduto, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEstoque, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEditar, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(950, 40);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblProduto
            // 
            this.lblProduto.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblProduto.ForeColor = System.Drawing.Color.White;
            this.lblProduto.Location = new System.Drawing.Point(3, 0);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Padding = new System.Windows.Forms.Padding(6);
            this.lblProduto.Size = new System.Drawing.Size(425, 40);
            this.lblProduto.TabIndex = 0;
            this.lblProduto.Text = "label1";
            this.lblProduto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEstoque
            // 
            this.lblEstoque.AutoSize = true;
            this.lblEstoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstoque.ForeColor = System.Drawing.Color.White;
            this.lblEstoque.Location = new System.Drawing.Point(479, 0);
            this.lblEstoque.Name = "lblEstoque";
            this.lblEstoque.Size = new System.Drawing.Size(124, 40);
            this.lblEstoque.TabIndex = 1;
            this.lblEstoque.Text = "label1";
            this.lblEstoque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNovoValor
            // 
            this.lblNovoValor.AutoSize = true;
            this.lblNovoValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNovoValor.ForeColor = System.Drawing.Color.White;
            this.lblNovoValor.Location = new System.Drawing.Point(609, 0);
            this.lblNovoValor.Name = "lblNovoValor";
            this.lblNovoValor.Size = new System.Drawing.Size(124, 40);
            this.lblNovoValor.TabIndex = 2;
            this.lblNovoValor.Text = "label2";
            this.lblNovoValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEstoqueFinal
            // 
            this.lblEstoqueFinal.AutoSize = true;
            this.lblEstoqueFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstoqueFinal.ForeColor = System.Drawing.Color.White;
            this.lblEstoqueFinal.Location = new System.Drawing.Point(739, 0);
            this.lblEstoqueFinal.Name = "lblEstoqueFinal";
            this.lblEstoqueFinal.Size = new System.Drawing.Size(124, 40);
            this.lblEstoqueFinal.TabIndex = 3;
            this.lblEstoqueFinal.Text = "label3";
            this.lblEstoqueFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = global::Sistema.Properties.Resources.Trash;
            this.btnExcluir.Location = new System.Drawing.Point(909, 3);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(34, 34);
            this.btnExcluir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnExcluir.TabIndex = 5;
            this.btnExcluir.TabStop = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::Sistema.Properties.Resources.Edit;
            this.btnEditar.Location = new System.Drawing.Point(869, 3);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(34, 34);
            this.btnEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnEditar.TabIndex = 4;
            this.btnEditar.TabStop = false;
            // 
            // UCItemEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCItemEstoque";
            this.Size = new System.Drawing.Size(950, 40);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcluir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.Label lblEstoqueFinal;
        private System.Windows.Forms.Label lblNovoValor;
        private System.Windows.Forms.Label lblEstoque;
        private System.Windows.Forms.PictureBox btnEditar;
        private System.Windows.Forms.PictureBox btnExcluir;
    }
}
