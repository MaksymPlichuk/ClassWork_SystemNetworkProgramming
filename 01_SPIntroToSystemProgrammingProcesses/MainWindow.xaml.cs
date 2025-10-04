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
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var item = (grid.SelectedItem as Process);
            item.Kill();
            string message = $"Task {item.ProcessName} Killed!";
        }
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            var item = (grid.SelectedItem as Process);
            string message = $"Task Name: {item.ProcessName}\nID: {item.Id}\nMachineName: {item.MachineName}\nPriority: {item.BasePriority}\nPagedMemorySize(KB): {item.PagedMemorySize64}\nStart Time: {item.StartTime}\nTotal Processor Time: {item.TotalProcessorTime}\nUser Processor Time: {item.UserProcessorTime}";

            MessageBox.Show(message, "Process info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void RunProcess(object sender, RoutedEventArgs e)
        {
            var item = nameProc.Text;
            MessageBox.Show(item, "Running: ", MessageBoxButton.OK, MessageBoxImage.Information);
            Process.Start(item);
        }
    }
}