using _12_NPMailKit.Entities;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.WindowsAPICodePack.Dialogs;
using MimeKit;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _12_NPMailKit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public ObservableCollection<string> FileToMail { get; set; }
        bool priority = false;
        public SenderUser LoggedUser { get; set; }
        public string RecieverMail { get; set; }

        MailKit.Net.Smtp.SmtpClient client;

        public MainWindow()
        {
            InitializeComponent();

            FileToMail = new ObservableCollection<string>();
            this.DataContext = this;
        }

        class attachedFiles
        {
            public string Name { get; set; }
            public attachedFiles(string N)
            {
                Name = N;
            }
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", LoggedUser.Login));
                message.To.Add(new MailboxAddress("", toTxt.Text));
                message.Subject = themeTxt.Text;
                message.Importance = MessageImportance.High;

                var builder = new BodyBuilder();
                builder.TextBody = msgTxt.Text;

                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mail.html");
                if (File.Exists(htmlPath))
                {
                    builder.HtmlBody = File.ReadAllText(htmlPath, Encoding.UTF8);
                }
                else { MessageBox.Show("File wasn't found!"); }


                foreach (string path in FileToMail)
                {

                    builder.Attachments.Add(@$"{path}");
                }
                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                    client.Authenticate(LoggedUser.Login, LoggedUser.Password);
                    client.Send(message);


                    client.Disconnect(true);
                    MessageBox.Show("Successfuly Sent message!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            

        }
      
        private void attachBtn_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.IsFolderPicker = false;

            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FileToMail.Add(fileDialog.FileName);
            }

        }

        private void newtextBtn_Click(object sender, RoutedEventArgs e)
        {
            FileToMail.Clear();
            msgTxt.Text = string.Empty;
            themeTxt.Text = string.Empty;
            toTxt.Text = string.Empty;
        }

        private void priorityBtn_Click(object sender, RoutedEventArgs e)
        {
            priority = true;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void FillRightEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            rightEmailTxt.Text = LoggedUser?.Login;
            toTxt.Text = RecieverMail;
        }
    }
}