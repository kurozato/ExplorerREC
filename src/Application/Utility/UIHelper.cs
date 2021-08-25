using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;


namespace BlackSugar.Utility
{
    public class UIHelper
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
            [DllImportAttribute("user32.dll")]
            internal static extern bool ReleaseCapture();
        }

        public static void SetWindowTitleBar(Control control, Form form)
        {
            control.MouseDown += (s, e) => {

                if (e.Button == MouseButtons.Left)
                {
                    //マウスのキャプチャを解除
                    NativeMethods.ReleaseCapture();
                    //タイトルバーでマウスの左ボタンが押されたことにする
                    NativeMethods.SendMessage(form.Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
                }
            };
        }

        public static void SetContextMenuItem(string name, string text, ContextMenuStrip contextMenuStrip)
        {
            foreach (ToolStripMenuItem item in contextMenuStrip.Items.Find(name, true))
            {
                item.Text = text;
            }
        }

        public static Control FindControl(string key, Control target)
        {
            foreach (Control control in target.Controls.Find(key, true))
            {
                return control;
            }
            return null;
        }

        public static void Gradation(Control control, Color color1, Color color2, LinearGradientMode mode)
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(control.Width, control.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            //g.VisibleClipBoundsは表示クリッピング領域に外接する四角形
            LinearGradientBrush gb = new LinearGradientBrush(g.VisibleClipBounds, color1, color2, mode);

            //四角を描く
            g.FillRectangle(gb, g.VisibleClipBounds);

            //リソースを解放する
            gb.Dispose();
            g.Dispose();

            control.BackgroundImage = canvas;
        }

        public static void SetColor(Control control, Color? foreColor, Color? backColor)
        {
            if (foreColor != null && foreColor != Color.Empty)
                control.ForeColor = (Color)foreColor;

            if (backColor != null && backColor != Color.Empty)
                control.BackColor = (Color)backColor;
        }

        public static void SetColor(ListView listView, Color backColor, Color selectedColor, Color foreColor, Color subItemForeColor)
        {
            listView.OwnerDraw = true;
            listView.UseCompatibleStateImageBehavior = false;
            listView.ForeColor = foreColor;
            listView.BackColor = backColor;
            listView.DrawItem += (s, e) => DrawItem(s, e, selectedColor, foreColor, subItemForeColor);
        }

        private static void DrawItem(object sender, DrawListViewItemEventArgs e, Color listViewSelectionColor, Color foreColor, Color subItemForeColor)
        {
            ListView lView = sender as ListView;
            if (lView.View == View.Details) return;
            TextFormatFlags flags = GetTextAlignment(lView, 0);
            //Color itemColor = e.Item.ForeColor;
            if (e.Item.Selected)
            {
                using (SolidBrush bkgrBrush = new SolidBrush(listViewSelectionColor))
                {
                    e.Graphics.FillRectangle(bkgrBrush, e.Bounds);
                }
                //itemColor = e.Item.BackColor;
            }
            else
            {
                e.DrawBackground();
            }
            var font = new Font(e.Item.Font.FontFamily, 12);
            TextRenderer.DrawText(e.Graphics, e.Item.Text, font, e.Bounds, foreColor, flags);

            if (lView.View == View.Tile && e.Item.SubItems.Count > 1)
            {
                var subItem = e.Item.SubItems[1];
                flags = GetTextAlignment(lView, 1);
                TextRenderer.DrawText(e.Graphics, subItem.Text, subItem.Font, e.Bounds, subItemForeColor, flags);
            }
        }

        private static TextFormatFlags GetTextAlignment(ListView lstView, int colIndex)
        {
            TextFormatFlags flags = (lstView.View == View.Tile)
                ? (colIndex == 0) ? TextFormatFlags.Default : TextFormatFlags.Bottom
                : TextFormatFlags.VerticalCenter;

            flags |= TextFormatFlags.LeftAndRightPadding | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis;
            switch (lstView.Columns[colIndex].TextAlign)
            {
                case HorizontalAlignment.Left:
                    flags |= TextFormatFlags.Left;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.Right;
                    break;
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
            }
            return flags;
        }

        public static void ClickBubble(Action<object, EventArgs> clickAction, Control root)
        {
            root.Click += (s, e) => clickAction(s, e);

            foreach(Control control in root.Controls)
                control.Click += (s, e) => clickAction(s, e);
        }

    }
}
