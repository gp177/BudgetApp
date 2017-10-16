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
    /// Interaction logic for AddTags.xaml
    /// </summary>
    public partial class AddTags : Window
    {
        Database db;
        public AddTags()
        {
            db = new Database();
            InitializeComponent();
            reloadTagsList();
            loadAddTagList();
        }
        private void tbAddNewTeg_TextChanged(object sender, TextChangedEventArgs e)
        {
            btCreateNewTag.IsEnabled = true;
        }

        private void btCreateNewTag_Click(object sender, RoutedEventArgs e)
        {
            //TODO: check for double ,focus on added element on a list
            try
            {
                String tag = tbAddNewTag.Text;
                db.AddTags(tag);
                reloadTagsList();
                tbAddNewTag.Text = String.Empty;
                btCreateNewTag.IsEnabled = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Tag with this Description alredy exist \n", "Insert error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void reloadTagsList()
        {
            List<String> list = db.GetTags();
            lbTagList.Items.Clear();
            foreach (String rec in list)
            {
                lbTagList.Items.Add(rec);
            }
        }
        private void loadAddTagList()
        {
            var AddrecWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is AddRecord) as AddRecord;
            
            foreach (var itm in AddrecWindow.lbTagsView.Items)
            {
                lbAddTagList.Items.Add(itm);
            }

        }

        private void btAddTegToList_Click(object sender, RoutedEventArgs e)
        {
            String item = (String)lbTagList.SelectedItem;
            if (lbAddTagList.Items.IsEmpty)
                lbAddTagList.Items.Add(item);
            else if (lbAddTagList.Items.IndexOf(item) < 0)
                lbAddTagList.Items.Add(item);
            else
                MessageBox.Show("The record has already been added", "Adding mistake", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var AddrecWindowWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is AddRecord) as AddRecord;
            AddrecWindowWin.lbTagsView.Items.Clear();
           foreach(string item in lbAddTagList.Items)
            {
                AddrecWindowWin.lbTagsView.Items.Add(item);
            }
        }
    }
}
