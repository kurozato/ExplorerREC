using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using BlackSugar.Entity;

namespace BlackSugar.Repository
{
    public interface IWindowChecker
    {
        bool IsExplorer(IntPtr handle);
    }

    public class WindowChecker : IWindowChecker
    {
        protected string FileName_EXPLORER;

        protected ILogWriter _logWriter;

        public WindowChecker(ILogWriter logWriter)
        {
            FileName_EXPLORER = ExplorerWindow.ExplorerFullName();
            _logWriter = logWriter ?? throw new ArgumentNullException(nameof(logWriter));
        }

        public bool IsExplorer(IntPtr handle)
        {
            bool ok = true;

            ok = ok && GetClassName(handle).ToUpper() == ExplorerWindow.CLASS_NAME_EXPLORER;
            ok = ok && GetFileNameEx(handle).ToUpper() == FileName_EXPLORER;

            return ok;
        }

        private class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern int GetClassName(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpClassName, int nSize);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

            [DllImport("kernel32.dll")]
            internal static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

            [DllImport("psapi.dll", CharSet = CharSet.Unicode)]
            internal static extern int GetModuleBaseName(IntPtr hWnd, IntPtr hModule, [MarshalAs(UnmanagedType.LPWStr), Out]StringBuilder lpBaseName, int nSize);

            [DllImport("psapi.dll", CharSet = CharSet.Unicode)]
            internal static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        }

        public string GetFileNameEx(IntPtr hWnd)
        {
            try
            {
                const int nChars = 1024;
                uint processId;
                StringBuilder filename = new StringBuilder(nChars);
                NativeMethods.GetWindowThreadProcessId(hWnd, out processId);
                IntPtr handle = NativeMethods.OpenProcess(0x0400 | 0x0010, false, processId);
                NativeMethods.GetModuleFileNameEx(handle, IntPtr.Zero, filename, nChars);

                return filename.ToString();
            }
            catch (Exception ex)
            {
                _logWriter.Write(ex, nameof(WindowChecker));
                throw;
            }
        }

        public string GetClassName(IntPtr hWnd)
        {
            try
            {
                const int nChars = 1024;
                StringBuilder classname = new StringBuilder(nChars);
                NativeMethods.GetClassName(hWnd, classname, nChars);

                return classname.ToString();
            }
            catch (Exception ex)
            {
                _logWriter.Write(ex, nameof(WindowChecker));
                throw;
            }
        }
    }
}
