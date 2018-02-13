using System;
using System.Drawing;
using System.Windows.Forms;

namespace TSDDBApp
{
    public partial class AddStudent : Form
    {
        private readonly Db _db;
        public AddStudent(Db db)
        {
            InitializeComponent();
            this._db = db;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.BackColor = Color.Pink;
                textBox5.Focus();
                return;
            }
            if (textBox4.Text == "")
            {
                textBox4.BackColor = Color.Pink;
                textBox4.Focus();
                return;
            }
            if (textBox1.Text == "")
            {
                textBox1.BackColor = Color.Pink;
                textBox1.Focus();
                return;
             }
            if (textBox2.Text == "")
            {
                textBox2.BackColor = Color.Pink;
                textBox2.Focus();
                return;
            }
            if (textBox3.Text == "")
            {
                textBox3.BackColor = Color.Pink;
                textBox3.Focus();
                return;
            }
      
       
            User newUser = new User
            {
                Password = textBox4.Text,
                Username = textBox5.Text,
                Name = textBox1.Text,
                LastName = textBox2.Text,
                Email = textBox3.Text,
                Age = numericUpDown1.Value.ToString()
            };
            if (Utils.CheckUnameLength(newUser.Username))
            {
                if (!_db.GetUser(newUser.Username, newUser.Email)) 
                 Utils.Error("Username or Email is already taken");
                else
                if (!Utils.IsValid(newUser.Email))
                 Utils.Error("Email is not valid");
                else
                if (Convert.ToInt32(newUser.Age)<18)
                    Utils.Error("Age must be >= 18");
                else
                { 
                  _db.Insert(newUser);
                  base.Close();
                 }
                }
            else
            {
                Utils.Error("Username must be at least 5 characters");
            }
        }
    }
}
