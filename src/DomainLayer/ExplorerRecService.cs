using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Entity;
using BlackSugar.Repository;

namespace BlackSugar.Service
{
    public interface IExplorerRecService
    {
        List<ExplorerWindow> GetExplorerWindows();

         void Initialize();

        bool OpenWindow(ExplorerWindow window);
    }

    public class ExplorerRecService : IExplorerRecService
    {
        protected IWindowChecker _windowChecker;
        protected IWindowGetter _windowGetter;
        protected IAutomationRegister _automationRegister;
        protected IDbCommander _dbCommander;
        protected IGeneralSetting _setting;

        protected List<ExplorerWindow> _recodes;

        protected ExplorerWindow _window;
        
        public ExplorerRecService(
            IWindowChecker windowChecker,
            IWindowGetter windowGetter,
            IAutomationRegister automationRegister,
            IDbCommander dbCommander,
            IGeneralSetting setting)
        {
            _windowChecker = windowChecker ?? throw new ArgumentNullException(nameof(windowChecker)); 
            _windowGetter = windowGetter ?? throw new ArgumentNullException(nameof(windowGetter)); 
            _automationRegister = automationRegister ?? throw new ArgumentNullException(nameof(automationRegister)); 
            _dbCommander = dbCommander ?? throw new ArgumentNullException(nameof(dbCommander));  
            _setting = setting ?? throw new ArgumentNullException(nameof(setting)); 
        }

        public void Initialize()
        {
            _dbCommander.Execute(Properties.Resources.CreateTable_ExplorerRecodes);
        }

        public List<ExplorerWindow> GetExplorerWindows()
        {
            return _dbCommander.Get<ExplorerWindow>(Properties.Resources.Query_ExplorerRecodes, null).ToList();
        }

        public bool OpenWindow(ExplorerWindow window)
        {
            if (System.IO.Directory.Exists(window.Path))
            {
                System.Diagnostics.Process.Start(window.Path);
                return true;
            }
            else
            {
                var command = Properties.Resources.DeleteExplorerRecodes;
                command = command.Replace("@Path", window.Path.Replace("'", "''"));

                _dbCommander.Execute(command);
                return false;
            }
        }

    }
}
