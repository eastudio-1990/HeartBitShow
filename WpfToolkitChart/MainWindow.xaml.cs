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
      showColumnChart();
    }

    public const double Resolution = 4.0 * 1000 / 6 / 32767;
    public void showColumnChart()
    {

            List<int> y = new List<int>();
            string data = File.ReadAllText(@"C:\Users\irantech24.com\source\repos\DGDena_Charts_PlayGround\ConsoleApp1\ConsoleApp2\file\a.txt");
            
            String[] spearator = { "Data:\r\n" };

            String[] hexValuesSplit = data.Split(spearator,
                   StringSplitOptions.RemoveEmptyEntries);

            string  arr =hexValuesSplit.Skip(1).First();
            
            List<string> newArr = new List<string>();
            int p = 0;
            string text = null;
            for (int q = 0; q < arr.Length; q++)
            {
                if (arr[q]=='\n')
                {
                    text = arr.Substring(p,q-p-1);
                    newArr.Add(text);
                    p=q+1;
                }
            }

            foreach (var line in newArr)
            {

                byte[] x = new byte[64];
                var i = 0;
                
                foreach (string hex in line.Split(new char[] { ' ' }))
                {
                    x[i++] = Convert.ToByte(hex, 16);
                }

                byte[] a = new byte[2];
                for (int c = 12; c < x.Length; c += 19)
                {
                    a[0] = x[c + 1];
                    a[1] = x[c];
                    var g = BitConverter.ToInt16(a, 0);
                    y.Add(g);
                }

            }

          


            List<double> _chart = new List<double>();

            foreach (var item in y)
            {
                var t= item* Resolution;
                
                _chart.Add(t);
                
            }
            
            //Setting data for line chart
            lineChart.DataContext = _chart;
    }

  }
}
