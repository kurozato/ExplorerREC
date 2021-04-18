using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackSugar.SimpleMvp;
using BlackSugar.SimpleMvp.WinForm;
using BlackSugar.Views;
using BlackSugar.Service;
using BlackSugar.Entity;

namespace BlackSugar.Presenters
{
    public class ColorSettingPresenter : Presenter<IColorSettingView>
    {
        protected IColorService _service;

        public ColorSettingPresenter(IColorService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        protected override void InitializeView()
        {
            _view.ColorInfo = _service.GetColorInfo();
            _view.GradationInfo = _service.GetGradationPatterns();
            _view.ThemeInfo = _service.GetColorThemes();

            _view.SettingAction = (t, p) => SettingResult(t, p);
        }
        
        private void SettingResult(ColorTheme theme, GradationPattern pattern)
        {
            var colorInfo = _service.GetColorInfo(theme, pattern);
            _service.Save(colorInfo);
        }
    }
}
