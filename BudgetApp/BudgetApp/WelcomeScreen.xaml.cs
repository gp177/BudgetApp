using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for WelcomeScreen.xaml
    /// </summary>
    public partial class WelcomeScreen : Window
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            
            String pass = pbPass.Password.ToString();

            if (pass == "admin")
            {
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Password", "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
            }  
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            WelcomeRegister welReg = new WelcomeRegister();
            welReg.Show();
        }
    }
}
