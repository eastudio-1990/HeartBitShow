using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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
            showColumnChart1();
            showColumnChart2();
        }

        CustomServices customServices = new CustomServices();

        private void showColumnChart1()
        {
             lineChart.DataContext= customServices.ConvertToDigit(@"C: \Users\irantech24.com\Downloads\WpfToolkitChart\toolkit chart sample\WpfToolkitChart\WpfToolkitChart\a.txt");

        }

        
        private void showColumnChart2()
        {
             lineChart2.DataContext= customServices.ConvertToDigit(@"C: \Users\irantech24.com\Downloads\WpfToolkitChart\toolkit chart sample\WpfToolkitChart\WpfToolkitChart\a.txt");

        }

    }
}





