using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Log_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrirPasta_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCaminho.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
       
            listResult.Items.Add("Selecione a pasta onde estâo localizados os LOG´s.");
        
            Count = 0;

            var selectedPath = txtCaminho.Text;


        }

        public static int Count { get; set; }
        public static List<Servidor> servidores { get; set; } = new List<Servidor>();

        static IEnumerable<Arquivos> getFiles(DirectoryInfo directoryInfo, int mainFolder, Servidor server)
        {
            List<Arquivos> retorno = new List<Arquivos>();

            var subDirectory = directoryInfo.GetDirectories();

            if (subDirectory.Length > 0)
            {
                foreach (var item in subDirectory)
                {
                    retorno.AddRange(getFiles(item, mainFolder, server));
                }
            }

            var files = directoryInfo.GetFiles("*.txt");

            if (files.Length > 0)
            {
                foreach (var item in files)
                {
                    var regex = @"[0-9]{1}	[0-9]{2}\/[0-9]{2}\/[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}	[A-Za-z0-9]{1}";
                    var regexError = "sistema	versao	local	tipo	ult_msg	DatHorIni	DatHor_ult_msg	dathor_ult_email";

                    Arquivos arquivo = new Arquivos(item.FullName.Split('\\').Last(), item.Length);
                    arquivo.columns = item.FullName.Substring(mainFolder).Split('\\');
                    try
                    {
                        using (StreamReader sw = new StreamReader(item.FullName))
                        {
                            string line = sw.ReadLine();

                            if (Regex.IsMatch(line, regex))
                                arquivo.Tipo = "Log";
                            else if (line.Equals(regexError))
                                arquivo.Tipo = "Erro";
                            else
                                arquivo.Tipo = "Legado";

                         
                            arquivo.FirstLine = line;
                        }
                    }
                    catch (Exception)
                    {
                        arquivo.Tipo = "Nâo Identificado";
                        arquivo.FirstLine = string.Empty;
                    }

                    arquivo.servidor = server;

                    Count++;

                    retorno.Add(arquivo);
                }
            }

            return retorno;
        }

        public static int CountServidor { get; set; } = 0;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            foreach (var server in servidores)
            {
                if (System.IO.Directory.Exists(server.Caminho))
                {
                    var mainFolder = server.Caminho.Length + 1;
                    var retorno = getFiles(new System.IO.DirectoryInfo(server.Caminho), mainFolder, server);

                    StringBuilder sb = new StringBuilder();

                    foreach (var item in retorno)
                    {
                        var fullLine = item.servidor.Alias + "|" + item.servidor.Nome + "|" + String.Join("|", item.columns) + "|" + item.Size + "|" + item.Tipo;
                        listResult.Items.Add(fullLine);
                        sb.AppendLine(fullLine);
                    }

                    listResult.Items.Add("Relatório gerado com sucesso com base em " + Count + " arquivos, agora selecione uma pasta para salvar o CSV.");

                    FolderBrowserDialog opSalvar = new FolderBrowserDialog();

                    var location = System.Reflection.Assembly.GetEntryAssembly().Location;

                    if (opSalvar.ShowDialog() == DialogResult.OK)
                    {
                       
                        if (System.IO.File.Exists(location + "\\relatorio.csv"))
                            System.IO.File.AppendAllText(location + "\\relatorio.csv", sb.ToString());
                        else
                            System.IO.File.WriteAllText(location + "\\relatorio.csv", sb.ToString());
                    }

                    listResult.Items.Add("Relatório Salvo no caminho: " + location + "\\relatorio.csv");
                }

                CountServidor++;
                backgroundWorker1.ReportProgress(CountServidor);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnCarregarServidores_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            if (op.ShowDialog() == DialogResult.OK)
            {
                listServidores.Items.Clear();
                servidores.Clear();
                using (StreamReader sw = new StreamReader(op.FileName))
                {
                    string line = String.Empty;

                    while ((line = sw.ReadLine()) != null)
                    {
                        var itens = line.Split(';');
                        servidores.Add(new Servidor()
                        {
                            Alias = itens[0],
                            Nome = itens[1],
                            Caminho = itens[2]
                        });

                        listServidores.Items.Add(line);
                    }
                }
            }

            progressBar1.Minimum = 0;
            progressBar1.Maximum = listServidores.Items.Count;
        }
    }

    public class Servidor
    {
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string Alias { get; set; }
    }

    public class Arquivos
    {
        public Arquivos(string _File, long _Size)
        {
            this.File = _File;
            this.Size = _Size;
        }
        public string FirstLine { get; set; }
        public long Size { get; set; }
        public IEnumerable<string> columns { get; set; }
        public string File { get; set; }
        public string Tipo { get; set; }
        public Servidor servidor { get; set; }

    }
}
