using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackSugar.SimpleMvp.WinForm;
using BlackSugar.Utility;
using BlackSugar.Entity;

namespace BlackSugar.Views
{
    public partial class RecListForm : ViewForm, IRecListView
    {
        protected List<ExplorerWindow> _model;

        protected ColorInfo _colorInfo;

        public RecListForm()
        {
            InitializeComponent();

            btnClose.Click += (s, e) => { this.Close(); };
            this.FormClosed += (s, e) => {
                if (this.Modal == false)
                    base.Dispose(true);

                _model = null;

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.Collect();
            };

            lstDetail.DoubleClick += (s, e) => {      
                SelectedAction(GetItem());
            };
            lstDetail.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                    SelectedAction(GetItem());
                else if (e.Control == true && (e.KeyCode == Keys.F || e.KeyCode == Keys.Q))
                    txtFilter.Focus();
            };

            txtFilter.TextChanged += (s, e) => {
                Func<ExplorerWindow, bool> filter 
                        = w => w.Path.ToUpper().IndexOf(txtFilter.Text.ToUpper().Trim()) >= 0;

                if (txtFilter.Text.Trim().Length == 0)
                    SetItem(_model);
                else
                    SetItem(_model.Where(filter).ToList());
            };

            UIHelper.SetWindowTitleBar(pnlTitlrBar, this);
            UIHelper.SetWindowTitleBar(lblTitle1, this);
            UIHelper.SetWindowTitleBar(lblTitle2, this);

            UIHelper.SetWindowTitleBar(pnlError, this);
            SwitchErrorPanel(false);
            btnOK.Click += (s, e) => { this.Close(); };
        }

        public Action<ExplorerWindow> SelectedAction { get; set; }

        private void SwitchErrorPanel(bool visible)
        {
            pnlError.Visible = visible;
            pnlBackSmokeTop.Visible = visible;
            pnlBackSmokeBottom.Visible = visible;
        }

        public ColorInfo ColorInfo
        {
            get => _colorInfo;
            set {
                _colorInfo = value;
                InitializeColor(_colorInfo);
            }
        }

        private void InitializeColor(ColorInfo colorInfo)
        {
            UIHelper.SetColor(this, null, colorInfo.DarkColor);
            UIHelper.SetColor(pnlHeader, null, colorInfo.DarkColor);
            UIHelper.SetColor(pnlTitlrBar, null, colorInfo.LightColor);
            UIHelper.SetColor(lblTitle1, colorInfo.ForeColor, null);
            UIHelper.SetColor(lblTitle2, Color.FromArgb(211, 56, 28), null);
            UIHelper.SetColor(btnClose, colorInfo.ForeColor, colorInfo.LightColor);
            UIHelper.SetColor(txtFilter, colorInfo.ForeColor, colorInfo.DarkColor);

            UIHelper.Gradation(pnlColorBar, colorInfo.GradationColorFrom, colorInfo.GradationColorTo, LinearGradientMode.Horizontal);
            UIHelper.SetColor(lstDetail, colorInfo.DarkColor, colorInfo.SelectedColor, colorInfo.ForeColor, Color.Gray);
        }

        public List<ExplorerWindow> Model {
            get => _model; 
            set {
                _model = value;
                SetItem(_model);
            }
        }

        private void SetItem(List<ExplorerWindow> models)
        {
            lstDetail.View = View.Tile;
            lstDetail.FullRowSelect = true;
            lstDetail.MultiSelect = false;
            lstDetail.TileSize = new Size(lstDetail.Width - 20, 40);
            lstDetail.Columns.AddRange(new ColumnHeader[]{ new ColumnHeader(), new ColumnHeader() });

            lstDetail.BeginUpdate();

            lstDetail.Items.Clear();

            foreach (var model in models)
            {
                ListViewItem item = new ListViewItem(new string[] { model.Name, model.Path });
                item.UseItemStyleForSubItems = false;
                item.SubItems[0].ForeColor = Color.White;
                lstDetail.Items.Add(item);
            };

            lstDetail.EndUpdate();
        }

        private ExplorerWindow GetItem()
        {
            foreach (ListViewItem item in lstDetail.SelectedItems)
            {
                return new ExplorerWindow()
                {
                    Name = item.SubItems[0].Text,
                    Path = item.SubItems[1].Text,
                };
            }
            return null;
        }

        public void ShowMessage(string message)
        {
            SwitchErrorPanel(true);
            lblErrorMessage.Text = message;
        }
    }
}
