﻿using System;
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
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using WIA;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Win32;

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
            string fileName = @"D:\test\image.jpg";
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
        public void reloadAccList()
        {
            List<Record> list = db.GetRecord();

            lvRecords.Items.Clear();
            foreach (Record r in list)
            {

                lvRecords.Items.Add(r);
            }


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRecord rec = new AddRecord();
            rec.ShowDialog();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdvancedSearch advSearch = new AdvancedSearch();
            advSearch.Show();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("Are you sure you want to quit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (dr == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                return;
            }
        }


        private void btDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            int items = lvRecords.SelectedItems.Count;
            if (MessageBox.Show("Are you sure yo want to delete " + items + " record(s)", "Delete record", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
            { return; }
            else
            {
                foreach (Record r in lvRecords.SelectedItems)
                {
                    db.deleteInterTag(r.RecordId);
                    db.DeleteRecord(r.RecordId);
                }
                reloadAccList();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lvRecords.Items.Add("test number " + i);
            }
        }



        private void miPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(lvRecords, "List Items Print");
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Scanners scn = new Scanners();
            scn.Show();
          
        }


        


        private string GetWebResponse(string url)
        {

            WebClient web_client = new WebClient();
            Stream response = web_client.OpenRead(url);

            using (StreamReader stream_reader = new StreamReader(response))
            {

                string result = stream_reader.ReadToEnd();
                stream_reader.Close();

                return result;
            }
        }

        private void btnGetPrices_Click(object sender, RoutedEventArgs e)
        {

            string url = "";
            if (txtSymbol1.Text != "") url += txtSymbol1.Text + "+";
            if (txtSymbol2.Text != "") url += txtSymbol2.Text + "+";
            if (txtSymbol3.Text != "") url += txtSymbol3.Text + "+";
            if (txtSymbol4.Text != "") url += txtSymbol4.Text + "+";
            if (url != "")
            {

                url = url.Substring(0, url.Length - 1);


                const string base_url =
                    "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1";
                url = base_url.Replace("@", url);


                try
                {

                    string result = GetWebResponse(url);
                    Console.WriteLine(result.Replace("\\r\\n", "\r\n"));

                    string[] lines = result.Split(
                        new char[] { '\r', '\n' },
                        StringSplitOptions.RemoveEmptyEntries);
                    lbStock1.Content = decimal.Parse(lines[0].Split(',')[1]).ToString("C3");
                    lbStock2.Content = decimal.Parse(lines[1].Split(',')[1]).ToString("C3");
                    lbStock3.Content = decimal.Parse(lines[2].Split(',')[1]).ToString("C3");
                    lbStock4.Content = decimal.Parse(lines[3].Split(',')[1]).ToString("C3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Read Error");
                }
            }





        }

        public string CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
        {
            string Output = "";
            string fromCurrency1 = tbCurrency1.Text;
            string toCurrency1 = tbCurrency2.Text;
            decimal amount1 = Convert.ToDecimal(tbResult.Text);


            const string urlPattern = "http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1";
            string url = string.Format(urlPattern, fromCurrency1, toCurrency1);

            string response = new WebClient().DownloadString(url);


            decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);

            Output = (amount1 * exchangeRate).ToString();

            lbResult.Content = Output;


            return Output;
        }

        private void btCurrency_Click(object sender, RoutedEventArgs e)
        {
            CurrencyConversion(new decimal(123.45), "CAD", "USD");

        }


        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lvRecords.Items);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();

        }

        private void miAbout_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BudgetApp Application" + Environment.NewLine + "Built: October 4, 2017" + Environment.NewLine +
           "Version: 1.02" + Environment.NewLine + "Creators: Giorgio Plescia, Oleksii Redko", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void miSaveas_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)

            {
                using (FileStream S = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                {
                    using (StreamWriter st = new StreamWriter(S))
                    {
                        foreach (Record aa in lvRecords.Items)
                            st.WriteLine(string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", aa.AccountId, aa.AccountStr, aa.CategoryStr, aa.RecordType, aa.TagList, aa.Amount, aa.Date));
                    }
                }
            }
        }
    }

   
}

