using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_SPAsyncAwait
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
        //C:\Users\Admin\Pictures\Saved Pictures\vinnuk.jpg
        //C:\Users\Admin\Desktop\test
        private async void copyButton_Click(object sender, RoutedEventArgs e)
        {
            if (fromField.Text != null && toField.Text != null)
            {
                string from = fromField.Text; string to = toField.Text;

                string fname = System.IO.Path.GetFileName(from);
                string destination = System.IO.Path.Combine(to, fname);

                try
                {
                    await copyFunctionAsync(from, destination);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show("File Succesfully copied!");
            }
        }
        private Task copyFunctionAsync(string f, string t )
        {

            return Task.Run(() => File.Copy(f, t,true));
        }
    }
}