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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        Database db;

        public AddCategory()
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

        private void btAddCategorySQL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String category = tbAddCategorySQL.Text;

                db.AddCategory(category);
                this.Close();
            } catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
