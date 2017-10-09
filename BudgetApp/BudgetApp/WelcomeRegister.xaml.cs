using System;
using System.Collections.Generic;
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
using System.Net.Mail;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for WelcomeRegister.xaml
    /// </summary>
    public partial class WelcomeRegister : Window
    {
        public WelcomeRegister()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String fullName = tbFullName.Text;
            String email = tbEmail.Text;
            String occupation = tbOccupation.Text;
            String country = tbCountry.Text;
            String about = tbAbout.Text;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(email);
            mail.To.Add("gp.rci1@gmail.com");
            mail.Subject = "Developer Password Request";
            mail.Body = string.Format("Full Name: {0},  Email: {1},  Occupation: {2},  Country: {3},  About: {4}", fullName, email, occupation, country, about);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("budgetappprogram@gmail.com", "AG1project");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            MessageBox.Show("mail Send");
        }
    }
}
