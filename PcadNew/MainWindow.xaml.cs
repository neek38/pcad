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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PcadNew
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Call_Method_Shaffera(object sender, RoutedEventArgs e)
        {
            WindowShaffera windowShaffera = new WindowShaffera();
            windowShaffera.Show();
            windowShaffera.WindowShaffera_Closed += ShafferaClosed;
            Window1.IsEnabled = false;
        }
        private void ShafferaClosed(object sender, EventArgs e)
        {
            Window1.IsEnabled = true;
        }
    }
}
