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
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(750, 750);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const int MaxX = 20;
            // Create new Graph
            chart = new Graph.Chart();
            chart.Location = new System.Drawing.Point(10, 10);
            chart.Size = new System.Drawing.Size(700, 700);
            
            chart.ChartAreas.Add("draw");

            // x axis
            chart.ChartAreas["draw"].AxisX.Minimum = 0;
            chart.ChartAreas["draw"].AxisX.Maximum = MaxX;
            chart.ChartAreas["draw"].AxisX.Interval = 1;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.Black;
            chart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;
            // y axis
            chart.ChartAreas["draw"].AxisY.Minimum = 0;
            chart.ChartAreas["draw"].AxisY.Maximum = 1;
            chart.ChartAreas["draw"].AxisY.Interval = 0.2;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.Black;
            chart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Dash;

            chart.ChartAreas["draw"].BackColor = Color.White;

           
            chart.Series.Add("MyFunc");

             
            chart.Series["MyFunc"].ChartType = Graph.SeriesChartType.Line;

            // Color the line 
            chart.Series["MyFunc"].Color = Color.Red;
            chart.Series["MyFunc"].BorderWidth = 3;

         
            for (double x = 0.1; x < MaxX; x += 0.1)
            {
                chart.Series["MyFunc"].Points.AddXY(x, Math.Sin(x) / x);
            }
            chart.Series["MyFunc"].LegendText = "Projected Amount";

         //   legend
           chart.Legends.Add("MyLegend");
            chart.Legends["MyLegend"].BorderColor = Color.Tomato;
            Controls.Add(this.chart);
        }
    }
}
