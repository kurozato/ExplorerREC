using BlackSugar.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BlackSugar.Views
{
    public interface IColorSettingView
    {
    //    void Show();

    //    DialogResult ShowDialog();

        ColorInfo ColorInfo { get; set; }

        Dictionary<GradationPattern, ColorInfo> GradationInfo { get; set; }

        Dictionary<ColorTheme, ColorInfo> ThemeInfo { get; set; }

        Action<ColorTheme, GradationPattern> SettingAction { get; set; }
    }
}
