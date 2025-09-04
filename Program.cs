using Sistema.DAO;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    internal static class Program
    {
        private static readonly ConfigDAO configDAO = new ConfigDAO();

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmPrincipal());
            Config config = configDAO.ObterGeral();
            ThemeProvider.primary = ColorTranslator.FromHtml(config.primary_color);
            ThemeProvider.secondary = ColorTranslator.FromHtml(config.secondary_color);
            ThemeProvider.titles_bg = ColorTranslator.FromHtml(config.titles_bg_color);
            Application.Run(new FrmLogin());
        }

        public static string NomeUsuario = "";
        public static string LoginUsuario = "";
    }
}
