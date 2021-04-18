using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Entity
{
    public class ExplorerWindow
    {
        public string Path { get; set; }

        public string Name { get; set; }

        public const string CLASS_NAME_EXPLORER = "CABINETWCLASS";

        public const string MODULE_NAME_EXPLORER = "EXPLORER.EXE";

        public static string ExplorerFullName()
        {
            return System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToUpper(),
                MODULE_NAME_EXPLORER);
        }
    }
}
