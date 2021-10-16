using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackSugar.SimpleMvp;
using BlackSugar.Repository;
using BlackSugar.Service;
using BlackSugar.Presenters;
using BlackSugar.Views;

namespace ExplorerRec
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var resolver = new DependencyResolver();
            resolver.Set(
                container =>
                { 
                    //Repository
                    container.RegisterSingleton<IGeneralSetting, GeneralSetting>();
                    container.RegisterSingleton<ILogWriter, LogWriter>();
                    container.RegisterSingleton<IFileReader, FileReader>();
                    container.RegisterSingleton<IFileWriter, FileWriter>();
                    container.RegisterSingleton<IDbCommander, DbCommander>();
                    container.RegisterSingleton<IWindowChecker, WindowChecker>();
                    container.RegisterSingleton<IWindowGetter, WindowGetter>();                
                    container.RegisterSingleton<IAutomationRegister, AutomationRegister>();
                    container.RegisterSingleton<IHotKeyNotifier, HotKeyNotifier>();
                    //Service
                    container.RegisterSingleton<IColorService, ColorService>();
                    container.RegisterSingleton<IExplorerRecService, ExplorerRecService>();
                    container.RegisterSingleton<INotifyService, NotifyService>();
                    //View
                    container.RegisterSingleton<INotifyView, NotifyForm>();
                    container.RegisterWindowsForm<IRecListView, RecListForm>();
                    container.RegisterWindowsForm<IColorSettingView, ColorSettingForm>();
                    //Presenter
                    container.RegisterSingleton<IPresenter<INotifyView>, NotifyPresenter>();
                    container.RegisterSingleton<IPresenter<IRecListView>, RecListPresenter>();
                    container.RegisterSingleton<IPresenter<IColorSettingView>, ColorSettingPresenter>();

                    container.Verify();
                });

            Router.Configure(resolver);

            var view = Router.NavigateTo<INotifyView>();
       
            view.TimerStart();

            Application.Run();

            view.TimerStop();
            view.Exit();

            resolver.Release();
            view = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
