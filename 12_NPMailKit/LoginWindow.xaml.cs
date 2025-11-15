using _12_NPMailKit.Entities;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows;



namespace _12_NPMailKit
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string rightLogin = "lenailyshun@gmail.com";
        private string rightPassword = "dqmq yyqu uxfb ikfc";
        public LoginWindow()
        {
            InitializeComponent();
            LoginTxt.Text = rightLogin;
            passTxt.Text = rightPassword;
        }
        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTxt.Text == rightLogin && passTxt.Text == rightPassword)
            {
                var user = new SenderUser(rightLogin, rightPassword);

                MailBox mailBox = new MailBox();
                mailBox.LoggedUser = user;

                this.Close();
                mailBox.Show();
            }
            else { MessageBox.Show("Wrong Credentials!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
           
        }
    }
}
