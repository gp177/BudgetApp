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
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

      

        private StringReader myReader;

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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                lvRecords.Items.Add("test number " + i);
            }
        }



                private void miPrint_Click(object sender, RoutedEventArgs e)
        {
            printDialog1.Document = printDocument1;
                 string strText = "";
                 foreach (object x in lvRecords.Items)
                     {
                         strText = strText + x.ToString() + "\n";
                     }
            
                    myReader = new StringReader(strText);
                 if (printDialog1.ShowDialog() == DialogResult.OK)
                     {
                         this.printDocument1.Print();
                     }


        }
       

        protected void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

          
            float linesPerPage = 0;
            float yPosition = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            Font printFont = this.lvRecords.Items.Font;
            string line = null;

           
            SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Black);


            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            //  using the StringReader, printing each line.
            while (count < linesPerPage && ((line = myReader.ReadLine()) != null))
            {
                // calculate the next line position 
                yPosition = topMargin + (count * printFont.GetHeight(e.Graphics));

                // draw the next line in the rich edit control

                e.Graphics.DrawString(line, printFont,
                                       myBrush, leftMargin,
                                       yPosition, new StringFormat());
                count++;
            }

            if (line != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

            myBrush.Dispose();
        }

       
    }
}
