using System.Diagnostics;
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

namespace _01_SPIntroToSystemProgrammingProcesses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grid.ItemsSource = Process.GetProcesses();
            //(grid.SelectedItem as Process)
            string message = " ";
            message += "Hellloooooooooo";

            MessageBox.Show(message, "Process info", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show(nameProc.Text);
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //grid.ItemsSource = Process.GetProcesses();
            var item = (grid.SelectedItem as Process);
            item.Kill();
            string message = $"Task {item.ProcessName} Killed!";

            MessageBox.Show(message, "Process info", MessageBoxButton.OK, MessageBoxImage.Information);
            //MessageBox.Show(nameProc.Text);
        }
    }
}