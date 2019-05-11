using System;
using System.Collections.Generic;
using System.Data;
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

namespace PcadNew
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int step { get; set; }
        public List<Wave> bufList = new List<Wave>();
        public DataTable myDT { get; set; }
        public MainWindow()
        {
            myDT = new DataTable();
            InitializeComponent();
        }

        private void Parnyh_Click(object sender, RoutedEventArgs e)
        {
            akushko_parnyh parnyhWindow = new akushko_parnyh();
            parnyhWindow.ShowDialog();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            razm_window razmwindow = new razm_window();
            razmwindow.Show();
        }
        private void auto_Click(object sender, RoutedEventArgs e)
        {
            Tracing t = new Tracing(true, myDT, 1, ref bufList);   
        }
        private void step_Click(object sender, RoutedEventArgs e)
        {
            step++;
            Tracing t = new Tracing(false, myDT,step,ref bufList);
        }
        private void start_demo_mode(object sender, RoutedEventArgs e)
        {
            if (stepBtn.Visibility == Visibility.Hidden)
            {
                stepBtn.Visibility = Visibility.Visible;
            }
            if (autoBtn.Visibility == Visibility.Hidden)
            {
                autoBtn.Visibility = Visibility.Visible;
            }
            if (matr.Visibility == Visibility.Hidden)
            {
                matr.Visibility = Visibility.Visible;
            }
            step = 0;
            myDT = new DataTable();
            for (int m = 0; m < 8; m++)
            {
                myDT.Columns.Add((m + 1).ToString());
            }
            myDT.Rows.Add("", "", "", "", "", "*", "", "");
            myDT.Rows.Add("", "", "", "", "", "*", "*", "*");
            myDT.Rows.Add("", "", "", "", "", "B", "", "");
            myDT.Rows.Add("", "*", "*", "*", "", "*", "", "*");
            myDT.Rows.Add("A", "", "", "", "", "", "", "*");
            myDT.Rows.Add("", "", "", "*", "*", "*", "*", "*");
            myDT.Rows.Add("", "", "", "", "", "", "", "");
            myDT.Rows.Add("", "*", "*", "*", "*", "*", "*", "");
            matr.ItemsSource = myDT.DefaultView;
        }
    }
}
