namespace File_Log_Form
{
    partial class Form1
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
            this.btnSalvar = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtCaminho = new System.Windows.Forms.TextBox();
            this.btnAbrirPasta = new System.Windows.Forms.Button();
            this.listResult = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(14, 63);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "Iniciar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txtCaminho
            // 
            this.txtCaminho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaminho.Location = new System.Drawing.Point(112, 18);
            this.txtCaminho.Name = "txtCaminho";
            this.txtCaminho.Size = new System.Drawing.Size(809, 20);
            this.txtCaminho.TabIndex = 1;
            // 
            // btnAbrirPasta
            // 
            this.btnAbrirPasta.Location = new System.Drawing.Point(14, 15);
            this.btnAbrirPasta.Name = "btnAbrirPasta";
            this.btnAbrirPasta.Size = new System.Drawing.Size(75, 23);
            this.btnAbrirPasta.TabIndex = 2;
            this.btnAbrirPasta.Text = "Abrir Pasta";
            this.btnAbrirPasta.UseVisualStyleBackColor = true;
            this.btnAbrirPasta.Click += new System.EventHandler(this.btnAbrirPasta_Click);
            // 
            // listResult
            // 
            this.listResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listResult.FormattingEnabled = true;
            this.listResult.Location = new System.Drawing.Point(112, 63);
            this.listResult.Name = "listResult";
            this.listResult.Size = new System.Drawing.Size(809, 290);
            this.listResult.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 366);
            this.Controls.Add(this.listResult);
            this.Controls.Add(this.btnAbrirPasta);
            this.Controls.Add(this.txtCaminho);
            this.Controls.Add(this.btnSalvar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtCaminho;
        private System.Windows.Forms.Button btnAbrirPasta;
        private System.Windows.Forms.ListBox listResult;
    }
}

