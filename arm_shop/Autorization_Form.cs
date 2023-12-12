namespace arm_shop
{
    public partial class Autorization_Form : Form
    {
        static public string pass, log, whoIs;
        SqlCommands commands = new SqlCommands();
        public Autorization_Form()
        {
            InitializeComponent();
        }

        private void Autorization_Form_Load(object sender, EventArgs e)
        {
            CenterToParent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            var p = Autorization.NewHash(Autorization.GenerateHash(password));
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Autorization.AutorizationMethod(login, password);
                switch (Autorization.role)
                {
                    case null:
                        MessageBox.Show("Пользователь не найден!");
                        break;
                    case "master":
                        if (login == Autorization.login_a && Autorization.NewHash(Autorization.GenerateHash(password)) == Autorization.password_a)
                        {
                            whoIs = "master";
                            pass = Autorization.password_a;
                            log = Autorization.login_a;
                            //this.Hide();
                        }
                        break;
                    case "client":
                        if (login == Autorization.login_a && Autorization.NewHash(Autorization.GenerateHash(password)) == Autorization.password_a)
                        {
                            whoIs = "client";
                            pass = Autorization.password_a;
                            log = Autorization.login_a;
                            //this.Hide();
                        }
                        break;
                }

            }
        }
    }
}