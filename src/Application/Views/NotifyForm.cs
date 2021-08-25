using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackSugar.Utility;

namespace BlackSugar.Views
{
    public class NotifyForm : Form, INotifyView
    {
        protected const int hotKeyID = 0;

        private System.ComponentModel.IContainer components;

        protected NotifyIcon notifyIcon;

        protected ContextMenuStrip contextMenuStrip;

        protected ToolStripMenuItem openStripItem;

        protected ToolStripMenuItem closeStripItem;

        protected Timer timer;

        //protected HotKeyRegister hotkeyRegister;

        public Action OpenAction { get; set; }

        public Action CloseAction { get; set; }

        public Action ColorSettingAction { get; set; }

        public Action RoopAction { get; set; }

        public Action ToggleHotKeyAction { get; set; }

        public string ToggleHotKey { 
            get => "hotkey" ;
            set {
                UIHelper.SetContextMenuItem("toggleHotKey", value, contextMenuStrip);
            } }

        private void SetItemText(string name, string text)
        {
            contextMenuStrip.Items[name].Text = text;
        }

        public NotifyForm()
        {
            components = new System.ComponentModel.Container();

            contextMenuStrip = new ContextMenuStrip(components);
            contextMenuStrip.Items.AddRange(new ToolStripMenuItem[] {
                new ToolStripMenuItem("Setting(Color)", null, (s, e) => ColorSettingAction(),"colorSetting"),
                new ToolStripMenuItem("EableHotKey", null, (s, e) => ToggleHotKeyAction(),"toggleHotKey"),
                new ToolStripMenuItem("Open", null, (s, e) => OpenAction(), "open"),
                new ToolStripMenuItem("Close", null, (s, e) => CloseAction(), "close"),
            });

            notifyIcon = new NotifyIcon(components);
            notifyIcon.DoubleClick += (s, e) => OpenAction();
            notifyIcon.Visible = true;
            notifyIcon.Icon = Properties.Resources.hiro_s;
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Text = "ExplorerREC";

            timer = new Timer(components);
            timer.Interval = 1000;
            timer.Tick += (s, e) => RoopAction();

            //hotkeyRegister = new HotKeyRegister(this.Handle, hotKeyID, KeyModifiers.Control, Keys.R);
            //hotkeyRegister.HotKeyPressed += (s, e) => OpenAction();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                //hotkeyRegister.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Exit()
        {
            notifyIcon.Visible = false;
            this.Close();
            this.Dispose();
        }

        public void SetTimerInterval(int interval)
        {
            timer.Interval = interval;
        }

        public void TimerStart()
        {
            timer.Start();
        }

        public void TimerStop()
        {
            timer.Stop();
        }
    }
}
