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
            try
            {
                db = new Database();
                InitializeComponent();
                reloadAccList();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("DataBese error" + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public  void reloadAccList()
        {
            List<Record> list = db.GetRecord();
           
            lvRecords.Items.Clear();
            foreach(Record r in list)
            {
                
                lvRecords.Items.Add(r);
            }
            

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRecord rec = new AddRecord();
            rec.Show();
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdvancedSearch advSearch = new AdvancedSearch();
            advSearch.Show();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
