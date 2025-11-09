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
using _12_NPMailKit.Library;



namespace _12_NPMailKit
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
            logintb.Text = rightLogin;
            passtb.Text = rightPassword;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login = logintb.Text.Trim();
            Password = passtb.Text;

            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password)) {
                MessageBox.Show("Enter right credentials");
                return;
            }

            var imap = new ImapService();
            try
            {
                this.IsEnabled = false;

                await imap.ConnectAsync(Login, Password);

                SessionState.Email = Login;
                SessionState.Password = Password;

                var main = new MainWindow(imap);
                main.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed");
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }

}
