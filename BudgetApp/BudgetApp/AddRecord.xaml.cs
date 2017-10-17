using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        Database db;
        String Filepat = "";
        public AddRecord()
        {
            db = new Database();
            InitializeComponent();
            reloadCategoryList();
            reloadAccountList();
            //reloadTagsView();
        }

        private void reloadCategoryList()
        {
            List<String> list = db.GetCategories();
            cbCategory.Items.Clear();
            foreach (String rec in list)
            {
                cbCategory.Items.Add(rec);
               
            }
        }

        private void reloadAccountList()
        {
            List<Record> list = db.GetAccounts();
            cbAccount.Items.Clear();
            foreach(Record rec in list)
            {
                cbAccount.Items.Add(rec.AccountName);
            }
        }
        private void reloadTagsView()
        {
            List<String> list = db.GetTags();
            lbTagsView.Items.Clear();
            foreach (String rec in list)
            {
                lbTagsView.Items.Add(rec);
            }
        }



        private void btAddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccount acc = new AddAccount();
            acc.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCategory cat = new AddCategory();
            cat.Show();
        }

        private void btAddRecord_Click(object sender, RoutedEventArgs e)
        {
            
            Record r = new Record();
            double AccUpdate = 0;
            // values from user inputsz
            String Account = cbAccount.Text;
            String Category = cbCategory.Text;
            String RecType = (rbSpending.IsChecked == true ? "Spending" : (rbIncome.IsChecked == true ? "Income" : ""));
           
            int AccId = db.GetAccountID(Account);
            int CatId = db.GetCategoryID(Category);
            int Amount = int.Parse(tbBalance.Text);
            DateTime Date = DateTime.Parse(DatePick.Text);
           
            r.Date = Date;
            r.Amount = Amount;
            r.AccountId = AccId;
            r.CategoryId = CatId;
            r.RecordType = RecType;
            int recId = db.AddRecord(r);
            //account balance change DOESNT WORK!!!!!!!!!!!!!!!
            if (RecType.Equals("Spending")) {
                db.UpdateBalance(AccId, (Double)db.GetBalanceById(AccId) - (Double)Amount);
             }else
               db.UpdateBalance(AccId, db.GetBalanceById(AccId) + Amount);
            //END:account balance change


            foreach (String item in lbTagsView.Items)
            {
                int tegId = db.GetTagIdbyDescription(item);
                db.AddInterTeg(tegId, recId);
            }
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWin.reloadAccList();

        }

        private void btCloselAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWin.ShowAccountCart();
        }

        private void btbtAddEditTags_Click(object sender, RoutedEventArgs e)
        {
            AddTags t = new AddTags();
            t.ShowDialog();
        }

       
        private void btSaveTag_Click(object sender, RoutedEventArgs e)
        {
            Record r = new Record();
            // values from user inputsz
            String Account = cbAccount.Text;
            String Category = cbCategory.Text;
            String RecType = (rbSpending.IsChecked == true ? "Spending" : (rbIncome.IsChecked == true ? "Income" : ""));

            int RecdId = Convert.ToInt32(tbRecordId.Text);
            int AccId = db.GetAccountID(Account);
            int CatId = db.GetCategoryID(Category);
            int Amount = int.Parse(tbBalance.Text);
            DateTime Date = DateTime.Parse(DatePick.Text);

            r.RecordId = RecdId;
            r.Date = Date;
            r.Amount = Amount;
            r.AccountId = AccId;
            r.CategoryId = CatId;
            r.RecordType = RecType;
            db.UpdateRecord(r);
            db.deleteInterTag(RecdId);
            foreach (String item in lbTagsView.Items)
            {
                int tegId = db.GetTagIdbyDescription(item);
                db.AddInterTeg(tegId, RecdId);
            }

            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWin.reloadAccList();
            this.Close();

        }

      
            
        
    }
}
