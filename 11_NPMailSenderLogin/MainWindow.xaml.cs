using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        public ObservableCollection<string> FileToMail { get; set; }
        bool priority = false;
        public string Login { get; set; }
        public string Password { get; set; }

        string server = "smtp.gmail.com";
        int port = 587;

        SmtpClient client;
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow window = new LoginWindow(Login,Password);
            if (window.ShowDialog() == true)
            {
                MessageBox.Show(Login + " " + Password);
            }
            window.Hide();

           Login=window.Login;
           Password=window.Password;
           rightEmailTxt.Text = Login;
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
            MailMessage msg = new MailMessage(rightEmailTxt.Text, toTxt.Text, themeTxt.Text, msgTxt.Text);
            using (StreamReader sr = new StreamReader("mail.html"))
            {
                msg.Body = sr.ReadToEnd();
            }
            msg.IsBodyHtml = true;

            if (priority == true)
            {
                msg.Priority = MailPriority.High;
            }

            foreach (string path in FileToMail) {
                msg.Attachments.Add(new Attachment(@$"{path}"));
            }
            msg.BodyEncoding = Encoding.UTF8;
            msg.SubjectEncoding = Encoding.UTF8;

            MessageBox.Show("Succesfully added Files!");
            MessageBox.Show(msg.Attachments.Count.ToString());
            client = new SmtpClient(server, port);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(Login, Password);


            client.SendCompleted += Client_SendCompleted;
            client.SendAsync(msg, msg);
        }
        private void Client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {

            var state = (MailMessage)e.UserState;
            MessageBox.Show($"Message was sent! Subject: {state.Subject}!");
        }
        private void attachBtn_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.IsFolderPicker = false;

            string path = "";

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
            client.Dispose();
        }
    }
}