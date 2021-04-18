using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BlackSugar.Entity
{
    public enum ColorTheme
    {
        Light = 0,
        Dark,
    }

    public enum GradationPattern
    {
        Monotone = 0,
        Orange,
        Blue,
        Green,
        Violet,
    }

    public class ColorInfo
    {
        public ColorTheme Theme { get; set; }
        public GradationPattern Pattern { get; set; }
        public string LightColorCode { get; set; }
        public string DarkColorCode { get; set; }
        public string GradationColorFromCode { get; set; }
        public string GradationColorToCode { get; set; }
        public string ForeColorCode { get; set; }
        public string SelectedColorCode { get; set; }

        public Color LightColor => GetColor(LightColorCode);
        public Color DarkColor => GetColor(DarkColorCode);
        public Color GradationColorFrom => GetColor(GradationColorFromCode);
        public Color GradationColorTo => GetColor(GradationColorToCode);
        public Color ForeColor => GetColor(ForeColorCode);
        public Color SelectedColor => GetColor(SelectedColorCode);

        private Color GetColor(string colorCode)
        {
            return ColorTranslator.FromHtml(colorCode);
        }
    }
}
