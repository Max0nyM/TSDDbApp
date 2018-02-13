using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace TSDDBApp
{
    public partial class Form1 : Form
    {
        private Db _db;
        private User _user;
        private User[] _userList;

        public Form1(Db db,User user)
        {
         
            InitializeComponent();
            this._db = db;
            this._user = user;
            if (user.Admin)
            {
                this.Text += " [Administrator]";
            }
            else
            {
                this.Text += " [User]";
                contextMenuStrip1.Items[0].Visible = false;
                contextMenuStrip1.Items[2].Visible = false;
            }
               
        }


        public void SetFocus(int i)
        {
            listView1.Items[i].Focused = true;
        }



        void Update()
        {
             listView1.Items.Clear();
            _userList = _db.GetUsers();
            foreach(var user in _userList)
            {
                ListViewItem item = new ListViewItem(user.Id.ToString(), 0);
                item.SubItems.Add(user.Name);
                item.SubItems.Add(user.LastName);
                item.SubItems.Add(user.Email);
                item.SubItems.Add(user.Age);
                listView1.Items.Add(item);
            }
           
            
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            Update();
        }

        private void deleteStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _db.Delete(_userList[listView1.SelectedItems[0].Index]);
            Update();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudent add = new AddStudent(_db);
            add.ShowDialog();
            Update();
        }

      
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
            if (listView1.SelectedItems.Count < 1)
            {
                contextMenuStrip1.Items[1].Enabled = false;
                contextMenuStrip1.Items[2].Enabled = false;
            }
            else
            {
                if (_user.Admin)
                {
                    if (_user.Id.ToString() == listView1.SelectedItems[0].SubItems[0].Text)
                    {
                        contextMenuStrip1.Items[2].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items[0].Enabled = true;
                        contextMenuStrip1.Items[1].Enabled = true;
                        contextMenuStrip1.Items[2].Enabled = true;
                    }
                

                }
                else
                {
                    if (_user.Id.ToString() == listView1.SelectedItems[0].SubItems[0].Text)
                    {
                       contextMenuStrip1.Items[1].Enabled = true;
                    }
                    else
                    {
                        contextMenuStrip1.Items[1].Enabled = false;
                    }
                }
              
                            
            }
        }

        private void updateInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User selectedUser = new User();
            selectedUser = _userList[listView1.SelectedItems[0].Index];
            EditStudent edit = new EditStudent(_db, selectedUser);
            edit.ShowDialog();
            Update();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
           SearchStudent search = new SearchStudent(_db);
           search.ShowDialog();
           Update();
        }
    }
}
