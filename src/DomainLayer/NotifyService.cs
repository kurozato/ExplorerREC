using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Entity;
using BlackSugar.Repository;

namespace BlackSugar.Service
{
    public interface INotifyService
    {
        void CheckWindow();

        void Initialize();

        bool ToggleHotKey(IntPtr handle, Action keyPressed);
    }

    public class NotifyService : INotifyService
    {
        protected IWindowChecker _windowChecker;
        protected IWindowGetter _windowGetter;
        protected IAutomationRegister _automationRegister;
        protected IDbCommander _dbCommander;
        protected IGeneralSetting _setting;
        protected IHotKeyNotifier _hotKeyNotifier;

        protected ExplorerWindow _window;

        public NotifyService(
            IWindowChecker windowChecker,
            IWindowGetter windowGetter,
            IAutomationRegister automationRegister,
            IDbCommander dbCommander,
            IHotKeyNotifier hotKeyNotifier,
            IGeneralSetting setting)
        {
            _windowChecker = windowChecker ?? throw new ArgumentNullException(nameof(windowChecker));
            _windowGetter = windowGetter ?? throw new ArgumentNullException(nameof(windowGetter));
            _automationRegister = automationRegister ?? throw new ArgumentNullException(nameof(automationRegister));
            _dbCommander = dbCommander ?? throw new ArgumentNullException(nameof(dbCommander));
            _hotKeyNotifier = hotKeyNotifier ?? throw new ArgumentNullException(nameof(hotKeyNotifier));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));

        }

        private void UpdateRecode(ExplorerWindow window)
        {
            var command = Properties.Resources.RegistExplorerRecodes;
            command = command.Replace("@Name", window.Name.Replace("'", "''"));
            command = command.Replace("@Path", window.Path.Replace("'", "''"));
            command = command.Replace("@MaxRowsCount", _setting.MaxRowsCount.ToString());

            _dbCommander.Execute(command);
        }

        public void CheckWindow()
        {
            var handle = _windowGetter.GetActiveWindowHandle();
            if (_windowChecker.IsExplorer(handle))
            {
                _window = _windowGetter.GetExplorerWindow(handle);

                _automationRegister.RegistWindowCloesd(handle, () => UpdateRecode(_window));
            }
        }

        public void Initialize()
        {
            _dbCommander.Execute(Properties.Resources.CreateTable_ExplorerRecodes);
        }

        public bool ToggleHotKey(IntPtr handle, Action keyPressed)
        {
            if (_hotKeyNotifier.RegistKeys)
                _hotKeyNotifier.Release();
            else
            {
                _hotKeyNotifier.Initialize(handle);
                _hotKeyNotifier.Regist(keyPressed);
            }

            return _hotKeyNotifier.RegistKeys;
        }
    }
}
