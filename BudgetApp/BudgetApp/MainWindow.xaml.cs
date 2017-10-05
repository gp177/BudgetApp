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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Database db;

        public MainWindow()
        {
            //Database connection
            try {
                db = new Database();
                InitializeComponent();
                     
            }catch(SqlException ex)
            {
                MessageBox.Show("DataBese error" + ex.Message,"ERROR", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
