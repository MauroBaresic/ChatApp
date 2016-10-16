using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ClientApp
{
    public class MultiLineListBox : ListBox
    {
        private EditTextBox editTextBox;

        public MultiLineListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.ScrollAlwaysVisible = true;

            editTextBox = new EditTextBox(this);
            editTextBox.Hide();
            editTextBox.MultiLineListBoxParent = this;
            Controls.Add(editTextBox);

        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                string s = Items[e.Index].ToString();
                SizeF sf = e.Graphics.MeasureString(s, Font, Width);
                int htex = (e.Index == 0) ? 15 : 10;
                e.ItemHeight = (int)sf.Height + htex;
                e.ItemWidth = Width;
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                string s = Items[e.Index].ToString();

                if ((e.State & DrawItemState.Selected) == 0) // not selected item
                {
                    e.Graphics.FillRectangle(new SolidBrush(ChatAppColors.ControlBackColor), e.Bounds);
                    e.Graphics.DrawString(s, Font, new SolidBrush(ChatAppColors.ForeColor), new RectangleF(e.Bounds.X, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height));
                    e.Graphics.DrawRectangle(new Pen(ChatAppColors.BackColor), e.Bounds);
                }
                else // selected item
                {
                    e.Graphics.FillRectangle(new SolidBrush(ChatAppColors.ForeColor), e.Bounds);
                    e.Graphics.DrawString(s, Font, new SolidBrush(ChatAppColors.BackColor), new RectangleF(e.Bounds.X, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height));
                    e.Graphics.DrawRectangle(new Pen(ChatAppColors.BackColor), e.Bounds);
                }
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            int index = IndexFromPoint(e.X, e.Y);

            if (index != ListBox.NoMatches &&
                index != 65535)
            {
                if (e.Button == MouseButtons.Right)
                {
                    string s = Items[index].ToString();
                    Rectangle rect = GetItemRectangle(index);

                    editTextBox.Location = new Point(rect.X, rect.Y);
                    editTextBox.Size = new Size(rect.Width, rect.Height);
                    editTextBox.Text = s;
                    editTextBox.Index = index;
                    editTextBox.SelectAll();
                    editTextBox.Show();
                    editTextBox.Focus();
                }
            }

            base.OnMouseUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                int index = SelectedIndex;
                if (index == ListBox.NoMatches ||
                    index == 65535)
                {
                    if (Items.Count > 0)
                        index = 0;
                }
                if (index != ListBox.NoMatches &&
                    index != 65535)
                {

                    string s = Items[index].ToString();
                    Rectangle rect = GetItemRectangle(index);

                    editTextBox.Location = new Point(rect.X, rect.Y);
                    editTextBox.Size = new Size(rect.Width, rect.Height);
                    editTextBox.Text = s;
                    editTextBox.Index = index;
                    editTextBox.SelectAll();
                    editTextBox.Show();
                    editTextBox.Focus();
                }
            }
            base.OnKeyDown(e);
        }


        class EditTextBox : TextBox
        {
            public MultiLineListBox MultiLineListBoxParent;
            public int Index = -1;

            public EditTextBox(MultiLineListBox inMultiLineListBox)
            {
                MultiLineListBoxParent = inMultiLineListBox;
                Multiline = true;
                ScrollBars = ScrollBars.Vertical;
                ForeColor = ChatAppColors.ForeColor;
                BackColor = ChatAppColors.ControlBackColor;
            }
            

            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);

                if (e.KeyChar == 13) //enter
                {
                    Validate();
                }

                if (e.KeyChar == 27) //escape
                {
                    Text = "";
                    Validate();
                }
            }

            protected override void OnLostFocus(System.EventArgs e)
            {
                Validate();

                base.OnLostFocus(e);
            }

            private void Validate()
            {
                if (!string.IsNullOrEmpty(Text.Trim()))
                {
                    MultiLineListBoxParent.Items[Index] = Text;
                    Hide();
                }
                else
                {
                    Text = MultiLineListBoxParent.Items[Index].ToString();
                    Hide();
                    MultiLineListBoxParent.SelectedIndex = Index;
                }
            }
        }
    }
}