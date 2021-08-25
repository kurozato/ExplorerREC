using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Repository
{
    public interface IGeneralSetting
    {
        string SettingPath { get; }

        string RecodePath { get; }

        int Interval { get; set; }

        int MaxRowsCount { get; set; }
    }

    public class GeneralSetting : IGeneralSetting
    {
        public const string FILENAME_SETTING = "g_setting";
        public const string FILENAME_RECODE = "e_recodes";

        public string SettingPath => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME_SETTING);

        public string RecodePath => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME_RECODE);

        public int Interval { get; set; }

        public int MaxRowsCount { get; set; }
    }


}
