using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generate_File_Logs
{
    class Program
    {
        public static int Count { get; set; }

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Selecione a pasta onde estâo localizados os LOG´s.");
            Console.WriteLine("Digite ENTER para continuar.");
            Console.ReadLine();

            Count = 0;

            FolderBrowserDialog op = new FolderBrowserDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                var mainFolder = op.SelectedPath.Length + 1;
                var retorno = getFiles(new System.IO.DirectoryInfo(op.SelectedPath), mainFolder);

                StringBuilder sb = new StringBuilder();

                foreach (var item in retorno)
                {
                    sb.AppendLine(String.Join("|", item.columns) + "|" + item.Size + "|" + item.valido + "|" + item.Tipo);
                }

                FolderBrowserDialog opSalvar = new FolderBrowserDialog();

                Console.WriteLine(sb.ToString());
                Console.WriteLine("Relatório gerado com sucesso com base em "+ Count + " arquivos, agora selecione uma pasta para salvar o CSV.");
                Console.WriteLine("Digite ENTER para continuar.");
                Console.ReadLine();

                if (opSalvar.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.File.Exists(opSalvar.SelectedPath + "\\relatorio.csv"))
                        System.IO.File.AppendAllText(opSalvar.SelectedPath + "\\relatorio.csv", sb.ToString());
                    else
                        System.IO.File.WriteAllText(opSalvar.SelectedPath + "\\relatorio.csv", sb.ToString());
                }
                
                Console.WriteLine("Relatório Salvo no caminho: " + opSalvar.SelectedPath + "\\relatorio.csv");
                Console.WriteLine("Digite ENTER para finalizar.");
                Console.ReadLine();
            }
        }

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
}
