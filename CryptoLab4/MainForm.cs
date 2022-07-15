using CryptoLab4.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoLab4
{

    public partial class MainForm : Form
    {
        private Chart chart;

        public MainForm()
        {
            InitializeComponent();
            InitChart();
        }

        private void getHashButton_Click(object sender, EventArgs e)
        {
            MD4 md4 = new ();
            outputRichTextBox.Clear();
            outputRichTextBox.Text += "MD4: " + md4.GetHexHashFromString(inputRichTextBox.Text);
        }

        private void InitChart()
        {
            ChartArea chartArea1 = new ();
            Legend legend1 = new ();
            Series series1 = new ();
            this.chart = new ();
            ((ISupportInitialize)(this.chart)).BeginInit();
            this.panel1.Controls.Add(this.chart);


            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Location = new System.Drawing.Point(3, 15);
            this.chart.Name = "chart1";
            this.chart.Legends.Add(legend1);
            series1.ChartArea = "ChartArea1";
            series1.ChartType = SeriesChartType.Line;
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new Size(700, 500);
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
                        Series series = chart.Series.Add("Avalanche one round");
                        series.ChartType = SeriesChartType.Line;
                        Series series1 = chart.Series.Add("Avalanche tho rounds");
                        series1.ChartType = SeriesChartType.Line;
                        Series series2 = chart.Series.Add("Avalanche three rounds");
                        series2.ChartType = SeriesChartType.Line;

                        series.ChartType = SeriesChartType.Line;

                        (int[], int[]) oneRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest(false, false);
                        (int[], int[]) twoRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest(true, false);
                        (int[], int[]) threeRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest();


                        for (int i = 0; i < oneRoundAvalancheEffectResult.Item1.Length; i++)
                        {
                            series.Points.AddXY(oneRoundAvalancheEffectResult.Item1[i], oneRoundAvalancheEffectResult.Item2[i]);
                            series1.Points.AddXY(twoRoundAvalancheEffectResult.Item1[i], twoRoundAvalancheEffectResult.Item2[i]);
                            series2.Points.AddXY(threeRoundAvalancheEffectResult.Item1[i], threeRoundAvalancheEffectResult.Item2[i]);
                        }
                        break;
                    }
                case 1:
                    {
                        Series series = chart.Series.Add("Avalanche");
                        series.ChartType = SeriesChartType.Line;
                        Series series1 = chart.Series.Add("Avalanche random");
                        series1.ChartType = SeriesChartType.Line;

                        (int[], int[]) defaultConstantsAvalancheEffectResult = MD4Tester.AvalancheEffectTest();
                        (int[], int[]) randomConstantsAvalancheEffectResult = MD4Tester.AvalancheEffectTest(true, true,
                            0xaaaaaaaa, 0xaaaaaaaa, 0xaaaaaaaa, 0xaaaaaaaa);

                        for (int i = 0; i < defaultConstantsAvalancheEffectResult.Item1.Length; i++)
                        {
                            series.Points.AddXY(defaultConstantsAvalancheEffectResult.Item1[i], defaultConstantsAvalancheEffectResult.Item2[i]);
                            series1.Points.AddXY(randomConstantsAvalancheEffectResult.Item1[i], randomConstantsAvalancheEffectResult.Item2[i]);
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

                        Dictionary<int, double> result = Average(N);

                        foreach (var i in result)
                        {
                            series.Points.AddXY(i.Key, i.Value);
                        }

                        break;
                    }
                case 3:
                    {
                        Series series = chart.Series.Add("Prototype");
                        series.ChartType = SeriesChartType.Line;
                        string prototype = "random text";
                        (int[] k, string[] prototypes, int[] N) = MD4Tester.PrototypesTest(prototype);

                        Dictionary<int, double> result = Average(N);

                        foreach (var i in result)
                        {
                            series.Points.AddXY(i.Key, i.Value);
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
            outputRichTextBox.Clear();
            outputRichTextBox.Text += $"------------------\ncollision\n------------------\n{collision.Item1}\n\n{collision.Item2}\n\n" +
                $"------------------\nhash\n------------------\n{hash}\n\ngenerated strings: {numGeneratedStrings}";
        }

        private void findPrototypeButton_Click(object sender, EventArgs e)
        {
            outputRichTextBox.Clear();
            (string prototype, string hash, int numGeneratedStrings)
                = MD4Tester.FindPrototype(inputRichTextBox.Text, (int)hashLengthNumericUpDown.Value);
            outputRichTextBox.Text += $"\n\n------------------\nprototype\n------------------\n{prototype}\n\n" +
                $"------------------\nhash\n------------------\n{hash}\n\ngenerated strings: {numGeneratedStrings}";
        }

        private Dictionary<int, double> Average(int[] source, int numSamplesToAvg = 4)
        {
            Dictionary<int, double> result = new Dictionary<int, double>();
            int mid = 0;
            for (int k = 0; k < source.Length; k++)
            {
                mid += source[k];
                if (k % numSamplesToAvg == 0)
                {
                    result[k + numSamplesToAvg] = mid / numSamplesToAvg;
                    mid = 0;
                }
            }

            return result;
        }
    }
}
