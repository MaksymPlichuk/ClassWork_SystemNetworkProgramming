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

namespace _09_NPMessengerClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IPEndPoint serverEndPoint;
        UdpClient client;
        int serverPort = 4040;
        string serverIp = "127.0.0.1";


        MessageInfo minfo;
        static ObservableCollection<MessageInfo> messages = new ObservableCollection<MessageInfo>();
        public MainWindow()
        {
            InitializeComponent();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            client = new UdpClient();
            minfo = new MessageInfo();

            this.DataContext = messages;
            this.ipField.Text = serverIp;
            this.portField.Text = serverPort.ToString();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            string msg = "$<JOIN>";
            SendToServer(msg);
            Listen();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

            string msg = sendTxt.Text;
            if (string.IsNullOrWhiteSpace(msg)) return;

            SendToServer(msg);
            MessageBox.Show(messages.Count.ToString());
        }
        private async void Listen()
        {
            while (true)
            {
                var res = await client.ReceiveAsync();
                string msg = Encoding.UTF8.GetString(res.Buffer);
                if (minfo.Username==null) { minfo.Username = client.Client.LocalEndPoint.ToString(); }
                messages.Add(new MessageInfo(msg, DateTime.Now, minfo.Username));
            }
        }
        
        private async void SendToServer(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            await client.SendAsync(data, data.Length, serverEndPoint);

        }
        public class MessageInfo
        {
            public string Message { get; set; }
            public DateTime Time { get; set; }
            public string Username { get; set; }

            public MessageInfo()
            {
                
            }
            public MessageInfo(string m, DateTime t, string username)
            {
                Message = m; Time = t;
                Username = username;
            }

            public override string ToString()
            {
                return $"User: {Username}. Message: {Message}. Time: {Time}";
            }
        }

        private void UsernameButton_Click(object sender, RoutedEventArgs e)
        {
            minfo.Username = userTxt.Text;
        }
        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            string msg = "$<DISC>";
            SendToServer(msg);
        }
    }
}