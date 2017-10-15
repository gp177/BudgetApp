﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;


namespace BudgetApp
{
    public partial class Scanners : Form
    {
        Database db;
        Device oDevice;
        Item oItem;
        CommonDialogClass dlg;

        public Scanners()
        {
            InitializeComponent();
           
        }
        public void Scann()
        {
            dlg.ShowAcquisitionWizard(oDevice);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dlg = new CommonDialogClass();
            oDevice = dlg.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);
            try
            {
                Scann();
                button1.Text = "Image scanned";
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(dlg.FileName);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db = new Database();
            System.Drawing.Image scanpic = pictureBox1.Image;
            
           // System.Drawing.Image image;
            System.IO.MemoryStream imageStream;
            byte[] imageBytes;
            
            imageStream = new System.IO.MemoryStream();
            scanpic.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg); 
            imageBytes = imageStream.ToArray();
            //  VARBINARY(MAX)
            db.AddPictures(imageBytes);
        }
    }
}
