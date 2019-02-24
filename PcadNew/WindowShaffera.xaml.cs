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
    /// Логика взаимодействия для WindowShaffera.xaml
    /// </summary>
    public partial class WindowShaffera : Window
    {
        public WindowShaffera()
        {
            InitializeComponent();
        }
        int[,] R;
        int[] pos;
        private void Demo_Start(object sender, RoutedEventArgs e)
        {
            R = new int[9, 9]        { { 0, 0, 0, 0, 0, 0, 0, 0, 0 },  //матрица расстояний размерностью на один больше,что бы счёт вёлся с единицы.
                                       { 0, 0, 0, 0, 2, 0, 1, 2, 0 },
                                       { 0, 0, 0, 1, 0, 2, 0, 0, 1 },
                                       { 0, 0, 1, 0, 0, 1, 1, 2, 0 },
                                       { 0, 2, 0, 0, 0, 2, 0, 1, 1 },
                                       { 0, 0, 2, 1, 2, 0, 1, 2, 0 },
                                       { 0, 1, 0, 1, 0, 1, 0, 0, 2 },
                                       { 0, 2, 0, 2, 1, 2, 0, 0, 0 },
                                       { 0, 0, 1, 0, 1, 0, 2, 0, 0 }};
            int i = R[1, 2];
            pos = new int[17] { 0, 2, 4, 1, 3, 0, 6, 7, 0, 8, 0, 0, 5, 0, 0, 0, 0 }; //массив позиций на одну больше,что бы счёт вёлся с единицы.
            //int row = R.GetLength(0);
            //int col = R.GetLength(1);
            //----------------------------------Обнуление ДРП---------------------------------------------------------------------------------
            P1.Visibility = P2.Visibility = P3.Visibility = P4.Visibility = P5.Visibility = P6.Visibility = P7.Visibility = 
            P8.Visibility = P9.Visibility = P10.Visibility = P11.Visibility = P12.Visibility = P13.Visibility = P14.Visibility = 
            P15.Visibility = P16.Visibility = Visibility.Hidden;
            TB1.Text = TB2.Text = TB3.Text = TB4.Text = TB5.Text = TB6.Text = TB7.Text = TB8.Text =
            TB9.Text = TB10.Text = TB11.Text = TB12.Text = TB13.Text = TB14.Text = TB15.Text = TB16.Text = "";
            //----------------------------------Заполнение ДРП---------------------------------------------------------------------------------
            if (pos[1] != 0) { P1.Visibility = Visibility.Visible; TB1.Text = "D" + pos[1].ToString();}
            if (pos[2] != 0) { P2.Visibility = Visibility.Visible; TB2.Text = "D" + pos[2].ToString();}
            if (pos[3] != 0) { P3.Visibility = Visibility.Visible; TB3.Text = "D" + pos[3].ToString();}
            if (pos[4] != 0) { P4.Visibility = Visibility.Visible; TB4.Text = "D" + pos[4].ToString();}
            if (pos[5] != 0) { P5.Visibility = Visibility.Visible; TB5.Text = "D" + pos[5].ToString();}
            if (pos[6] != 0) { P6.Visibility = Visibility.Visible; TB6.Text = "D" + pos[6].ToString();}
            if (pos[7] != 0) { P7.Visibility = Visibility.Visible; TB7.Text = "D" + pos[7].ToString();}
            if (pos[8] != 0) { P8.Visibility = Visibility.Visible; TB8.Text = "D" + pos[8].ToString();}
            if (pos[9] != 0) { P9.Visibility = Visibility.Visible; TB9.Text = "D" + pos[9].ToString();}
            if (pos[10] != 0) { P10.Visibility = Visibility.Visible; TB10.Text = "D" + pos[10].ToString();}
            if (pos[11] != 0) { P11.Visibility = Visibility.Visible; TB11.Text = "D" + pos[11].ToString();}
            if (pos[12] != 0) { P12.Visibility = Visibility.Visible; TB12.Text = "D" + pos[12].ToString();}
            if (pos[13] != 0) { P13.Visibility = Visibility.Visible; TB13.Text = "D" + pos[13].ToString();}
            if (pos[14] != 0) { P14.Visibility = Visibility.Visible; TB14.Text = "D" + pos[14].ToString();}
            if (pos[15] != 0) { P15.Visibility = Visibility.Visible; TB15.Text = "D" + pos[15].ToString();}
            if (pos[16] != 0) { P16.Visibility = Visibility.Visible; TB16.Text = "D" + pos[16].ToString();}
            Step.IsEnabled = true;
            DoAll.IsEnabled = true;
        }

        private void Demo_do_All(object sender, RoutedEventArgs e)
        {
            if (pos[9] != 0 || pos[10] !=0 || pos[11] !=0 || pos[12] !=0 || pos[13] !=0 || pos[14] !=0 || pos[15] !=0 || pos[16] != 0)  //если ложь,значит столбцов меньше трёх
            {
                //----------------------Считаем связи между столбцами------------------------------------------------   
                int rCol12 = 0, rCol13 = 0, rCol14 = 0, rCol23 = 0, rCol24 = 0, rCol34 = 0;
                int L12 = 1, L23 = 0, L34 = 0, buf; Visibility bufVis; string bufstr;
                while (L12 > 0 || L23 > 0 || L34 > 0)   //делать до тех пор,пока есть хотя бы одна положительная,то есть если на предыдущем шаге мы что-то меняли
                {
                    for (int i = 1; i < 5; i++)
                        for (int j = 1; j < 5; j++)
                        {
                            rCol12 += R[pos[i], pos[j + 4]];
                            rCol13 += R[pos[i], pos[j + 8]];
                            rCol14 += R[pos[i], pos[j + 12]];
                            rCol23 += R[pos[i + 4], pos[j + 8]];
                            rCol24 += R[pos[i + 4], pos[j + 12]];
                            rCol34 += R[pos[i + 8], pos[j + 12]];
                        }
                    //----------------------Считаем дельтаL------------------------------------------------   
                    L12 = (rCol13 - rCol23) + (rCol14 - rCol24);
                    L23 = -(rCol12 - rCol13) + (rCol24 - rCol34);
                    L34 = -(rCol13 - rCol14) - (rCol23 - rCol24);
                    if (L12 > 0 && L12 > L23 && L12 > L34)
                    {   //------------------меняем местами в массиве позиций-------------------------
                        buf = pos[1]; pos[1] = pos[5]; pos[5] = buf;
                        buf = pos[2]; pos[2] = pos[6]; pos[6] = buf;
                        buf = pos[3]; pos[3] = pos[7]; pos[7] = buf;
                        buf = pos[4]; pos[4] = pos[8]; pos[8] = buf;
                        //---------------------------меняем визуал-------------------------
                        bufVis = P1.Visibility; P1.Visibility = P5.Visibility; P5.Visibility = bufVis;
                        bufVis = P2.Visibility; P2.Visibility = P6.Visibility; P6.Visibility = bufVis;
                        bufVis = P3.Visibility; P3.Visibility = P7.Visibility; P7.Visibility = bufVis;
                        bufVis = P4.Visibility; P4.Visibility = P8.Visibility; P8.Visibility = bufVis;
                        bufstr = TB1.Text; TB1.Text = TB5.Text; TB5.Text = bufstr;
                        bufstr = TB2.Text; TB2.Text = TB6.Text; TB6.Text = bufstr;
                        bufstr = TB3.Text; TB3.Text = TB7.Text; TB7.Text = bufstr;
                        bufstr = TB4.Text; TB4.Text = TB8.Text; TB8.Text = bufstr;
                    }
                    if (L23 > 0 && L23 > L12 && L23 > L34)
                    {
                        buf = pos[5]; pos[5] = pos[9]; pos[9] = buf;
                        buf = pos[6]; pos[6] = pos[10]; pos[10] = buf;
                        buf = pos[7]; pos[7] = pos[11]; pos[11] = buf;
                        buf = pos[8]; pos[8] = pos[12]; pos[12] = buf;
                        bufVis = P5.Visibility; P5.Visibility = P9.Visibility; P9.Visibility = bufVis;
                        bufVis = P6.Visibility; P6.Visibility = P10.Visibility; P10.Visibility = bufVis;
                        bufVis = P7.Visibility; P7.Visibility = P11.Visibility; P11.Visibility = bufVis;
                        bufVis = P8.Visibility; P8.Visibility = P12.Visibility; P12.Visibility = bufVis;
                        bufstr = TB5.Text; TB5.Text = TB9.Text; TB9.Text = bufstr;
                        bufstr = TB6.Text; TB6.Text = TB10.Text; TB10.Text = bufstr;
                        bufstr = TB7.Text; TB7.Text = TB11.Text; TB11.Text = bufstr;
                        bufstr = TB8.Text; TB8.Text = TB12.Text; TB12.Text = bufstr;
                    }
                    if (L34 > 0 && L34 > L12 && L34 > L23)
                    {
                        buf = pos[9]; pos[9] = pos[13]; pos[13] = buf;
                        buf = pos[10]; pos[10] = pos[14]; pos[14] = buf;
                        buf = pos[11]; pos[11] = pos[15]; pos[15] = buf;
                        buf = pos[12]; pos[12] = pos[16]; pos[16] = buf;
                        bufVis = P9.Visibility; P9.Visibility = P13.Visibility; P13.Visibility = bufVis;
                        bufVis = P10.Visibility; P10.Visibility = P14.Visibility; P14.Visibility = bufVis;
                        bufVis = P11.Visibility; P11.Visibility = P15.Visibility; P15.Visibility = bufVis;
                        bufVis = P12.Visibility; P12.Visibility = P16.Visibility; P16.Visibility = bufVis;
                        bufstr = TB9.Text; TB9.Text = TB13.Text; TB13.Text = bufstr;
                        bufstr = TB10.Text; TB10.Text = TB14.Text; TB14.Text = bufstr;
                        bufstr = TB11.Text; TB11.Text = TB15.Text; TB15.Text = bufstr;
                        bufstr = TB12.Text; TB12.Text = TB16.Text; TB16.Text = bufstr;
                    }
                }
            }
            if (pos[3] != 0 || pos[4] != 0 || pos[7] != 0 || pos[8] != 0 || pos[11] != 0 || pos[12] != 0 || pos[15] != 0 || pos[16] != 0) //если ложь,значит строк меньше двух
            {
                //----------------------Считаем связи cтроками------------------------------------------------   
                int rRow12 = 0, rRow13 = 0, rRow14 = 0, rRow23 = 0, rRow24 = 0, rRow34 = 0;
                int L12 = 1, L23 = 0, L34 = 0, buf; Visibility bufVis; string bufstr;
                while (L12 > 0 || L23 > 0 || L34 > 0)   //делать до тех пор,пока есть хотя бы одна положительная,то есть если на предыдущем шаге мы что-то меняли
                {
                    for (int i = 1; i < 17; i += 4)
                        for (int j = 1; j < 17; j += 4)
                        {
                            rRow12 += R[pos[i], pos[j + 1]];
                            rRow13 += R[pos[i], pos[j + 2]];
                            rRow14 += R[pos[i], pos[j + 3]];
                            rRow23 += R[pos[i + 1], pos[j + 2]];
                            rRow24 += R[pos[i + 1], pos[j + 3]];
                            rRow34 += R[pos[i + 2], pos[j + 3]];
                        }
                    //----------------------Считаем дельтаL------------------------------------------------   
                    L12 = (rRow13 - rRow23) + (rRow14 - rRow24);
                    L23 = -(rRow12 - rRow13) + (rRow24 - rRow34);
                    L34 = -(rRow13 - rRow14) - (rRow23 - rRow24);
                    if (L12 > 0 && L12 > L23 && L12 > L34)
                    {   //------------------меняем местами в массиве позиций-------------------------
                        buf = pos[1]; pos[1] = pos[2]; pos[2] = buf;
                        buf = pos[5]; pos[5] = pos[6]; pos[6] = buf;
                        buf = pos[9]; pos[9] = pos[10]; pos[10] = buf;
                        buf = pos[13]; pos[13] = pos[14]; pos[14] = buf;
                        //---------------------------меняем визуал-------------------------
                        bufVis = P1.Visibility; P1.Visibility = P2.Visibility; P2.Visibility = bufVis;
                        bufVis = P5.Visibility; P5.Visibility = P6.Visibility; P6.Visibility = bufVis;
                        bufVis = P9.Visibility; P9.Visibility = P10.Visibility; P10.Visibility = bufVis;
                        bufVis = P13.Visibility; P13.Visibility = P14.Visibility; P14.Visibility = bufVis;
                        bufstr = TB1.Text; TB1.Text = TB2.Text; TB2.Text = bufstr;
                        bufstr = TB5.Text; TB5.Text = TB6.Text; TB6.Text = bufstr;
                        bufstr = TB9.Text; TB9.Text = TB10.Text; TB10.Text = bufstr;
                        bufstr = TB13.Text; TB13.Text = TB14.Text; TB14.Text = bufstr;
                    }
                    if (L23 > 0 && L23 > L12 && L23 > L34)
                    {
                        buf = pos[2]; pos[2] = pos[3]; pos[3] = buf;
                        buf = pos[6]; pos[6] = pos[7]; pos[7] = buf;
                        buf = pos[10]; pos[10] = pos[11]; pos[11] = buf;
                        buf = pos[14]; pos[14] = pos[15]; pos[15] = buf;
                        bufVis = P2.Visibility; P2.Visibility = P3.Visibility; P3.Visibility = bufVis;
                        bufVis = P6.Visibility; P6.Visibility = P7.Visibility; P7.Visibility = bufVis;
                        bufVis = P10.Visibility; P10.Visibility = P11.Visibility; P11.Visibility = bufVis;
                        bufVis = P14.Visibility; P14.Visibility = P15.Visibility; P15.Visibility = bufVis;
                        bufstr = TB2.Text; TB2.Text = TB3.Text; TB3.Text = bufstr;
                        bufstr = TB6.Text; TB6.Text = TB7.Text; TB7.Text = bufstr;
                        bufstr = TB10.Text; TB10.Text = TB11.Text; TB11.Text = bufstr;
                        bufstr = TB14.Text; TB14.Text = TB15.Text; TB15.Text = bufstr;
                    }
                    if (L34 > 0 && L34 > L12 && L34 > L23)
                    {
                        buf = pos[3]; pos[3] = pos[4]; pos[4] = buf;
                        buf = pos[7]; pos[7] = pos[8]; pos[8] = buf;
                        buf = pos[11]; pos[11] = pos[12]; pos[12] = buf;
                        buf = pos[15]; pos[15] = pos[16]; pos[16] = buf;
                        bufVis = P3.Visibility; P3.Visibility = P4.Visibility; P4.Visibility = bufVis;
                        bufVis = P7.Visibility; P7.Visibility = P8.Visibility; P8.Visibility = bufVis;
                        bufVis = P11.Visibility; P11.Visibility = P12.Visibility; P12.Visibility = bufVis;
                        bufVis = P15.Visibility; P15.Visibility = P16.Visibility; P16.Visibility = bufVis;
                        bufstr = TB3.Text; TB3.Text = TB4.Text; TB4.Text = bufstr;
                        bufstr = TB7.Text; TB7.Text = TB8.Text; TB8.Text = bufstr;
                        bufstr = TB11.Text; TB11.Text = TB12.Text; TB12.Text = bufstr;
                        bufstr = TB15.Text; TB15.Text = TB16.Text; TB16.Text = bufstr;
                    }
                }
            }

        }
        public event EventHandler WindowShaffera_Closed;
        private void Window_Closed(object sender, EventArgs e)
        {
            WindowShaffera_Closed(this, EventArgs.Empty);
        }
    }
}
