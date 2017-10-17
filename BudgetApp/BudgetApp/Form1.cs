using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graph = System.Windows.Forms.DataVisualization.Charting;

namespace BudgetApp
{
    public partial class Form1 : Form
    {
        Database db;
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(750, 750);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            db = new Database();
            const int MaxX = 20;
            // Create new Graph
            chart = new Graph.Chart();
            chart.Location = new System.Drawing.Point(15, 15);
            chart.Size = new System.Drawing.Size(700, 700);
            
            chart.ChartAreas.Add("draw");

            // x axis
            chart.ChartAreas["draw"].AxisX.Minimum = 0;
            chart.ChartAreas["draw"].AxisX.Maximum = 10;
            chart.ChartAreas["draw"].AxisX.Interval = 1;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.Black;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;
            // y axis
            chart.ChartAreas["draw"].AxisY.Minimum = 0;
            chart.ChartAreas["draw"].AxisY.Maximum = 3;
            chart.ChartAreas["draw"].AxisY.Interval = 1;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.Black;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;

            chart.ChartAreas["draw"].BackColor = Color.White;

           
            chart.Series.Add("Chart");
           

             
            chart.Series["Chart"].ChartType = Graph.SeriesChartType.Line;
           

            // Color the line 
            chart.Series["Chart"].Color = Color.Red;
            chart.Series["Chart"].BorderWidth = 3;

            Record rc = new Record();

            for (rc.Amount = db.GetBalanceById(77); rc.Amount < MaxX; rc.Amount += 0.1)
            {
                chart.Series["Chart"].Points.AddXY(rc.Amount, Math.Sin(rc.Amount) / rc.Amount);
               
            }
            chart.Series["Chart"].LegendText = "Projected Increase";

         //   legend
           chart.Legends.Add("MyLegend");
            chart.Legends["MyLegend"].BorderColor = Color.Red;
            Controls.Add(this.chart);
        }
    }
}
