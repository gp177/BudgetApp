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

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for AdvancedSearch.xaml
    /// </summary>
    public partial class AdvancedSearch : Window
    {
        Database db;

      
        public AdvancedSearch()
        {
            db = new Database();
            InitializeComponent();
            reloadAccountList();
            reloadCategoryList();
        }

        private void reloadAccountList()
        {
            List<Record> list = db.GetAccounts();
            cbAccount.Items.Clear();
            foreach (Record rec in list)
            {
                cbAccount.Items.Add(rec.AccountName);
            }
        }
        private void reloadCategoryList()
        {
            List<String> list = db.GetCategories();
            cbCategory.Items.Clear();
            foreach (String rec in list)
            {
                cbCategory.Items.Add(list);

            }
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
