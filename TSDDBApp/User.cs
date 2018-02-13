namespace TSDDBApp
{
    public class User
    {
       
        private int _id;
        private string _username;
        private string _password;
        private string _name;
        private string _lastName;
        private string _email;
        private string _age;
        private bool _admin = false;
        private bool _loggedIn = false;
        private bool _exists = false;

        public User(string username = null, string password = null)
        {
            _username = (username);
            _password = (password);
        }

        public string Name
        {
            get => _name;
            set => _name = (value);
        }

        public string Email
        {
            get => _email;
            set => _email = (value);
        }


        public string LastName
        {
            get => _lastName;
            set => _lastName = (value);
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Username
        {
            get => _username;
            set => _username = (value);
        }

        public string Password
        {
            get => _password;
            set => _password = (value);
        }

        public string Age
        {
            get => _age;
            set => _age = (value);
        }

        public bool Admin
        {
            get => _admin;
            set => _admin = value;
        }


        public bool Exists
        {
            get => _exists;
            set => _exists = value;
        }



        public bool LoggedIn
        {
            get => _loggedIn;
            set => _loggedIn = value;
        }


    }
}
