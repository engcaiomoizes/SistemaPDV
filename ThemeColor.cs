using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema
{
    internal class ThemeColor
    {
        public static Color PrimaryColor { get; set; }
        public static Color SecondaryColor { get; set; }

        public static List<String> ColorList = new List<String>()
        {
            "#3498db",
            "#2980b9",
            "#74b9ff",
            "#0984e3",
            "#00a8ff",
            "#0097e6",
            "#273c75",
            "#54a0ff",
            "#2e86de",
            "#1abc9c",
            "#16a085",
            "#2ecc71",
            "#27ae60",
            "#55efc4",
            "#00b894",
            "#a29bfe",
            "#6c5ce7",
            "#7ed6df",
            "#22a6b3",
            "#686de0",
            "#4834d4",
            "#487eb0",
            "#40739e",
            "#4b7bec",
            "#3867d6",
        };

        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            } else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A, (byte) red, (byte) green, (byte) blue);
        }
    }
}
