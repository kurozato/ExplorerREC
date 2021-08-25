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
using BlackSugar.Repository;

namespace BlackSugar.Presenters
{
    public class NotifyPresenter : PresenterBase<INotifyView>
    {
        //protected IExplorerRecService _service;
        protected INotifyService _service;

        protected IGeneralSetting _setting;

        protected ILogWriter _logWriter;

        public NotifyPresenter(
            INotifyService service, 
            IGeneralSetting setting,
            ILogWriter logWriter)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _logWriter = logWriter ?? throw new ArgumentNullException(nameof(logWriter));
        }

        protected override void InitializeView()
        {
            _service.Initialize();
            _view.SetTimerInterval(_setting.Interval);

            _view.CloseAction = () => CloseResult();
            _view.OpenAction = () => OpenResult();
            _view.ColorSettingAction = () => ColorSettingResult();
            _view.RoopAction = () => RoopResult();
            _view.ToggleHotKeyAction = () => ToggleHotKeyResult();
        }

        private void CloseResult()
        {
            Application.Exit();
        }
        private void OpenResult()
        {
            Router.NavigateTo<IRecListView>(false);
        }
        private void ColorSettingResult()
        {
            Router.NavigateTo<IColorSettingView>(false);
        }
        private void RoopResult()
        {
            try
            {
                _view.TimerStop();
                _service.CheckWindow();
            }
            catch (Exception ex)
            {
                _logWriter.Write(ex, nameof(NotifyPresenter));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _view.TimerStart();
            }
        }

        private void ToggleHotKeyResult()
        {
            if (_service.ToggleHotKey(_view.Handle, _view.OpenAction))
                _view.ToggleHotKey = "DisableHotKey";
            else
                _view.ToggleHotKey = "EableHotKey";
        }
    }
}
