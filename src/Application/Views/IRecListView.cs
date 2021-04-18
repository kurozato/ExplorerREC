using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Entity;

namespace BlackSugar.Views
{
    public interface IRecListView
    {
        Action<ExplorerWindow> SelectedAction { get; set; }

        void Close();

        List<ExplorerWindow> Model { get; set; }

        ColorInfo ColorInfo { get; set; }

        void ShowMessage(string message);
    }
}
