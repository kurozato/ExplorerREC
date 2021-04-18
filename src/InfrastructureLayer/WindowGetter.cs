using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using BlackSugar.Entity;
using BlackSugar.Utility;

namespace BlackSugar.Repository
{
    public interface IWindowGetter
    {
        List<ExplorerWindow> GetExplorerWindows();

        ExplorerWindow GetExplorerWindow(IntPtr handle);

        IntPtr GetActiveWindowHandle();
    }

    public class WindowGetter : IWindowGetter
    {
        public List<ExplorerWindow> GetExplorerWindows()
        {
            var fullName = ExplorerWindow.ExplorerFullName();
            return GetExplorerWindows((web, list) =>
            {
                if (web.FullName.ToUpper() == fullName && web.LocationURL != "")
                {
                    list.Add(new ExplorerWindow()
                    {
                        Path = new Uri(web.LocationURL).LocalPath,
                        Name = FolderInfo.GetName(new Uri(web.LocationURL).LocalPath),
                    });
                }
                return false;
            });
        }

        public ExplorerWindow GetExplorerWindow(IntPtr handle)
        {
            var windows = GetExplorerWindows((web, list) =>
            {
                if (web.HWND == handle.ToInt32() && web.LocationURL != "")
                {
                    list.Add(new ExplorerWindow()
                    {
                        Path = new Uri(web.LocationURL).LocalPath,
                        Name = FolderInfo.GetName(new Uri(web.LocationURL).LocalPath),
                    });
                    return true;
                }
                return false;
            });

            if (windows.Any())
                return windows[0];
            else
                return null;
        }

        public IntPtr GetActiveWindowHandle()
        {
            return NativeMethods.GetForegroundWindow();
        }

        private class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern IntPtr GetForegroundWindow();
        }

        private List<ExplorerWindow> GetExplorerWindows(Func<SHDocVw.IWebBrowser2, List<ExplorerWindow>, bool> funcBreak)
        {
            var list = new List<ExplorerWindow>();

            Shell32.Shell shell = null;
            SHDocVw.ShellWindows win = null;
            IEnumerator enumerator = null;

            try
            {
                shell = new Shell32.Shell();
                win = shell.Windows();

                enumerator = win.GetEnumerator();

                SHDocVw.IWebBrowser2 web = null;

                while (enumerator.MoveNext())
                {
                    web = enumerator.Current as SHDocVw.IWebBrowser2;
                    try
                    {
                        if (funcBreak(web, list))
                            break;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        if (web != null) Marshal.ReleaseComObject(web);
                    }
                }

                return list;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ICustomAdapter adapter = enumerator as ICustomAdapter;
                if (adapter != null) Marshal.ReleaseComObject(adapter.GetUnderlyingObject());
                if (win != null) Marshal.ReleaseComObject(win);
                if (shell != null) Marshal.ReleaseComObject(shell);

                adapter = null;
                enumerator = null;
                win = null;
                shell = null;

                // Application オブジェクトのガベージ コレクトを強制します。
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
    }
}
