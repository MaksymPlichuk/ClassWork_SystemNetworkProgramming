using PropertyChanged;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
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

namespace _08_NPClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel model = new();
        UdpClient client;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = model;
            client = new UdpClient();
            model.Ip = "127.0.0.1";
            model.Port = 8080;
        }

        private async void SendData(object sender, RoutedEventArgs e)
        {
            string msg = model.Message;
            if (string.IsNullOrWhiteSpace(msg)) return;

            model.Messages.Add(new MessageItem { Text = msg, Time = DateTime.Now.ToShortTimeString() });

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                await client.SendAsync(data, data.Length, model.Ip, model.Port);

                var result = await client.ReceiveAsync();
                string response = Encoding.UTF8.GetString(result.Buffer);
                model.Messages.Add(new MessageItem { Text = response, Time = DateTime.Now.ToShortTimeString() });
            }
            catch (Exception ex)
            {
                model.Messages.Add(new MessageItem { Text = $"Error: {ex.Message}", Time = DateTime.Now.ToShortTimeString() });
            }

            model.Message = string.Empty;
        }

    }
    class MessageItem
    {
        public string Text { get; set; }
        public string Time { get; set; }
    }
    [AddINotifyPropertyChangedInterface]
    class ViewModel
    {
        public int Port { get; set; }
        public string Ip { get; set; }
        public string Message { get; set; }
        public ObservableCollection<MessageItem> Messages { get; set; }
        public ViewModel()
        {
            //Message = "Hello";
            //Port = 8080;
            //Ip = "127.0.0.1";
            Messages = new ObservableCollection<MessageItem>();
        }
    }
}