using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PcadNew
{
    /// <summary>
    /// Логика взаимодействия для akushko_parnyh.xaml
    /// </summary>
    public partial class akushko_parnyh : Window
    {
        public akushko_parnyh()
        {
            InitializeComponent();
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "Demo clicked";
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "Start clicked";
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "Next clicked";
        }
    }
}
