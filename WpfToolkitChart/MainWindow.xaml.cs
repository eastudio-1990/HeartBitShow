using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfToolkitChart
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      ShowCharts();
    }

    public const double Resolution = 4.0 * 1000 / 6 / 32767;
        private void ShowCharts()
        {
            string data = File.ReadAllText(@"D:\DgDena\WpfToolkitChart\WpfToolkitChart\a.txt");
            String[] spearator = { "Data:\r\n" };
            String[] hexValuesSplit = data.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            string arr = hexValuesSplit.Skip(1).First();

            List<string> newArr = new List<string>();
            int p = 0;
            string text;

            for (int q = 0; q < arr.Length; q++)
            {
                if (arr[q] == '\n')
                {
                    text = arr.Substring(p, q - p - 1);
                    newArr.Add(text);
                    p = q + 1;
                }
            }

            int numSamples = newArr.Count * 3;

            List<int> L1 = new List<int>(numSamples);
            List<int> L2 = new List<int>(numSamples);
            List<int> L3 = new List<int>(numSamples);
            List<int> L4 = new List<int>(numSamples);
            List<int> L5 = new List<int>(numSamples);
            List<int> L6 = new List<int>(numSamples);

            ///L1   
            foreach (var line in newArr)
            {
                byte[] x = new byte[64];
                var i = 0;
                short g;

                foreach (string hex in line.Split(new char[] { ' ' }))
                {
                    x[i++] = Convert.ToByte(hex, 16);
                }

                byte[] a = new byte[2];

                // L1
                for (int c = 10; c < x.Length; c += 19)
                {
                    a[0] = x[c + 1];
                    a[1] = x[c];
                    g = BitConverter.ToInt16(a, 0);
                    L1.Add(g);
                }

                // L2
                for (int c = 12; c < x.Length; c += 19)
                {
                    a[0] = x[c + 1];
                    a[1] = x[c];
                    g = BitConverter.ToInt16(a, 0);
                    L2.Add(g);
                }
            }

            ///L3 = L2 - L1      
            // L4 = - (L1 + L2) / 2            
            ///L5 = L1 - (L2 / 2)      
            ///L6 = L2 - (L1 / 2) 
            for (int i = 0; i < L1.Count; i++)
            {
                L3.Add(L2[i] - L1[i]);
                L4.Add(-(L1[i] - L2[i]) / 2);
                L5.Add(L1[i] - L2[i] / 2);
                L6.Add(L2[i] - (L1[i] / 2));
            }

            List<KeyValuePair<int, double>> convertedList1 = new List<KeyValuePair<int, double>>(numSamples);
            List<KeyValuePair<int, double>> convertedList2 = new List<KeyValuePair<int, double>>(numSamples);
            List<KeyValuePair<int, double>> convertedList3 = new List<KeyValuePair<int, double>>(numSamples);
            List<KeyValuePair<int, double>> convertedList4 = new List<KeyValuePair<int, double>>(numSamples);
            List<KeyValuePair<int, double>> convertedList5 = new List<KeyValuePair<int, double>>(numSamples);
            List<KeyValuePair<int, double>> convertedList6 = new List<KeyValuePair<int, double>>(numSamples);

            int Xaxis1 = 0;

            for (int i = 0; i < L1.Count; i++)
            {
                Xaxis1 += 1;
                convertedList1.Add(new KeyValuePair<int, double>(Xaxis1, L1[i] * Resolution));
                convertedList2.Add(new KeyValuePair<int, double>(Xaxis1, L2[i] * Resolution));
                convertedList3.Add(new KeyValuePair<int, double>(Xaxis1, L3[i] * Resolution));
                convertedList4.Add(new KeyValuePair<int, double>(Xaxis1, L4[i] * Resolution));
                convertedList5.Add(new KeyValuePair<int, double>(Xaxis1, L5[i] * Resolution));
                convertedList6.Add(new KeyValuePair<int, double>(Xaxis1, L6[i] * Resolution));
            }

            lineChartL1.DataContext = convertedList1;
            lineChartL2.DataContext = convertedList2;
            lineChartL3.DataContext = convertedList3;
            lineChartL4.DataContext = convertedList4;
           // lineChartL5.DataContext = convertedList5;
            //lineChartL6.DataContext = convertedList6;

        }



    }

}

