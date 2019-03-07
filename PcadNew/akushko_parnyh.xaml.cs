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
        List<Rectangle> p = new List<Rectangle>();
        List<TextBlock> pos = new List<TextBlock>();
        bool auto_fl = false, step_fl = false, demo_fl = false, ffl = true;
        int[] el_array;
        int[,] drp_matr = new int[4, 4];
        int[,] r_matr;
        int[,] d_matr;
        int[,] pos_json;
        int[] positions = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[,] pos_matr = new int[,] { { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 }, { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 } };
        //матрица хранения max суммы для обмена
        int[,] pos_max = new int[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
        int step = 0, D1 = -1, D2 = -1, D3 = -1, D4 = -1;
        public akushko_parnyh()
        {
            InitializeComponent();
            p.Add(p1); p.Add(p2); p.Add(p3); p.Add(p4); p.Add(p5); p.Add(p6); p.Add(p7); p.Add(p8);
            p.Add(p9); p.Add(p10); p.Add(p11); p.Add(p12); p.Add(p13); p.Add(p14); p.Add(p15); p.Add(p16);
            pos.Add(pos1); pos.Add(pos2); pos.Add(pos3); pos.Add(pos4); pos.Add(pos5); pos.Add(pos6); pos.Add(pos7); pos.Add(pos8);
            pos.Add(pos9); pos.Add(pos10); pos.Add(pos11); pos.Add(pos12); pos.Add(pos13); pos.Add(pos14); pos.Add(pos15); pos.Add(pos16); 
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            tb.Text += "\nDemo clicked\n";
            d_bt.IsEnabled = false;
            c_bt.IsEnabled = true;
            demo_fl = true;
            demo_set();
            D_calc();
            Work();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            tb.Text += "\nStart clicked\n";
            d_bt.IsEnabled = false;
            auto_fl = true;
            n_bt.IsEnabled = false;
            c_bt.IsEnabled = true;
            if (demo_fl == false && auto_fl == true && ffl == false)
            {
                json_set();
                D_calc();
                Work();
            }
            else if (demo_fl == false && auto_fl == true && ffl == true)
            {
                d_bt.IsEnabled = false;
                json_set();
                D_calc();
                Work();
            }
            if (demo_fl == true && auto_fl == true)
            {
                drp_exchange();
                D_calc();
                Work();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            uncolorize(D1, D2);
            uncolorize(D3, D4);
            n_bt.IsEnabled = false;
            drp_exchange();
            D_calc();
            Work();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ex_tb.Clear();
            tb.Clear();
            for (int i = 0; i < 16; i++)
            {
                pos_unvisible(i);
                p[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
            }
            auto_fl = demo_fl = step_fl = false;
            d_bt.IsEnabled = s_bt.IsEnabled = n_bt.IsEnabled = true;
            step = pos_max[0, 0] = pos_max[0, 1] = pos_max[0, 2] = pos_max[1, 1] = pos_max[1, 2] = 0;
            rd.Clear(); rd2.Clear(); dr.Clear(); dr2.Clear(); D_tb.Clear(); R_tb.Clear();
        }

        private void json_set()
        {

        }

        private void json_save()
        {
            ex_tb.AppendText("\n\nФайл сохранен.");
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
            string probel = "   ", str = "";
            dr.Text = dr2.Text = "";
            for (int i = 0; i < positions.Length; i++)
            {
                if (i == 15)
                {
                    rd.AppendText("D" + positions[i].ToString());
                    rd2.AppendText("D" + positions[i].ToString());
                }
                else
                {
                    rd.AppendText("D" + positions[i].ToString() + "\n");
                    rd2.AppendText("D" + positions[i].ToString() + "\n");
                }
            }
            for (int i = 0; i < r_matr.GetLength(0); i++)
            {
                for (int j = 0; j < r_matr.GetLength(1); j++)
                {
                    str += r_matr[i, j].ToString() + probel;
                }
                if (i != 15)
                {
                    R_tb.AppendText(str + "\n");
                }
                else
                {
                    R_tb.AppendText(str);
                }
                str = "";
            }

            tb.Text += "\nSource loaded";
        }

        private void Work()
        {
            string temp_char = "";
            string temp_num = "";
            string res = "";
            int[] temp_L = new int[drp_matr.Length];
            int ans = 0;
            int i = 0, j = 0, x = 0;
            step++;
            tb.Text += "\n Шаг " + step.ToString() + "\n";

            for (i = 0; i < drp_matr.GetLength(0); i++)
            {
                for (j = 0; j < drp_matr.GetLength(1); j++)
                {
                    try //down
                    {
                        temp_char = "ΔL[" + drp_matr[i, j] + "][" + drp_matr[i, j + 1] + "]=";
                        colorize(drp_matr[i, j], drp_matr[i, j + 1]);
                        D1 = drp_matr[i, j]; D2 = drp_matr[i, j + 1];
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
                        colorize(drp_matr[i, j], drp_matr[i + 1, j]);
                        D3 = drp_matr[i, j]; D4 = drp_matr[i + 1, j];
                        if (drp_matr[i, j] != 0 && drp_matr[i + 1, j] != 0)
                        {
                            int w = 0;

                            for (x = 0; x < d_matr.GetLength(0); x++)
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
                ex_tb.AppendText( step.ToString() + ") К обмену D" + drp_matr[pos_max[0, 1], pos_max[0, 2]].ToString() + " и D" + drp_matr[pos_max[1, 1], pos_max[1, 2]].ToString() + ". ΔL=" + pos_max[0, 0].ToString()+"\n");
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
                json_save();
            }
        }

        private void uncolorize(int d1, int d2)
        {
            int a = search_pos(d1);
            int b = search_pos(d2);
            p[a].Fill = p[b].Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
        }

        private void colorize(int d1, int d2)
        {
            int a = search_pos(d1);
            int b = search_pos(d2);
            p[a].Fill = p[b].Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
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

            string probel = "   ", str = "";
            dr.Text = dr2.Text = "";
            for (int i = 0; i < positions.Length; i++)
            {
                if (i == 15)
                {
                    dr.AppendText("D" + positions[i].ToString());
                    dr2.AppendText("D" + positions[i].ToString());
                }
                else
                {
                    dr.AppendText("D" + positions[i].ToString() + "\n");
                    dr2.AppendText("D" + positions[i].ToString() + "\n");
                }
            }
            for (int i = 0; i < r_matr.GetLength(0); i++)
            {
                for (int j = 0; j < r_matr.GetLength(1); j++)
                {
                    str += d_matr[i, j].ToString() + probel;
                }
                if (i != 15)
                {
                    D_tb.AppendText(str + "\n");
                }
                else
                {
                    D_tb.AppendText(str);
                }
                str = "";
            }
        }

        private void pos_visible(int position, int el)
        {
                pos[position].Visibility = System.Windows.Visibility.Visible;
                pos[position].Text = "D" + el;
                positions[position] = el;
        }

        private void pos_unvisible(int position)
        {
            pos[position].Visibility = System.Windows.Visibility.Hidden;
            pos[position].Text = " ";
        }

        private void pos_ex(int a, int b)
        {
            string buf;
            int pos_a = -1, pos_b = -1, ibuf = -1;
            pos_a = search_pos(a);
            pos_b = search_pos(b);

            buf = pos[pos_a].Text;
            pos[pos_a].Text = pos[pos_b].Text;
            pos[pos_b].Text = buf;
            
            pos_a = search_pos(drp_matr[pos_max[0, 1], pos_max[0, 2]]);
            pos_b = search_pos(drp_matr[pos_max[1, 1], pos_max[1, 2]]);
            ibuf = positions[pos_a];
            positions[pos_a] = positions[pos_b];
            positions[pos_b] = ibuf;
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
