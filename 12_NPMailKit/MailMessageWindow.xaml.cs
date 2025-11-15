using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using static _12_NPMailKit.MailBox;

namespace _12_NPMailKit
{
    /// <summary>
    /// Interaction logic for MailMessageWindow.xaml
    /// </summary>
    public partial class MailMessageWindow : Window
    {
        public string MailMessage { get; set; }
        public string MailSender { get; set; }
        public string MailReciever { get; set; }
        public string MailTheme { get; set; }
        public ObservableCollection<string> MailAttachments { get; set; }
        public MailMessageWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        private void LoadData()
        {
            recieverEmailTxt.Text = MailReciever;
            themeTxt.Text = MailTheme;
            fromEmailTxt.Text = MailSender;
            msgTxt.Text = MailMessage;

        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
