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
    /// Логика взаимодействия для help.xaml
    /// </summary>
    public partial class help : Window
    {
        public help()
        {
            InitializeComponent();
            tb.Text = "\tМетод парных перестановк используется для оптимизации размещения после компоновки или последовательного размещения. Заключается в просчете ΔL для ближайших пар по горизонтали и вертикали. Входными данными служит результат работы модуля \"Компоновка\" или \"Последовательное размещение\".\n\n\tПорядок работы с программой:\n1. Выбрать из выпадающего списка необходимые данные и нажать кнопку \"Загрузить данные\".\n2. Выбрать пошаговое либо автоматическое выполнение.\n3. После просчета первого узла перейти к просчету второго узла, повторять шаги 1 и 2. Сохранение результата происходит после просчета всех узлов.";
        }
    }
}
