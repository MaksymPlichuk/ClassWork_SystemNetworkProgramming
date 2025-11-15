using _12_NPMailKit.Entities;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace _12_NPMailKit
{
    /// <summary>
    /// Interaction logic for MailBox.xaml
    /// </summary>
    public partial class MailBox : Window
    {
        public SenderUser LoggedUser { get; set; }
        public ObservableCollection<string> Folders { get; set; }
        public ObservableCollection<MailItem> Mails { get; set; }
        public ImapClient client { get; set; }
        public MailKit.IMailFolder folder { get; set; }

        public MailBox()
        {
            InitializeComponent();
            Folders = new ObservableCollection<string>();
            Mails = new ObservableCollection<MailItem>();
            DataContext = this;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = MailsBox.SelectedItem as MailItem;

                folder.AddFlags(item.Uid, MessageFlags.Deleted, true);
                folder.Expunge();

                Mails.Remove(item);
                MessageBox.Show($"Successfuly deleted Mail with ID[ {item.Uid} ]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (folder != null && folder.IsOpen) { folder?.Close(); }
                Mails.Clear();
                Folders.Clear();
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void FoldexBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFolderName = FoldersBox.SelectedItem.ToString();
            LoadMailsFromFolder(selectedFolderName);
        }
        private void LoadMailsFromFolder(string folderName)
        {
            if (folder != null && folder.IsOpen) { folder?.Close(); }

            folder = client.GetFolder(folderName);
            folder.Open(FolderAccess.ReadWrite);

            Mails.Clear();

            var uids = folder.Search(SearchQuery.All).Take(20).ToList();

            foreach (var uid in uids)
            {
                var message = folder.GetMessage(uid);
                Mails.Add(new MailItem
                {
                    Uid = uid,
                    DisplayMsg = $"{message.From} - {message.Subject}"
                });
            }

        }

        private void ReplyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MailsBox.SelectedItems == null) { MessageBox.Show("Choose to what Mail you Want To Reply!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                MailItem selecetedMail = MailsBox.SelectedItem as MailItem;
                var message = folder.GetMessage(selecetedMail.Uid);


                MainWindow mainWindow = new MainWindow();
                mainWindow.LoggedUser = LoggedUser;
                mainWindow.RecieverMail = message.To.ToString();
                mainWindow.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {

            client = new ImapClient();
            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
            client.Authenticate(LoggedUser.Login, LoggedUser.Password);
            LoadData(client);

            //var name = client.Inbox.Open(FolderAccess.ReadOnly).ToString(); ;
            //folder = client.GetFolder(name);


        }
        private void LoadData(ImapClient client)
        {
            foreach (var item in client.GetFolders(client.PersonalNamespaces[0]))
            {
                Folders.Add(item.FullName);
            }
            
        }

        public class MailItem
        {
            public UniqueId Uid { get; set; }
            public string DisplayMsg { get; set; }
            public override string ToString()
            {
                return DisplayMsg;
            }
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailItem selecetedMail = MailsBox.SelectedItem as MailItem;

                string fullMsg = folder.GetMessage(selecetedMail.Uid).ToString();
                var message = folder.GetMessage(selecetedMail.Uid);

                MailMessageWindow mailMessageWindow = new MailMessageWindow();
                mailMessageWindow.MailMessage = fullMsg;
                mailMessageWindow.MailSender = message.From.ToString();
                mailMessageWindow.MailReciever = message.To.ToString();
                mailMessageWindow.MailTheme = message.Subject.ToString();

                var attach = new ObservableCollection<string>();
                foreach (var item in message.Attachments)
                {
                    var i = item.ToString();
                    attach.Add(i);
                }

                mailMessageWindow.MailAttachments = attach;
                

                mailMessageWindow.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
