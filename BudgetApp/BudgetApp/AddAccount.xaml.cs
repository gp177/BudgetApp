using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        Database db;
        public AddAccount()
        {
            try
            {
                db = new Database();
                InitializeComponent();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("DataBese error" + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btAddAccountSql_Click(object sender, RoutedEventArgs e)
        {
            String name = tbAccountName.Text;
            String type = tbAccountType.Text;
            double balance = Double.Parse(tbBalance.Text);
            int number = int.Parse(tbAccountNumber.Text);
            db.AddAccount(name, type, number, balance);
            this.Close();
            
        }
    }
}
