using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema
{
    internal class ThemeProvider
    {

        public static Color primary {  get; set; }
        public static Color secondary {  get; set; }
        public static Color titles_bg { get; set; }

        private static string primaryDefault = "#3C5078";
        private static string secondaryDefault = "#404040";
        private static string titlesBgDefault = "#009C88";

        public static string GetPrimaryDefault() => primaryDefault;
        public static string GetSecondaryDefault() => secondaryDefault;
        public static string GetTitlesBgDefault() => titlesBgDefault;

    }
}
