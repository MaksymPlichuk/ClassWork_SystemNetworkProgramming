using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System.IO;
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

namespace _06_SPOpenFileDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel model = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            model.Progress = 0;
            this.DataContext = model;
        }

        private void sourceBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.IsFolderPicker = false;

            if (fileDialog.ShowDialog()==CommonFileDialogResult.Ok)
            {
                model.Source = sourceText.Text = fileDialog.FileName;
            }
        }
        private void destBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog folderDiag = new CommonOpenFileDialog();
            folderDiag.IsFolderPicker = true;

            if (folderDiag.ShowDialog() == CommonFileDialogResult.Ok)
            {
                model.Destination = destText.Text = folderDiag.FileName;
            }
        }
        private async void copyBtn(object sender, RoutedEventArgs e)
        {
            string fname = System.IO.Path.GetFileName(model.Source);
            string destFilename = System.IO.Path.Combine(model.Destination, fname);
            await CopyFileAsync(model.Source, destFilename);
            MessageBox.Show("File Copied!");
            model.Progress = 0;
        }
        private Task CopyFileAsync(string src, string dest)
        {
            return Task.Run(() =>
            {

                using FileStream srcStream = new FileStream(src, FileMode.Open, FileAccess.Read);
                using FileStream desStream = new FileStream(dest, FileMode.Create, FileAccess.Write);
                byte[] buffer = new byte[1024 * 8];
                int bytes = 0;
                do
                {
                    bytes = srcStream.Read(buffer, 0, buffer.Length);
                    desStream.Write(buffer, 0, bytes);

                    float percentage = desStream.Length / (srcStream.Length / 100);   
                    model.Progress = percentage;
                } while (bytes > 0);
            });
        }

    }
    [AddINotifyPropertyChangedInterface]
    class ViewModel
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public float Progress { get; set; }
        public bool IsWaiting => Progress == 0;
    }
}