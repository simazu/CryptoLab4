using CryptoLab4.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Windows.Forms.DataVisualization.Charting;
using Syncfusion;

namespace CryptoLab4
{

    public partial class MainForm : Form
    {
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;

        public MainForm()
        {
            InitializeComponent();
            InitChart();
        }

        private void getHashButton_Click(object sender, EventArgs e)
        {
            string message = ioRichTextBox.Text;
            MD4 md4 = new MD4();
            ioRichTextBox.Text += "\n\nMD4: " + md4.GetHexHashFromString(message);
        }

        private void InitChart()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.panel1.Controls.Add(this.chart);


            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Location = new System.Drawing.Point(3, 15);
            this.chart.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(310, 172);
            this.chart.TabIndex = 3;
            this.chart.Text = "chart1";
        }

        private void plotButton_Click(object sender, EventArgs e)
        {
            chart.Series.Clear();

            switch (chartComboBox.SelectedIndex)
            {
                case 0:
                    {
                        Series series = chart.Series.Add("Avalance");
                        series.ChartType = SeriesChartType.Line;

                        (int[], int[]) oneRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest(false, false);
                        (int[], int[]) twoRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest(true, false);
                        (int[], int[]) threeRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest();

                        for (int i = 0; i < oneRoundAvalancheEffectResult.Item1.Length; i++)
                        {
                            series.Points.AddXY(oneRoundAvalancheEffectResult.Item1[i], oneRoundAvalancheEffectResult.Item2[i]);
                        }
                        break;
                    }
                case 1:
                    {
                        Series series = chart.Series.Add("Avalance");
                        series.ChartType = SeriesChartType.Line;

                        (int[], int[]) defaultConstantsAvalancheEffectResult = MD4Tester.AvalancheEffectTest();
                        (int[], int[]) randomConstantsAvalancheEffectResult = MD4Tester.AvalancheEffectTest(true, true,
                            0xaaaaaaaa, 0xaaaaaaaa, 0xaaaaaaaa, 0xaaaaaaaa);

                        for (int i = 0; i < defaultConstantsAvalancheEffectResult.Item1.Length; i++)
                        {
                            series.Points.AddXY(defaultConstantsAvalancheEffectResult.Item1[i], defaultConstantsAvalancheEffectResult.Item2[i]);
                        }
                        break;
                    }
                case 2:
                    {
                        Series series = chart.Series.Add("Collision");
                        series.ChartType = SeriesChartType.Line;

                        int messageLengthInBits = 80;
                        (int[] k, (string, string)[] collisions, string[] collisionHashes, int[] N)
                            = MD4Tester.CollisionsTest(messageLengthInBits);

                        for (int i = 0; i < k.Length; i++)
                        {
                            series.Points.AddXY(k[i], N[i]);
                        }

                        break;
                    }
                case 3:
                    {
                        Series series = chart.Series.Add("Prototype");
                        series.ChartType = SeriesChartType.Line;
                        string prototype = "random text";
                        (int[] k, string[] prototypes, int[] N) = MD4Tester.PrototypesTest(prototype);

                        for (int i = 0; i < 10; i++)
                        {
                            series.Points.AddXY(k[i], N[i]);
                        }

                        break;
                    }
                case 4:
                    {
                        Series series = chart.Series.Add("Time");
                        series.ChartType = SeriesChartType.Line;

                        (int[] lengths, int[] hashingTimes) = MD4Tester.HashingTimeFromMessageLength();

                        for (int i = 0; i < lengths.Length; i++)
                        {
                            series.Points.AddXY(lengths[i], hashingTimes[i]);
                        }

                        break;
                    }
            }
        }

        private void findCollisionButton_Click(object sender, EventArgs e)
        {
            ((string, string) collision, string hash, int numGeneratedStrings)
                = MD4Tester.FindCollision((int)messageLengthNumericUpDown.Value, (int)hashLengthNumericUpDown.Value);
            ioRichTextBox.Clear();
            ioRichTextBox.Text += $"------------------\ncollision\n------------------\n{collision.Item1}\n\n{collision.Item2}\n\n" +
                $"------------------\nhash\n------------------\n{hash}\n\ngenerated strings: {numGeneratedStrings}";
        }

        private void findPrototypeButton_Click(object sender, EventArgs e)
        {
            (string prototype, string hash, int numGeneratedStrings)
                = MD4Tester.FindPrototype(ioRichTextBox.Text, (int)hashLengthNumericUpDown.Value);
            ioRichTextBox.Text += $"\n\n------------------\nprototype\n------------------\n{prototype}\n\n" +
                $"------------------\nhash\n------------------\n{hash}\n\ngenerated strings: {numGeneratedStrings}";
        }
    }
}
