using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Business;
using Common.ViewModels;

namespace ClientApp
{
    public class MultiLineListBox : ListBox
    {
        private EditTextBox editTextBox;
        public ChatClient ChatClient;

        public MultiLineListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.ScrollAlwaysVisible = true;

            editTextBox = new EditTextBox(this);
            editTextBox.Hide();
            editTextBox.MultiLineListBoxParent = this;
            Controls.Add(editTextBox);
        }

        public void SetChatClient(ChatClient client)
        {
            this.ChatClient = client;
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
        
        public void EditSelectedMessage(string username)
        {
            int index = SelectedIndex;

            if (index != ListBox.NoMatches && index != 65535)
            {
                var message = Items[index] as MessageVM;
                if (message != null)
                {
                    if (message.SenderUsername.Equals(username))
                    {
                        string s = message.Content;
                        Rectangle rect = GetItemRectangle(index);

                        editTextBox.Location = new Point(rect.X, rect.Y);
                        editTextBox.Size = new Size(rect.Width, rect.Height);
                        editTextBox.Text = s;
                        editTextBox.Index = index;
                        editTextBox.SelectAll();
                        editTextBox.Show();
                        editTextBox.Focus();
                    }
                    else
                    {
                        MessageBox.Show(this, "You can edit your messages only!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void DeleteSelectedMessage(string username)
        {
            int index = SelectedIndex;

            if (index != ListBox.NoMatches && index != 65535)
            {
                var message = Items[index] as MessageVM;
                if (message != null)
                {
                    if (message.SenderUsername.Equals(username))
                    {
                        ChatClient.DeleteMessage(message);
                    }
                    else
                    {
                        MessageBox.Show(this, "You can delete your messages only!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void EditMessage(MessageVM message)
        {
            string username = message.SenderUsername;
            long id = message.MessageId;

            for (int i = 0; i < Items.Count; i++)
            {
                var m = Items[i] as MessageVM;
                if (m?.MessageId == id && username == m.SenderUsername)
                {
                    m.Content = message.Content;
                    this.RefreshItem(i);
                    break;
                }
            }
        }

        public void DeleteMessage(MessageVM message)
        {
            string username = message.SenderUsername;
            long id = message.MessageId;

            for (int i = 0; i < Items.Count; i++)
            {
                var m = Items[i] as MessageVM;
                if (m?.MessageId == id && username == m.SenderUsername)
                {
                    this.Items.RemoveAt(i);
                    break;
                }
            }
        }

        public void DeleteAllMesages(MessageVM message)
        {
            string username = message.SenderUsername;

            var newList = new List<MessageVM>();
            for (int i = 0; i < Items.Count; i++)
            {
                var m = Items[i] as MessageVM;
                if (m?.SenderUsername != username)
                {
                    newList.Add(m);
                }
            }

            Items.Clear();
            foreach (var messageVm in newList)
            {
                Items.Add(messageVm);
            }
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
                var message = MultiLineListBoxParent.Items[Index] as MessageVM;
                if(message == null) return;
                if (!string.IsNullOrEmpty(Text.Trim()))
                {
                    if (message.Content != Text)
                    {
                        MultiLineListBoxParent.ChatClient.EditMessage(message, Text);
                    }
                    Hide();
                }
                else
                {
                    Text = message.Content;

                    Hide();
                    MultiLineListBoxParent.SelectedIndex = Index;
                }
            }
        }
    }
}