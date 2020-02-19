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

            if (System.IO.Directory.Exists(selectedPath)) 
            {
                var mainFolder = selectedPath.Length + 1;
                var retorno = getFiles(new System.IO.DirectoryInfo(selectedPath), mainFolder);

                StringBuilder sb = new StringBuilder();

                foreach (var item in retorno)
                {
                    var fullLine = String.Join("|", item.columns) + "|" + item.Size + "|" + item.valido + "|" + item.Tipo;
                    listResult.Items.Add(fullLine);
                    sb.AppendLine(fullLine);
                }

                listResult.Items.Add("Relatório gerado com sucesso com base em " + Count + " arquivos, agora selecione uma pasta para salvar o CSV.");

                FolderBrowserDialog opSalvar = new FolderBrowserDialog();

                if (opSalvar.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.File.Exists(opSalvar.SelectedPath + "\\relatorio.csv"))
                        System.IO.File.AppendAllText(opSalvar.SelectedPath + "\\relatorio.csv", sb.ToString());
                    else
                        System.IO.File.WriteAllText(opSalvar.SelectedPath + "\\relatorio.csv", sb.ToString());
                }

                listResult.Items.Add("Relatório Salvo no caminho: " + opSalvar.SelectedPath + "\\relatorio.csv");
           
            }
        }

        public static int Count { get; set; }

      
        static IEnumerable<Arquivos> getFiles(DirectoryInfo directoryInfo, int mainFolder)
        {
            List<Arquivos> retorno = new List<Arquivos>();

            var subDirectory = directoryInfo.GetDirectories();

            if (subDirectory.Length > 0)
            {
                foreach (var item in subDirectory)
                {
                    retorno.AddRange(getFiles(item, mainFolder));
                }
            }

            var files = directoryInfo.GetFiles();

            if (files.Length > 0)
            {
                foreach (var item in files)
                {
                    var regex = @"[0-9]{1}	[0-9]{2}\/[0-9]{2}\/[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}	[A-Za-z0-9]{1}";
                    var regexError = "sistema	versao	local	tipo	ult_msg	DatHorIni	DatHor_ult_msg	dathor_ult_email";

                    Arquivos arquivo = new Arquivos(item.FullName.Split('\\').Last(), item.Length);
                    arquivo.columns = item.FullName.Substring(mainFolder).Split('\\');
                    using (StreamReader sw = new StreamReader(item.FullName))
                    {
                        string line = sw.ReadLine();

                        if (Regex.IsMatch(line, regex))
                            arquivo.Tipo = "Log";
                        else if (line.Equals(regexError))
                            arquivo.Tipo = "Erro";

                        arquivo.valido = Regex.IsMatch(line, regex) || line.Equals(regexError);
                        arquivo.FirstLine = line;
                    }

                    Count++;

                    retorno.Add(arquivo);
                }
            }

            return retorno;
        }
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
        public bool valido { get; set; }
    }
}
