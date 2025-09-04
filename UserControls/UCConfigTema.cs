using Sistema.DAO;
using Sistema.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema.UserControls
{
    public partial class UCConfigTema : UserControl
    {
        private Color primary;
        private Color secondary;
        private Color titles_bg;

        private readonly ConfigDAO dao = new ConfigDAO();

        public UCConfigTema()
        {
            InitializeComponent();
        }

        private void UCConfigTema_Load(object sender, EventArgs e)
        {
            Config config = dao.ObterGeral();

            primary = ColorTranslator.FromHtml(config.primary_color);
            secondary = ColorTranslator.FromHtml(config.secondary_color);
            titles_bg = ColorTranslator.FromHtml(config.titles_bg_color);

            PreencherTema();
        }

        private void PreencherTema()
        {
            pnlCorPrimaria.BackColor = primary;
            pnlCorSecundaria.BackColor = secondary;
            pnlBgTitulos.BackColor = titles_bg;
        }

        public string GetPrimaryColor()
        {
            return MainClass.ColorToHex(primary);
        }

        public string GetSecondaryColor()
        {
            return MainClass.ColorToHex(secondary);
        }

        public string GetTitlesBgColor()
        {
            return MainClass.ColorToHex(titles_bg);
        }

        private void pnlCorPrimaria_Click(object sender, EventArgs e)
        {
            selectColor.ShowDialog();
            primary = selectColor.Color;
            pnlCorPrimaria.BackColor = primary;
        }

        private void pnlCorSecundaria_Click(object sender, EventArgs e)
        {
            selectColor.ShowDialog();
            secondary = selectColor.Color;
            pnlCorSecundaria.BackColor = secondary;
        }

        private void pnlBgTitulos_Click(object sender, EventArgs e)
        {
            selectColor.ShowDialog();
            titles_bg = selectColor.Color;
            pnlBgTitulos.BackColor = titles_bg;
        }

        private void btnPadraoPrimaria_Click(object sender, EventArgs e)
        {
            primary = ColorTranslator.FromHtml(ThemeProvider.GetPrimaryDefault());
            pnlCorPrimaria.BackColor = primary;
        }

        private void btnPadraoSecundaria_Click(object sender, EventArgs e)
        {
            secondary = ColorTranslator.FromHtml(ThemeProvider.GetSecondaryDefault());
            pnlCorSecundaria.BackColor = secondary;
        }

        private void btnPadraoTitulosBg_Click(object sender, EventArgs e)
        {
            titles_bg = ColorTranslator.FromHtml(ThemeProvider.GetTitlesBgDefault());
            pnlBgTitulos.BackColor = titles_bg;
        }
    }
}
