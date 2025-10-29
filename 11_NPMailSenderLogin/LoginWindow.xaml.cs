using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _11_NPMailSenderLogin
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Login { get; set; }
        public string Password { get; set; }

        private string rightLogin = "lenailyshun@gmail.com";
        private string rightPassword = "dqmq yyqu uxfb ikfc";


        public LoginWindow()
        {
            InitializeComponent();
        }
        public LoginWindow(string login,string password)
        {
            InitializeComponent();
            this.Login = login;
            this.Password = password;

            logintb.Text = rightLogin;
            passtb.Text = rightPassword;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login = logintb.Text;
            Password = passtb.Text;
            if (Login == rightLogin && Password == rightPassword)
            {
                this.Hide();
            }
            else { MessageBox.Show("Wrong Credentials!"); }
            
        }
    }
}
