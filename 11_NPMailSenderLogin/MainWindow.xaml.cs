using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _11_NPMailSenderLogin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow window = new LoginWindow(Login,Password);
            if (window.ShowDialog() == true)
            {
                MessageBox.Show(Login + " " + Password);
            }
            window.Hide();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}