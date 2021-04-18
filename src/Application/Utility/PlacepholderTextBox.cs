using System;
using System.Drawing;

namespace BlackSugar.Controls
{
    public class PlaceholderTextBox : System.Windows.Forms.TextBox
    {
        private string _placeholder = string.Empty;

        // （プロパティ）
        public string Placeholder
        {
            get { return _placeholder; }
            set
            {
                _placeholder = value;
                Invalidate();
            }
        }

        private Color _placeholderColor;
    
        public Color PlaceholderColor
        {
            get { return _placeholderColor; }
            set
            {
                _placeholderColor = value;
                Invalidate();
            }
        }

        public PlaceholderTextBox()
        {
            // プレースホルダのテキスト色を、前景色と背景色の中間
            _placeholderColor = System.Drawing.Color.FromArgb((this.ForeColor.A >> 1 + this.BackColor.A >> 1), (this.ForeColor.R >> 1 + this.BackColor.R >> 1), ((this.ForeColor.G >> 1 + this.BackColor.G) >> 1), (this.ForeColor.B >> 1 + this.BackColor.B >> 1));

        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 15) //  WM_PAINT == 15
            {
                if (this.Enabled && !this.ReadOnly && !this.Focused && (_placeholder != null) && (_placeholder.Length > 0) && (this.TextLength == 0))
                {
                    using (var g = this.CreateGraphics())
                    {
                        // erase drawing control
                        g.FillRectangle(new System.Drawing.SolidBrush(this.BackColor), this.ClientRectangle);

                         g.DrawString(_placeholder, this.Font, new System.Drawing.SolidBrush(_placeholderColor), 1, 1);
                    }
                }
            }
        }
    }
}