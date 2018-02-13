using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSDDBApp
{
    public partial class SearchStudent : Form
    {
        private Db _db;
        private User[] _userList;

        public SearchStudent(Db db)
        {
            InitializeComponent();
            this._db = db;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            StringBuilder paramList = new StringBuilder();
            User searchUser = new User();
            if (textBox1.Text != "") { 
                paramList.Append(" and name=@name");
                searchUser.Name = textBox1.Text;
            }
            if (textBox2.Text != "")
            { 
                paramList.Append(" and last_name=@last_name");
                searchUser.LastName = textBox2.Text;
            }
            if (textBox3.Text != "") { 
                paramList.Append(" and username=@username");
                searchUser.Username = textBox3.Text;
            }
            if (textBox4.Text != "")
            {
                paramList.Append(" and email=@email");
                searchUser.Email = textBox4.Text;
            }
            if (numericUpDown1.Text != "") { 
                paramList.Append(" and age=@age");
                searchUser.Age = numericUpDown1.Text;
            }

            _userList = _db.SearchUsers(paramList.ToString(),searchUser);
            foreach (var user in _userList)
            {
                ListViewItem item = new ListViewItem(user.Id.ToString(), 0);
                item.SubItems.Add(user.Name);
                item.SubItems.Add(user.LastName);
                item.SubItems.Add(user.Email);
                item.SubItems.Add(user.Age);
                listView1.Items.Add(item);
            }

        }
    }
}
