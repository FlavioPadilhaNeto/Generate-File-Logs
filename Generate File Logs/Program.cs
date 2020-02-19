using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            
            if (FolderBrowserLauncher.ShowFolderBrowser(op) == DialogResult.OK)
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

                if (FolderBrowserLauncher.ShowFolderBrowser(op) == DialogResult.OK)
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

        public static class FolderBrowserLauncher
        {
            /// <summary>
            /// Using title text to look for the top level dialog window is fragile.
            /// In particular, this will fail in non-English applications.
            /// </summary>
            const string _topLevelSearchString = "Browse For Folder";

            /// <summary>
            /// These should be more robust.  We find the correct child controls in the dialog
            /// by using the GetDlgItem method, rather than the FindWindow(Ex) method,
            /// because the dialog item IDs should be constant.
            /// </summary>
            const int _dlgItemBrowseControl = 0;
            const int _dlgItemTreeView = 100;

            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll")]
            static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

            /// <summary>
            /// Some of the messages that the Tree View control will respond to
            /// </summary>
            private const int TV_FIRST = 0x1100;
            private const int TVM_SELECTITEM = (TV_FIRST + 11);
            private const int TVM_GETNEXTITEM = (TV_FIRST + 10);
            private const int TVM_GETITEM = (TV_FIRST + 12);
            private const int TVM_ENSUREVISIBLE = (TV_FIRST + 20);

            /// <summary>
            /// Constants used to identity specific items in the Tree View control
            /// </summary>
            private const int TVGN_ROOT = 0x0;
            private const int TVGN_NEXT = 0x1;
            private const int TVGN_CHILD = 0x4;
            private const int TVGN_FIRSTVISIBLE = 0x5;
            private const int TVGN_NEXTVISIBLE = 0x6;
            private const int TVGN_CARET = 0x9;


            /// <summary>
            /// Calling this method is identical to calling the ShowDialog method of the provided
            /// FolderBrowserDialog, except that an attempt will be made to scroll the Tree View
            /// to make the currently selected folder visible in the dialog window.
            /// </summary>
            /// <param name="dlg"></param>
            /// <param name="parent"></param>
            /// <returns></returns>
            public static DialogResult ShowFolderBrowser(FolderBrowserDialog dlg, IWin32Window parent = null)
            {
                DialogResult result = DialogResult.Cancel;
                int retries = 10;

                using (Timer t = new Timer())
                {
                    t.Tick += (s, a) =>
                    {
                        if (retries > 0)
                        {
                            --retries;
                            IntPtr hwndDlg = FindWindow((string)null, _topLevelSearchString);
                            if (hwndDlg != IntPtr.Zero)
                            {
                                IntPtr hwndFolderCtrl = GetDlgItem(hwndDlg, _dlgItemBrowseControl);
                                if (hwndFolderCtrl != IntPtr.Zero)
                                {
                                    IntPtr hwndTV = GetDlgItem(hwndFolderCtrl, _dlgItemTreeView);

                                    if (hwndTV != IntPtr.Zero)
                                    {
                                        IntPtr item = SendMessage(hwndTV, (uint)TVM_GETNEXTITEM, new IntPtr(TVGN_CARET), IntPtr.Zero);
                                        if (item != IntPtr.Zero)
                                        {
                                            SendMessage(hwndTV, TVM_ENSUREVISIBLE, IntPtr.Zero, item);
                                            retries = 0;
                                            t.Stop();
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            //
                            //  We failed to find the Tree View control.
                            //
                            //  As a fall back (and this is an UberUgly hack), we will send
                            //  some fake keystrokes to the application in an attempt to force
                            //  the Tree View to scroll to the selected item.
                            //
                            t.Stop();
                            SendKeys.Send("{TAB}{TAB}{DOWN}{DOWN}{UP}{UP}");
                        }
                    };

                    t.Interval = 10;
                    t.Start();

                    result = dlg.ShowDialog(parent);
                }

                return result;
            }
        }

    }
}
