using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Views
{
    public interface INotifyView
    {
        Action OpenAction { get; set; }

        Action CloseAction { get; set; }

        Action ColorSettingAction { get; set; }

        Action RoopAction { get; set; }

        void Exit();

        void SetTimerInterval(int interval);

        void TimerStart();

        void TimerStop();
    }
}
