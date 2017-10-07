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
        }

        private void reloadCategoryList()
        {
            List<Record> list = db.GetCategories();
            cbCategory.Items.Clear();
            foreach (Record rec in list)
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
                cbAccount.Items.Add(rec);
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
    }
}
