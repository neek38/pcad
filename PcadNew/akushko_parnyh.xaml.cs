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
        bool auto_fl = false, step_fl = false, demo_fl = false;
        int[] el_array;
        int[,] drp_matr = new int[4, 4];
        int[,] r_matr;
        int[,] d_matr;
        int[,] pos_json;
        int[] positions = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[,] pos_matr = new int[,] { { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 }, { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 } };
        //матрица хранения max суммы для обмена
        int[,] pos_max = new int[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
        int step = 0;
        public akushko_parnyh()
        {
            InitializeComponent();
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text != "") tb.Text += "\nDemo clicked";
            else tb.Text = "Demo clicked";
            d_bt.IsEnabled = false;
            s_bt.IsEnabled = false;
            c_bt.IsEnabled = true;
            demo_fl = true;
            demo_set();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text != "") tb.Text += "\nStart clicked";
            else tb.Text = "Start clicked";
            d_bt.IsEnabled = false;
            auto_fl = true;
            n_bt.IsEnabled = false;
            c_bt.IsEnabled = true;
            demo_set();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (demo_fl == true)
            {
                n_bt.IsEnabled = false;
                drp_exchange();
                D_calc();
                Work();
            }
            else if (step_fl == true)
            {
                drp_exchange();
                D_calc();
                Work();
            }
            else if (auto_fl == false)
                step_fl = true;


        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ex_tb.Clear();
            tb.Clear();
            step_tb.Clear();
            for (int i = 0; i < 16; i++)
                pos_unvisible(i);
            d_bt.IsEnabled = true;
            s_bt.IsEnabled = true;
            n_bt.IsEnabled = true;
            step = 0;
            pos_max[0, 0] = 0;
            pos_max[0, 1] = 0;
            pos_max[0, 2] = 0;
            pos_max[1, 1] = 0;
            pos_max[1, 2] = 0;
        }

        private void demo_set()
        {
            el_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int c = 0;
            for (int i = 0; i < el_array.Length; i++)
            {
                if (el_array[i] != 0)
                {
                    pos_visible(c, el_array[i]);
                    drp_matr[pos_matr[0, c], pos_matr[1, c]] = el_array[i];
                }
                else
                {
                    drp_matr[pos_matr[0, c], pos_matr[1, c]] = el_array[i];
                }
                c++;
            }

            d_matr = new int[el_array.Length, el_array.Length];
            r_matr = new int[,] { { 0, 0, 6, 0, 0, 3, 3, 0, 0, 0, 0, 3, 0, 0, 0, 0 },
                                  { 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 1, 0, 3, 0, 0, 0 },
                                  { 6, 0, 0, 0, 0, 6, 6, 0, 0, 0, 3, 0, 0, 0, 3, 0 },
                                  { 0, 3, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 6, 1 },
                                  { 0, 0, 0, 0, 0, 1, 1, 1, 6, 6, 0, 3, 0, 0, 0, 0 },
                                  { 3, 0, 6, 0, 1, 0, 0, 0, 1, 1, 2, 2, 0, 4, 5, 0 },
                                  { 3, 0, 6, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 3, 0, 3 },
                                  { 0, 3, 0, 6, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 5, 1 },
                                  { 0, 0, 0, 0, 6, 1, 1, 1, 0, 6, 2, 1, 3, 0, 0, 2 },
                                  { 0, 0, 0, 0, 6, 1, 1, 1, 6, 0, 0, 0, 0, 0, 1, 1 },
                                  { 0, 1, 3, 0, 0, 2, 0, 0, 2, 0, 0, 1, 2, 0, 0, 2 },
                                  { 3, 0, 0, 0, 3, 2, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
                                  { 0, 3, 0, 0, 0, 0, 1, 0, 3, 0, 2, 1, 0, 2, 4, 1 },
                                  { 0, 0, 0, 0, 0, 4, 3, 0, 0, 0, 0, 0, 2, 0, 0, 1 },
                                  { 0, 0, 3, 6, 0, 5, 0, 5, 0, 1, 0, 1, 4, 0, 0, 0 },
                                  { 0, 0, 0, 1, 0, 0, 3, 1, 2, 1, 2, 0, 1, 1, 0, 0 } };
            tb.Text += "\nSource loaded";
            D_calc();
            Work();
        }

        private void Work()
        {
            string temp_char = "";
            string temp_num = "";
            string res = "";
            int[] temp_L = new int[drp_matr.Length];
            int ans = 0;
            int i = 0, j = 0, x = 0, y = 0;
            step++;
            step_tb.Text = step.ToString();

            for (i = 0; i < drp_matr.GetLength(0); i++)
            {
                for (j = 0; j < drp_matr.GetLength(1); j++)
                {
                    try //down
                    {
                        temp_char = "ΔL[" + drp_matr[i, j] + "][" + drp_matr[i, j + 1] + "]=";
                        if (drp_matr[i, j] != 0 && drp_matr[i, j + 1] != 0)
                        {
                            int w = 0;

                            for (x = 0; x < d_matr.GetLength(0); x++)
                            {
                                if (x + 1 != drp_matr[i, j] && x + 1 != drp_matr[i, j + 1])
                                {
                                    temp_L[w] = (r_matr[(drp_matr[i, j] - 1), x] - r_matr[(drp_matr[i, j + 1] - 1), x]) * (d_matr[(drp_matr[i, j] - 1), x] - d_matr[(drp_matr[i, j + 1] - 1), x]);
                                    temp_char = temp_char + "(R" + drp_matr[i, j] + (x + 1).ToString() + '-' + 'R' + drp_matr[i, j + 1] + (x + 1).ToString() + ")+(D" + drp_matr[i, j] + (x + 1).ToString() + "-D" + drp_matr[i, j + 1] + (x + 1).ToString() + ")+";
                                    temp_num = temp_num + '(' + r_matr[(drp_matr[i, j] - 1), x] + '-' + r_matr[(drp_matr[i, j + 1] - 1), x].ToString() + ")*(" + d_matr[(drp_matr[i, j] - 1), x].ToString() + '-' + d_matr[(drp_matr[i, j + 1] - 1), x].ToString() + ")+";
                                    w++;
                                }
                            }

                            for (int z = 0; z < temp_L.Length; z++)
                            {
                                ans += temp_L[z];
                            }

                            res = ans.ToString();

                            if (ans > pos_max[0, 0])
                            {
                                pos_max[0, 0] = ans;
                                pos_max[0, 1] = i; //столбцы
                                pos_max[0, 2] = j; //строки
                                pos_max[1, 1] = i; //столбцы
                                pos_max[1, 2] = j + 1; //строки
                            }

                            temp_char = temp_char.TrimEnd(new char[] { '+' });
                            temp_num = temp_num.TrimEnd(new char[] { '+' });

                            tb.Text += "\n" + temp_char + "=" + temp_num + "=" + res.ToString() + "\n";
                            ans = 0;
                            temp_char = "";
                            temp_num = "";
                        }
                    }
                    catch (Exception)
                    {
                        temp_char = "";
                    }

                    try //right
                    {
                        temp_char = "ΔL[" + drp_matr[i, j] + "][" + drp_matr[i + 1, j] + "]=";
                        if (drp_matr[i, j] != 0 && drp_matr[i + 1, j] != 0)
                        {
                            int w = 0;

                            for (x = 0; x < r_matr.GetLength(0); x++)
                            {
                                if (x + 1 != drp_matr[i, j] && x + 1 != drp_matr[i + 1, j])
                                {
                                    temp_L[w] = (r_matr[(drp_matr[i, j] - 1), x] - r_matr[(drp_matr[i + 1, j] - 1), x]) * (d_matr[(drp_matr[i, j] - 1), x] - d_matr[(drp_matr[i + 1, j] - 1), x]);
                                    temp_char = temp_char + "(R" + drp_matr[i, j] + (x + 1).ToString() + '-' + 'R' + drp_matr[i + 1, j] + (x + 1).ToString() + ")+(D" + drp_matr[i, j] + (x + 1).ToString() + "-D" + drp_matr[i + 1, j] + (x + 1).ToString() + ")+";
                                    temp_num = temp_num + '(' + r_matr[(drp_matr[i, j] - 1), x] + '-' + r_matr[(drp_matr[i + 1, j] - 1), x].ToString() + ")*(" + d_matr[(drp_matr[i, j] - 1), x].ToString() + '-' + d_matr[(drp_matr[i + 1, j] - 1), x].ToString() + ")+";
                                    w++;
                                }
                            }

                            for (int z = 0; z < temp_L.Length; z++)
                            {
                                ans += temp_L[z];
                            }

                            res = ans.ToString();

                            if (ans > pos_max[0, 0])
                            {
                                pos_max[0, 0] = ans;
                                pos_max[0, 1] = i; //столбцы
                                pos_max[0, 2] = j; //строки
                                pos_max[1, 1] = i + 1; //столбцы
                                pos_max[1, 2] = j; //строки
                            }

                            temp_char = temp_char.TrimEnd(new char[] { '+' });
                            temp_num = temp_num.TrimEnd(new char[] { '+' });

                            tb.Text += "\n" + temp_char + "=" + temp_num + "=" + res.ToString() + "\n";
                            ans = 0;
                            temp_char = "";
                            temp_num = "";
                        }
                    }
                    catch (Exception)
                    {
                        temp_char = "";
                    }
                }
            }
            tb.AppendText("\n_____________________________________________________________________________________________________________________________________________________________________\n"); 
            if (pos_max[0, 0] > 0)
            {
                ex_tb.AppendText("К обмену D" + drp_matr[pos_max[0, 1], pos_max[0, 2]].ToString() + " и D" + drp_matr[pos_max[1, 1], pos_max[1, 2]].ToString() + ". ΔL=" + pos_max[0, 0].ToString()+"\n");
                tb.ScrollToEnd();
                if (auto_fl == true)
                {
                    drp_exchange();
                    D_calc();
                    Work();
                }
                else
                {
                    n_bt.IsEnabled = true;
                }
            }
            else
            {
                ex_tb.AppendText("Нет элементов для обмена.");
                tb.ScrollToEnd();
                d_bt.IsEnabled = false;
                n_bt.IsEnabled = false;
                s_bt.IsEnabled = false;
            }
        }

        private void drp_exchange()
        {
            int buf = 0;
            buf = drp_matr[pos_max[0, 1], pos_max[0, 2]];
            drp_matr[pos_max[0, 1], pos_max[0, 2]] = drp_matr[pos_max[1, 1], pos_max[1, 2]];
            drp_matr[pos_max[1, 1], pos_max[1, 2]] = buf;
            pos_ex(drp_matr[pos_max[0, 1], pos_max[0, 2]], drp_matr[pos_max[1, 1], pos_max[1, 2]]);

            pos_max[0, 0] = 0;
            pos_max[0, 1] = 0;
            pos_max[0, 2] = 0;
            pos_max[1, 1] = 0;
            pos_max[1, 2] = 0;
        }

        private void D_calc()
        {
            for (int i = 0; i < drp_matr.GetLength(1); i++)
                for (int j = 0; j < drp_matr.GetLength(0); j++)
                {
                    for (int a = 0; a < drp_matr.GetLength(1); a++)
                        for (int b = 0; b < drp_matr.GetLength(0); b++)
                        {
                            if (drp_matr[a, b] != 0 && drp_matr[i, j] != 0)
                            {
                                d_matr[drp_matr[i, j] - 1, drp_matr[a, b] - 1] = Math.Abs(Math.Abs(a - i) + Math.Abs(b - j));
                                d_matr[drp_matr[a, b] - 1, drp_matr[i, j] - 1] = Math.Abs(Math.Abs(a - i) + Math.Abs(b - j));
                            }
                        }
                }
            //string buf = "";
            //for (int i = 0; i < d_matr.GetLength(0); i++)
            //{
            //    for (int j = 0; j < d_matr.GetLength(1); j++)
            //    {
            //        buf = buf + "  " + d_matr[i, j].ToString();
            //    }
            //    tb.Text += buf + "\n";
            //    buf = "";
            //}
        }

        private void pos_visible(int pos, int el)
        {
            switch (pos)
            {
                case 0:
                    pos1.Visibility = System.Windows.Visibility.Visible;
                    pos1.Text = "D" + el;
                    positions[0] = el;
                    break;
                case 1:
                    pos2.Visibility = System.Windows.Visibility.Visible;
                    pos2.Text = "D" + el;
                    positions[1] = el;
                    break;
                case 2:
                    pos3.Visibility = System.Windows.Visibility.Visible;
                    pos3.Text = "D" + el;
                    positions[2] = el;
                    break;
                case 3:
                    pos4.Visibility = System.Windows.Visibility.Visible;
                    pos4.Text = "D" + el;
                    positions[3] = el;
                    break;
                case 4:
                    pos5.Visibility = System.Windows.Visibility.Visible;
                    pos5.Text = "D" + el;
                    positions[4] = el;
                    break;
                case 5:
                    pos6.Visibility = System.Windows.Visibility.Visible;
                    pos6.Text = "D" + el;
                    positions[5] = el;
                    break;
                case 6:
                    pos7.Visibility = System.Windows.Visibility.Visible;
                    pos7.Text = "D" + el;
                    positions[6] = el;
                    break;
                case 7:
                    pos8.Visibility = System.Windows.Visibility.Visible;
                    pos8.Text = "D" + el;
                    positions[7] = el;
                    break;
                case 8:
                    pos9.Visibility = System.Windows.Visibility.Visible;
                    pos9.Text = "D" + el;
                    positions[8] = el;
                    break;
                case 9:
                    pos10.Visibility = System.Windows.Visibility.Visible;
                    pos10.Text = "D" + el;
                    positions[9] = el;
                    break;
                case 10:
                    pos11.Visibility = System.Windows.Visibility.Visible;
                    pos11.Text = "D" + el;
                    positions[10] = el;
                    break;
                case 11:
                    pos12.Visibility = System.Windows.Visibility.Visible;
                    pos12.Text = "D" + el;
                    positions[11] = el;
                    break;
                case 12:
                    pos13.Visibility = System.Windows.Visibility.Visible;
                    pos13.Text = "D" + el;
                    positions[12] = el;
                    break;
                case 13:
                    pos14.Visibility = System.Windows.Visibility.Visible;
                    pos14.Text = "D" + el;
                    positions[13] = el;
                    break;
                case 14:
                    pos15.Visibility = System.Windows.Visibility.Visible;
                    pos15.Text = "D" + el;
                    positions[14] = el;
                    break;
                case 15:
                    pos16.Visibility = System.Windows.Visibility.Visible;
                    pos16.Text = "D" + el;
                    positions[15] = el;
                    break;
                default:
                    break;
            }

        }

        private void pos_unvisible(int pos)
        {
            switch (pos)
            {
                case 0:
                    pos1.Visibility = System.Windows.Visibility.Hidden;
                    pos1.Text = " ";
                    break;
                case 1:
                    pos2.Visibility = System.Windows.Visibility.Hidden;
                    pos2.Text = " ";
                    break;
                case 2:
                    pos3.Visibility = System.Windows.Visibility.Hidden;
                    pos3.Text = " ";
                    break;
                case 3:
                    pos4.Visibility = System.Windows.Visibility.Hidden;
                    pos4.Text = " ";
                    break;
                case 4:
                    pos5.Visibility = System.Windows.Visibility.Hidden;
                    pos5.Text = " ";
                    break;
                case 5:
                    pos6.Visibility = System.Windows.Visibility.Hidden;
                    pos6.Text = " ";
                    break;
                case 6:
                    pos7.Visibility = System.Windows.Visibility.Hidden;
                    pos7.Text = " ";
                    break;
                case 7:
                    pos8.Visibility = System.Windows.Visibility.Hidden;
                    pos8.Text = " ";
                    break;
                case 8:
                    pos9.Visibility = System.Windows.Visibility.Hidden;
                    pos9.Text = " ";
                    break;
                case 9:
                    pos10.Visibility = System.Windows.Visibility.Hidden;
                    pos10.Text = " ";
                    break;
                case 10:
                    pos11.Visibility = System.Windows.Visibility.Hidden;
                    pos11.Text = " ";
                    break;
                case 11:
                    pos12.Visibility = System.Windows.Visibility.Hidden;
                    pos12.Text = " ";
                    break;
                case 12:
                    pos13.Visibility = System.Windows.Visibility.Hidden;
                    pos13.Text = " ";
                    break;
                case 13:
                    pos14.Visibility = System.Windows.Visibility.Hidden;
                    pos14.Text = " ";
                    break;
                case 14:
                    pos15.Visibility = System.Windows.Visibility.Hidden;
                    pos15.Text = " ";
                    break;
                case 15:
                    pos16.Visibility = System.Windows.Visibility.Hidden;
                    pos16.Text = " ";
                    break;
                default:
                    break;
            }

        }

        private void pos_ex(int a, int b)
        {
            string buf;
            int pos_a = -1, pos_b = -1, ibuf = -1;
            pos_a = search_pos(a);
            pos_b = search_pos(b);
            switch (pos_a)
            {
                case 0:
                    buf = pos1.Text;
                    pos1.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 1:
                    buf = pos2.Text;
                    pos2.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 2:
                    buf = pos3.Text;
                    pos3.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 3:
                    buf = pos4.Text;
                    pos4.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 4:
                    buf = pos5.Text;
                    pos5.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 5:
                    buf = pos6.Text;
                    pos6.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 6:
                    buf = pos7.Text;
                    pos7.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 7:
                    buf = pos8.Text;
                    pos8.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 8:
                    buf = pos9.Text;
                    pos9.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 9:
                    buf = pos10.Text;
                    pos10.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 10:
                    buf = pos11.Text;
                    pos11.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 11:
                    buf = pos12.Text;
                    pos12.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 12:
                    buf = pos13.Text;
                    pos13.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 13:
                    buf = pos14.Text;
                    pos14.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 14:
                    buf = pos15.Text;
                    pos15.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                case 15:
                    buf = pos16.Text;
                    pos16.Text = "D" + b;
                    pos_ex2(pos_b, buf);
                    break;
                default:
                    break;
            }
            pos_a = search_pos(drp_matr[pos_max[0, 1], pos_max[0, 2]]);
            pos_b = search_pos(drp_matr[pos_max[1, 1], pos_max[1, 2]]);
            ibuf = positions[pos_a];
            positions[pos_a] = positions[pos_b];
            positions[pos_b] = ibuf;
        }

        private void pos_ex2(int b, string buf)
        {
            switch (b)
            {
                case 0:
                    pos1.Text = buf;
                    break;
                case 1:
                    pos2.Text = buf;
                    break;
                case 2:
                    pos3.Text = buf;
                    break;
                case 3:
                    pos4.Text = buf;
                    break;
                case 4:
                    pos5.Text = buf;
                    break;
                case 5:
                    pos6.Text = buf;
                    break;
                case 6:
                    pos7.Text = buf;
                    break;
                case 7:
                    pos8.Text = buf;
                    break;
                case 8:
                    pos9.Text = buf;
                    break;
                case 9:
                    pos10.Text = buf;
                    break;
                case 10:
                    pos11.Text = buf;
                    break;
                case 11:
                    pos12.Text = buf;
                    break;
                case 12:
                    pos13.Text = buf;
                    break;
                case 13:
                    pos14.Text = buf;
                    break;
                case 14:
                    pos15.Text = buf;
                    break;
                case 15:
                    pos16.Text = buf;
                    break;
                default:
                    break;
            }
        }

        private int search_pos(int d)
        {
            int pos = -1;
            for (int i = 0; i < positions.Length; i++)
                if (positions[i] == d)
                    pos = i;
            return pos;
        }
    }
}
