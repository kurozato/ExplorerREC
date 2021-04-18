using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Entity;
using BlackSugar.Repository;

namespace BlackSugar.Service
{
    public interface IColorService
    {
        ColorInfo GetColorInfo();

        ColorInfo GetColorInfo(ColorTheme theme, GradationPattern pattern);

        Dictionary<GradationPattern, ColorInfo> GetGradationPatterns();

        Dictionary<ColorTheme, ColorInfo> GetColorThemes();

        void Save(ColorInfo colorInfo);
    }

    public class ColorService : IColorService
    {
        private IFileWriter _fileWriter;

        private IGeneralSetting _setting;

        private IFileReader _fileReader;

        public ColorService(
            IGeneralSetting setting,
            IFileWriter fileWriter,
            IFileReader fileReader)
        {
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
        }

        public ColorInfo GetColorInfo()
        {
            var colorInfo = _fileReader.Read<ColorInfo>(_setting.SettingPath + "Color");

            if (colorInfo != null)
                return colorInfo;

            return GetColorInfo(ColorTheme.Dark, GradationPattern.Orange);
        }

        public Dictionary<GradationPattern, ColorInfo> GetGradationPatterns()
        {
            var patterns = new Dictionary<GradationPattern, ColorInfo>();

            patterns.Add(GradationPattern.Monotone, GetColorInfo(ColorTheme.Light, GradationPattern.Monotone));
            patterns.Add(GradationPattern.Orange, GetColorInfo(ColorTheme.Light, GradationPattern.Orange));
            patterns.Add(GradationPattern.Blue, GetColorInfo(ColorTheme.Light, GradationPattern.Blue));
            patterns.Add(GradationPattern.Green, GetColorInfo(ColorTheme.Light, GradationPattern.Green));
            patterns.Add(GradationPattern.Violet, GetColorInfo(ColorTheme.Light, GradationPattern.Violet));

            return patterns;
        }

        public Dictionary<ColorTheme, ColorInfo> GetColorThemes()
        {
            var themes = new Dictionary<ColorTheme, ColorInfo>();

            themes.Add(ColorTheme.Light, GetLightTheme());
            themes.Add(ColorTheme.Dark, GetDarkTheme());

            return themes;
        }

        public void Save(ColorInfo colorInfo)
        {
            _fileWriter.Write(_setting.SettingPath + "Color", colorInfo);
        }

        public ColorInfo GetColorInfo(ColorTheme theme, GradationPattern pattern)
        {
            ColorInfo colorInfo = null;

            switch (theme)
            {
                case ColorTheme.Dark:
                    colorInfo = GetDarkTheme();
                    break;
                case ColorTheme.Light:
                    colorInfo = GetLightTheme();
                    break;
            }

            switch(pattern)
            {
                case GradationPattern.Monotone:
                    SetMonotonePattern(colorInfo);
                    break;
                case GradationPattern.Orange:
                    SetOrangePattern(colorInfo);
                    break;
                case GradationPattern.Blue:
                    SetBluePattern(colorInfo);
                    break;
                case GradationPattern.Green:
                    SetGreenPattern(colorInfo);
                    break;
                case GradationPattern.Violet:
                    SetVioletPattern(colorInfo);
                    break;
            }

            return colorInfo;
        }

        private ColorInfo GetDarkTheme()
        {
            return new ColorInfo
            {
                LightColorCode = "#2c2c2c",
                DarkColorCode = "#232323",
                SelectedColorCode = "#000000",
                ForeColorCode = "#ffffff",
                Theme = ColorTheme.Dark,
            };
        }

        private ColorInfo GetLightTheme()
        {
            return new ColorInfo
            {
                LightColorCode = "#eeeeee",
                DarkColorCode = "#ffffff",
                SelectedColorCode = "#dcdcdc",
                ForeColorCode = "#000000",
                Theme = ColorTheme.Light,
            };
        }

        private void SetMonotonePattern(ColorInfo colorInfo)
        {
            colorInfo.GradationColorFromCode = "#f5f5f5";
            colorInfo.GradationColorToCode = "#696969";
            colorInfo.Pattern = GradationPattern.Monotone;
        }

        private void SetOrangePattern(ColorInfo colorInfo)
        {
            colorInfo.GradationColorFromCode = "#ffa500";
            colorInfo.GradationColorToCode = "#ff4500";
            colorInfo.Pattern = GradationPattern.Orange;
        }

        private void SetBluePattern(ColorInfo colorInfo)
        {
            colorInfo.GradationColorFromCode = "#40e0d0";
            colorInfo.GradationColorToCode = "#4141e0";
            colorInfo.Pattern = GradationPattern.Blue;
        }

        private void SetGreenPattern(ColorInfo colorInfo)
        {
            colorInfo.GradationColorFromCode = "#00ff00";
            colorInfo.GradationColorToCode = "#008000";
            colorInfo.Pattern = GradationPattern.Green;
        }

        private void SetVioletPattern(ColorInfo colorInfo)
        {
            colorInfo.GradationColorFromCode = "#ff00ff";
            colorInfo.GradationColorToCode = "#9400d3";
            colorInfo.Pattern = GradationPattern.Violet;
        }


    }
}
