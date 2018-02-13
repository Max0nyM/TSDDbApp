using System;
using System.Drawing;
using System.Windows.Forms;

namespace TSDDBApp
{
    public partial class EditStudent : Form
    {
        readonly Db _db;
        int _id;
        readonly User _updateUser;
        public EditStudent(Db db, User user)
        {
            InitializeComponent();
            this.textBox1.Text = user.Name;
            this.textBox2.Text = user.LastName;
            this.textBox3.Text = user.Email;
            this.numericUpDown1.Value = Convert.ToDecimal(user.Age);
            this._db = db;
            this._id = user.Id;
            _updateUser = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
     
         
            _updateUser.Name = textBox1.Text;
            _updateUser.LastName = textBox2.Text;
            _updateUser.Email = textBox3.Text;
            _updateUser.Age = numericUpDown1.Value.ToString();




            if (!_db.CheckUser(_updateUser.Email, _updateUser.Id.ToString()))
                Utils.Error("Email is already taken");
            else
                if (!Utils.IsValid(_updateUser.Email))
                Utils.Error("Email is not valid");
            else
                if (Convert.ToInt32(_updateUser.Age) < 18)
                Utils.Error("Age must be >= 18");
            else
            {
                    _db.Update(_updateUser);
                    base.Close();
                }
          
        }
    }
}

