using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
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

namespace _10_NPCarRegionClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NetworkStream ns = null;
        StreamReader streamReader = null;
        StreamWriter streamWriter = null;
        CancellationTokenSource cancellationToken;

        IPEndPoint serverEndPoint;
        TcpClient client;
        int serverPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]!);
        string serverIp = ConfigurationManager.AppSettings["ServerAdress"]!;


        MessageInfo minfo;
        static ObservableCollection<MessageInfo> messages = new ObservableCollection<MessageInfo>();
        public MainWindow()
        {
            InitializeComponent();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            client = new TcpClient();
            minfo = new MessageInfo();

            this.DataContext = messages;
            this.ipField.Text = serverIp;
            this.portField.Text = serverPort.ToString();
        }
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.Connect(serverEndPoint);
                ns = client.GetStream();
                streamReader = new StreamReader(ns);
                streamWriter = new StreamWriter(ns);

                cancellationToken = new CancellationTokenSource();
                Listen(cancellationToken.Token);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

            string msg = sendTxt.Text;
            if (string.IsNullOrWhiteSpace(msg)) return;

            streamWriter.WriteLine(msg);
            streamWriter.Flush();

            //MessageBox.Show(messages.Count.ToString());
        }
        private async void Listen(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var res = await streamReader.ReadLineAsync();

                if (minfo.Username == null) { minfo.Username = client.Client.LocalEndPoint.ToString(); }
                messages.Add(new MessageInfo(res, DateTime.Now, minfo.Username));
            }
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
            cancellationToken?.Cancel();
            streamReader?.Close();
            streamWriter?.Close();
            ns?.Close();
            client?.Close();
            MessageBox.Show("Disconected Succesfully");
        }
    }
}