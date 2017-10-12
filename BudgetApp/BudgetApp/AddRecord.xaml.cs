﻿using System;
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
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        Database db;
        public AddRecord()
        {
            db = new Database();
            InitializeComponent();
            reloadCategoryList();
            reloadAccountList();
            reloadTagsList();
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
        private void reloadTagsList()
        {
            List<String> list = db.GetTegs();
            lbTagList.Items.Clear();
            foreach (String rec in list)
            {
                lbTagList.Items.Add(rec);
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
            // values from user inputs
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
            //db.AddInterTeg(tagid, recId);
            foreach(String item in lbAddTagList.Items)
            {
                int tegId=db.GetTagIdbyDescription(item);
                db.AddInterTeg(tegId, recId);
            }
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWin.reloadAccList();


            //db.AddAmount(Amount);
            //db.AddDate(Date);

        }

        private void tbAddNewTeg_TextChanged(object sender, TextChangedEventArgs e)
        {
            btCreateNewTag.IsEnabled = true;
        }

        private void btCreateNewTag_Click(object sender, RoutedEventArgs e)
        {
            //TODO: check for double ,focus on added element on a list
            String tag = tbAddNewTag.Text;
            db.AddTags(tag);
            reloadTagsList();
            tbAddNewTag.Text = "";
            btCreateNewTag.IsEnabled = false;
        
                     
        }

        private void btAddTegToList_Click(object sender, RoutedEventArgs e)
        {
            String item = (String)lbTagList.SelectedItem;
            if (lbAddTagList.Items.IsEmpty)
                lbAddTagList.Items.Add(item);
            else if (lbAddTagList.Items.IndexOf(item) < 0)
                lbAddTagList.Items.Add(item);
            else
                return;
        }
    }
}
