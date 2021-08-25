using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.Utility;
using System.Windows.Forms;

namespace BlackSugar.Repository
{
    public interface IHotKeyNotifier
    {
        bool RegistKeys { get; }

        void Initialize(IntPtr handle);

        void Regist(Action keyPressed);

        void Release();
    }

    public class HotKeyNotifier : IHotKeyNotifier, IDisposable
    {
        private bool disposedValue;

        protected HotKeyRegister _hotKeyRegister;

        protected const int hotKeyID = 0;

        public bool RegistKeys => _hotKeyRegister != null;

        public void Initialize(IntPtr handle)
        {
            _hotKeyRegister = new HotKeyRegister(handle, hotKeyID, KeyModifiers.Control, Keys.R);
        }

        public void Regist(Action keyPressed)
        {
            _hotKeyRegister.HotKeyPressed += (s, e) => keyPressed();
        }

        public void Release()
        {
            _hotKeyRegister?.Dispose();
            _hotKeyRegister = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Release();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
