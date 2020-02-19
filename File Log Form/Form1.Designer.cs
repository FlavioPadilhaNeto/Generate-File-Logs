﻿namespace File_Log_Form
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
            this.listServidores = new System.Windows.Forms.ListBox();
            this.btnCarregarServidores = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.listError = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblArquivos = new System.Windows.Forms.Label();
            this.lblNomeArquivo = new System.Windows.Forms.Label();
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
            this.listResult.Size = new System.Drawing.Size(522, 264);
            this.listResult.TabIndex = 3;
            // 
            // listServidores
            // 
            this.listServidores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listServidores.FormattingEnabled = true;
            this.listServidores.Location = new System.Drawing.Point(640, 90);
            this.listServidores.Name = "listServidores";
            this.listServidores.Size = new System.Drawing.Size(281, 121);
            this.listServidores.TabIndex = 4;
            // 
            // btnCarregarServidores
            // 
            this.btnCarregarServidores.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCarregarServidores.Location = new System.Drawing.Point(799, 61);
            this.btnCarregarServidores.Name = "btnCarregarServidores";
            this.btnCarregarServidores.Size = new System.Drawing.Size(122, 23);
            this.btnCarregarServidores.TabIndex = 5;
            this.btnCarregarServidores.Text = "Carregar Servidores";
            this.btnCarregarServidores.UseVisualStyleBackColor = true;
            this.btnCarregarServidores.Click += new System.EventHandler(this.btnCarregarServidores_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(112, 333);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(809, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // listError
            // 
            this.listError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listError.FormattingEnabled = true;
            this.listError.Location = new System.Drawing.Point(640, 219);
            this.listError.Name = "listError";
            this.listError.Size = new System.Drawing.Size(281, 108);
            this.listError.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(637, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Arquivos:";
            // 
            // lblArquivos
            // 
            this.lblArquivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblArquivos.AutoSize = true;
            this.lblArquivos.Location = new System.Drawing.Point(685, 71);
            this.lblArquivos.Name = "lblArquivos";
            this.lblArquivos.Size = new System.Drawing.Size(13, 13);
            this.lblArquivos.TabIndex = 9;
            this.lblArquivos.Text = "0";
            // 
            // lblNomeArquivo
            // 
            this.lblNomeArquivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNomeArquivo.AutoSize = true;
            this.lblNomeArquivo.Location = new System.Drawing.Point(637, 50);
            this.lblNomeArquivo.Name = "lblNomeArquivo";
            this.lblNomeArquivo.Size = new System.Drawing.Size(43, 13);
            this.lblNomeArquivo.TabIndex = 10;
            this.lblNomeArquivo.Text = "Arquivo";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 366);
            this.Controls.Add(this.lblNomeArquivo);
            this.Controls.Add(this.lblArquivos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listError);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCarregarServidores);
            this.Controls.Add(this.listServidores);
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
        private System.Windows.Forms.ListBox listServidores;
        private System.Windows.Forms.Button btnCarregarServidores;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox listError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblArquivos;
        private System.Windows.Forms.Label lblNomeArquivo;
    }
}

