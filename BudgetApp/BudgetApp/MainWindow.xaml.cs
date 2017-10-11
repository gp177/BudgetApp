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
using WIA;
using System.Net;

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
            Environment.Exit(0);
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
            CommonDialogClass commonDialogClass = new CommonDialogClass();
            Device scannerDevice = commonDialogClass.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, false, false);
            if (scannerDevice != null)
            {
                Item scannnerItem = scannerDevice.Items[1];
                AdjustScannerSettings(scannnerItem, 300, 0, 0, 1010, 620, 0, 0);
                object scanResult = commonDialogClass.ShowTransfer(scannnerItem, WIA.FormatID.wiaFormatPNG, false);
                if (scanResult != null)
                {
                    ImageFile image = (ImageFile)scanResult;
                    string fileName = System.IO.Path.GetTempPath() + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fffffff") + ".png";
                    SaveImageToPNGFile(image, fileName);
                   // pictureBoxScannedImage.ImageLocation = fileName;
                    
                }
            }
        }

        private static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel,
           int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents)
        {
            const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
            const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
            const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
            const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
            const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
            const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
            const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
            const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
        }

        private static void SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            Property prop = properties.get_Item(ref propName);
            prop.set_Value(ref propValue);
        }

        private static void SaveImageToPNGFile(ImageFile image, string fileName)
        {
            ImageProcess imgProcess = new ImageProcess();
            object convertFilter = "Convert";
            string convertFilterID = imgProcess.FilterInfos.get_Item(ref convertFilter).FilterID;
            imgProcess.Filters.Add(convertFilterID, 0);
            SetWIAProperty(imgProcess.Filters[imgProcess.Filters.Count].Properties, "FormatID", WIA.FormatID.wiaFormatPNG);
            image = imgProcess.Apply(image);
            image.SaveFile(fileName);

<<<<<<< HEAD
=======
        }

        // Get a web response.
        private string GetWebResponse(string url)
        {
            // Make a WebClient.
            WebClient web_client = new WebClient();

            // Get the indicated URL.
            Stream response = web_client.OpenRead(url);

            // Read the result.
            using (StreamReader stream_reader = new StreamReader(response))
            {
                // Get the results.
                string result = stream_reader.ReadToEnd();

                // Close the stream reader and its underlying stream.
                stream_reader.Close();

                // Return the result.
                return result;
            }
        }

        private void btnGetPrices_Click(object sender, RoutedEventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //Application.DoEvents();

            // Build the URL.
            string url = "";
            if (txtSymbol1.Text != "") url += txtSymbol1.Text + "+";
            if (txtSymbol2.Text != "") url += txtSymbol2.Text + "+";
            if (txtSymbol3.Text != "") url += txtSymbol3.Text + "+";
            if (txtSymbol4.Text != "") url += txtSymbol4.Text + "+";
            if (url != "")
            {
                // Remove the trailing plus sign.
                url = url.Substring(0, url.Length - 1);

                // Prepend the base URL.
                const string base_url =
                    "http://download.finance.yahoo.com/d/quotes.csv?s=@&f=sl1d1t1c1";
                url = base_url.Replace("@", url);

                // Get the response.
                try
                {
                    // Get the web response.
                    string result = GetWebResponse(url);
                    Console.WriteLine(result.Replace("\\r\\n", "\r\n"));

                    // Pull out the current prices.
                    string[] lines = result.Split(
                        new char[] { '\r', '\n' },
                        StringSplitOptions.RemoveEmptyEntries);
                    txtPrice1.Text = decimal.Parse(lines[0].Split(',')[1]).ToString("C3");
                    txtPrice2.Text = decimal.Parse(lines[1].Split(',')[1]).ToString("C3");
                    txtPrice3.Text = decimal.Parse(lines[2].Split(',')[1]).ToString("C3");
                    txtPrice4.Text = decimal.Parse(lines[3].Split(',')[1]).ToString("C3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Read Error");
                }
            }

            //this.Cursor = Cursors.Default;
>>>>>>> eca875535aedae72f7ca9dd9054f5c971aa94117
        }
    }
}

