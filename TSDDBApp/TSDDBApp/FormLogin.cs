using System;
using System.Windows.Forms;

namespace TSDDBApp
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        Db _db;
        private void button1_Click(object sender, EventArgs e)
        {

            if (_db.Connected() == System.Data.ConnectionState.Open) { 
            User user = new User(textBox1.Text, textBox2.Text);
            user = _db.InitUser(user);


            if (user.Exists)
            {
                if (user.LoggedIn)
                {                    
                    Form1 form = new Form1(_db,user);
                    form.ShowDialog();
                 }
                else
                {
                        Utils.Error("Wrong Password");
                }
            }
            else
            {
                    Utils.Error("User not exists");
            }
            }
            else
            {
                Utils.Error("MySQL Server is not working");
            }

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            _db = new Db();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_db.Connected() == System.Data.ConnectionState.Open)
            {
                AddStudent add = new AddStudent(_db);
                add.ShowDialog();
            }
            else
                Utils.Error("MySQL Server is not working");

        }
    }
}
