using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackSugar.SimpleMvp.WinForm;
using BlackSugar.Utility;
using BlackSugar.Entity;
using System.Drawing.Drawing2D;

namespace BlackSugar.Views
{
    public partial class ColorSettingForm : ViewForm, IColorSettingView
    {
        private GradationPattern _pattern;

        private ColorTheme _theme;

        private ColorInfo _colorInfo;

        private Dictionary<GradationPattern, ColorInfo> _gradationInfo;

        private Dictionary<ColorTheme, ColorInfo> _themeInfo;

        private Dictionary<GradationPattern, object[]> _panelInfo;

        private const int pnlGroupIndex = 0;
        private const int labelIndex = 1;
        private const int pnlGradationIndex = 2;
        private const int pnlSideIndex = 3;

        private void InitializePanel()
        {
            _panelInfo = new Dictionary<GradationPattern, object[]>();
            //Add Color
            SetGradation(GradationPattern.Monotone);
            SetGradation(GradationPattern.Orange);
            SetGradation(GradationPattern.Blue);
            SetGradation(GradationPattern.Green);
            SetGradation(GradationPattern.Violet);


            foreach(var item in _panelInfo)
            {
                RegistClick(item.Value[0] as Panel, item.Key);
            }
        }
    
        private void SetGradation(GradationPattern pattern)
        {
            var pnlGrp = UIHelper.FindControl("pnlGroup" + pattern.ToString(), this);
            var label = UIHelper.FindControl("lbl" + pattern.ToString(), pnlGrp);
            var pnlGradation = UIHelper.FindControl("pnl" + pattern.ToString(), pnlGrp);
            var pnlSide = UIHelper.FindControl("pnlSide" + pattern.ToString(), pnlGrp);

            var obj = new object[4];
            obj[0] = pnlGrp;
            obj[1] = label;
            obj[2] = pnlGradation;
            obj[3] = pnlSide;

            _panelInfo.Add(pattern, obj);
        }

        public ColorSettingForm()
        {
            InitializeComponent();
            InitializePanel();

            btnClose.Click += (s, e) => { this.Close(); };

            btnOK.Click += (s, e) => {
                SettingAction(_theme, _pattern);
                this.Close();
            };

            this.FormClosed += (s, e) => {
                if (this.Modal == false)
                    base.Dispose(true);

                _colorInfo = null;
                _gradationInfo = null;

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.Collect();
            };

            btnDark.Click += (s, e) => { SelectTheme(ColorTheme.Dark); };
            btnLight.Click += (s, e) => { SelectTheme(ColorTheme.Light); };

            foreach (var item in _panelInfo)
            {
                RegistClick(item.Value[pnlGroupIndex] as Control, item.Key);
            }

            UIHelper.SetWindowTitleBar(this, this);
            UIHelper.SetWindowTitleBar(lblTitle1, this);
            UIHelper.SetWindowTitleBar(lblTitle2, this);
        }

        private void SelectTheme(ColorTheme theme)
        {
            SuspendLayout();

            _colorInfo = _themeInfo[theme];
            _colorInfo.Pattern = _pattern;
            InitializeColor(_colorInfo);
            ReSetPanelSideColor(_colorInfo);
            _theme = theme;

            PerformLayout();
        }

        public ColorInfo ColorInfo
        {
            get => _colorInfo;
            set {
                _colorInfo = value;
                InitializeColor(_colorInfo);
                ReSetPanelSideColor(_colorInfo);
                SetPattern(_colorInfo.Pattern);
                _theme = _colorInfo.Theme;
                //_pattern = _colorInfo.Pattern;
            }
        }

        public Dictionary<GradationPattern, ColorInfo> GradationInfo
        {
            get => _gradationInfo;
            set
            {
                _gradationInfo = value;
                InitializeColor(_themeInfo, _gradationInfo);
            }
        }
        public Dictionary<ColorTheme, ColorInfo> ThemeInfo
        {
            get => _themeInfo;
            set
            {
                _themeInfo = value;
                InitializeColor(_themeInfo, _gradationInfo);
            }
        }

        public Action<ColorTheme, GradationPattern> SettingAction { get; set; }


        private void InitializeColor(ColorInfo colorInfo)
        {
            UIHelper.SetColor(this, null, colorInfo.DarkColor);
            UIHelper.SetColor(lblTitle1, colorInfo.ForeColor, null);
            UIHelper.SetColor(lblTitle2, colorInfo.ForeColor, null);
            UIHelper.SetColor(lblOr, colorInfo.ForeColor, null);
            UIHelper.SetColor(btnClose, colorInfo.ForeColor, colorInfo.LightColor);
            UIHelper.SetColor(btnOK, colorInfo.ForeColor, colorInfo.LightColor);

            foreach (var item in _panelInfo)
            {
                UIHelper.SetColor(item.Value[pnlGradationIndex] as Control, null, colorInfo.DarkColor);
                UIHelper.SetColor(item.Value[labelIndex] as Control, colorInfo.ForeColor, null);
            }
        }

        private void ReSetPanelSideColor(ColorInfo colorInfo)
        {
            foreach (var item in _panelInfo)
            {
                UIHelper.SetColor(item.Value[pnlSideIndex] as Control, null, colorInfo.DarkColor);
            }
        }

        private void InitializeColor(
            Dictionary<ColorTheme, ColorInfo> themeInfo, 
            Dictionary<GradationPattern, ColorInfo> gradationInfo)
        {
            if (themeInfo == null) return;
            if (gradationInfo == null) return;

            foreach (var item in _panelInfo)
            {
                var colorInfo = gradationInfo[item.Key];
                UIHelper.Gradation(item.Value[pnlGradationIndex] as Control, colorInfo.GradationColorFrom, colorInfo.GradationColorTo, LinearGradientMode.Horizontal);
            }

            SetTheme(btnLight, themeInfo[ColorTheme.Light]);
            SetTheme(btnDark, themeInfo[ColorTheme.Dark]);
        }

        private void SetTheme(Button button, ColorInfo colorInfo)
        {
            UIHelper.SetColor(button, colorInfo.ForeColor, colorInfo.LightColor);
        }

        private void SetPattern(GradationPattern pattern)
        {
            Panel panel = _panelInfo[pattern][pnlGroupIndex] as Panel;

            var key = "pnlSide" + pattern.ToString();
            Panel side = UIHelper.FindControl(key, panel) as Panel;
            UIHelper.SetColor(side, null, Color.Gold);
            _pattern = pattern;
        }

        private void RegistClick(Control panel, GradationPattern pattern)
        {
          
            UIHelper.ClickBubble((s, e) => {

                ReSetPanelSideColor(_colorInfo);
                SetPattern(pattern);
            }, panel);
            
        }

    }
}
