using System;
using System.IO;
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
    /// Логика взаимодействия для ReadMeShaffera.xaml
    /// </summary>
    public partial class ReadMeShaffera : Window
    {
        public ReadMeShaffera()
        {
            InitializeComponent();
        }

        private void ReadMeShaffera_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader("ReadMeShaffera.txt", System.Text.Encoding.Default);
            TB.Text = sr.ReadToEnd();
            sr.Close();
        }
    }
}
