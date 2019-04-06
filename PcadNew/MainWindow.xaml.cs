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
using System.Data;
using System.Collections.ObjectModel;

namespace PcadNew
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable myDT;
        int p = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_MVV(object sender, RoutedEventArgs e)
        {


        }
        public void Drow_drp(bool mode)
        {
            //string[] el = new string[90];
            //for (int i = 0; i < 90; i++)
            //{
            //    el[i] = "";
            //}

            Button btn_step = new Button
            {
                Content = "Следующий шаг",
                Margin = new Thickness(550, 100, 0, 0)
            };
            Button btn_auto = new Button
            {
                Content = "Авто",

                Margin = new Thickness(650, 100, 0, 0)
            };

            Button btn_start = new Button
            {
                Content = "Пуск",
                Margin = new Thickness(550, 75, 0, 0)
            };
            RadioButton btn_automode = new RadioButton
            {
                Content = "Авто режим",
                Margin = new Thickness(550, 50, 0, 0)
            };
            RadioButton btn_stepmode = new RadioButton
            {
                Content = "Пошаговый режим",
                Margin = new Thickness(650, 50, 0, 0)
            };
            //DRP.Children.Add(btn_step);
            //DRP.Children.Add(btn_auto);
            //DRP.Children.Add(btn_start);
            //DRP.Children.Add(btn_stepmode);
            //DRP.Children.Add(btn_automode);
            myDT = new DataTable();
            for (int m = 0; m < 8; m++)
                myDT.Columns.Add((m + 1).ToString());
            myDT.Rows.Add("", "", "", "", "", "*", "", "");
            myDT.Rows.Add("", "", "", "", "", "*", "*", "*");
            myDT.Rows.Add("", "", "", "", "", "B", "", "");
            myDT.Rows.Add("", "*", "*", "*", "", "*", "", "*");
            myDT.Rows.Add("A", "", "", "", "", "", "", "*");
            myDT.Rows.Add("", "", "", "*", "*", "*", "*", "*");
            myDT.Rows.Add("", "", "", "", "", "", "", "");
            myDT.Rows.Add("", "*", "*", "*", "*", "*", "*", "");
            matr.ItemsSource = myDT.DefaultView;
            //bool fl = true;
            //int k = 1;
            //fl = wave(1, 1, k, ref myDT);
            //fl = wave(4, 5, k, ref myDT);
            //int i = 0;
            //int j = 0;
            //while (fl)
            //{
            //    for (i = 0; i < 8; i++)
            //    {
            //        if (!fl) break;
            //        for (j = 0; j < 8; j++)
            //        {
            //            try
            //            {
            //                if (k == Convert.ToInt32(myDT.Rows[i][j]))
            //                    fl = wave(i, j, k + 1, ref myDT);
            //                if (!fl) break;
            //            }
            //            catch (Exception)
            //            {
            //            }
            //        }

            //    }
            //    k++;
            //}
        }
        public bool wave(int a, int b, int c, ref DataTable dt)
        {
            int k;
            if (a != 0)
                if (dt.Rows[a - 1][b].ToString() == "")
                    dt.Rows[a - 1][b] = c;
                else
                    try
                    {
                        k = Convert.ToInt32(dt.Rows[a - 1][b]);
                        if (k == c || (c - 1) == k)
                        {
                            if (search(a, b, k - 1, dt, '0') != search(a - 1, b, k, dt, 'd')) return false;

                        }


                    }
                    catch (Exception)
                    {
                    }
            if (dt.Rows[a + 1][b].ToString() == "")
                dt.Rows[a + 1][b] = c;
            else
                try
                {
                    k = Convert.ToInt32(dt.Rows[a + 1][b]);
                    if (k == c || (k - 1) == c)
                    {

                        if (search(a, b, k - 1, dt, '0') != search(a - 1, b, k, dt, 'u')) return false;
                    }

                }
                catch (Exception)
                {
                }
            if (b != 0)
                if (dt.Rows[a][b - 1].ToString() == "")
                    dt.Rows[a][b - 1] = c;
                else
                    try
                    {
                        k = Convert.ToInt32(dt.Rows[a][b - 1]);
                        if (k == c || (k - 1) == c)
                        {
                            if (search(a, b, k - 1, dt, '0') != search(a - 1, b, k, dt, 'r')) return false;
                        }

                    }
                    catch (Exception)
                    {
                    }
            if (dt.Rows[a][b + 1].ToString() == "")
                dt.Rows[a][b + 1] = c;
            else
                try
                {
                    k = Convert.ToInt32(dt.Rows[a][b + 1]);
                    if (k == c || (k - 1) == c)
                    {
                        if (search(a, b, k - 1, dt, '0') != search(a - 1, b, k, dt, 'l')) return false;
                    }

                }
                catch (Exception)
                {
                }



            return true;

        }
        public char search(int a, int b, int c, DataTable dt, char sym)
        {
            if (c == 1)
            {
                if (a != 0 && sym != 'u')
                {
                    if ((dt.Rows[a - 1][b].ToString() == "A")) return 'A';
                    if (dt.Rows[a - 1][b].ToString() == "B") return 'B';
                }
                if (sym != 'd')
                {
                    if (dt.Rows[a + 1][b].ToString() == "A") return 'A';
                    if (dt.Rows[a + 1][b].ToString() == "B") return 'B';
                }
                if (b != 0 && sym != 'l')
                {
                    if (dt.Rows[a][b - 1].ToString() == "A") return 'A';
                    if (dt.Rows[a][b - 1].ToString() == "B") return 'B';
                }
                if (sym != 'r')
                {
                    if (dt.Rows[a][b + 1].ToString() == "A") return 'A';
                    if (dt.Rows[a][b + 1].ToString() == "B") return 'B';
                }
            }
            else
            if (a != 0 && sym != 'u')
                if (dt.Rows[a - 1][b].ToString() != "")
                {
                    try
                    {
                        if (c > Convert.ToInt32(dt.Rows[a - 1][b]))
                            return search(a - 1, b, Convert.ToInt32(dt.Rows[a - 1][b]), dt, '0');
                    }
                    catch (Exception)
                    {
                    }
                }
            if (dt.Rows[a + 1][b].ToString() != "" && sym != 'd')
            {
                try
                {
                    if (c > Convert.ToInt32(dt.Rows[a + 1][b]))
                        return search(a + 1, b, Convert.ToInt32(dt.Rows[a + 1][b]), dt, '0');
                }
                catch (Exception)
                {
                }
            }
            if (b != 0 && sym != 'l')
                if (dt.Rows[a][b - 1].ToString() != "")
                {
                    try
                    {
                        if (c > Convert.ToInt32(dt.Rows[a][b - 1]))
                            return search(a, b - 1, Convert.ToInt32(dt.Rows[a][b - 1]), dt, '0');
                    }
                    catch (Exception)
                    {
                    }
                }
            if (dt.Rows[a][b + 1].ToString() != "" && sym != 'r')
            {
                try
                {
                    if (c > Convert.ToInt32(dt.Rows[a][b + 1]))
                        return search(a, b + 1, Convert.ToInt32(dt.Rows[a][b + 1]), dt, '0');
                }
                catch (Exception)
                {
                }
            }
            return '0';
        }
        private void Mode1_Click(object sender, RoutedEventArgs e)
        {
            Drow_drp(true);
        }

        private void Mode2_Click(object sender, RoutedEventArgs e)
        {
            Drow_drp(false);
        }
        static bool fl1 = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool fl = true;
            int k = 1;
            int step;
            fl = wave(4, 0, k, ref myDT);
            //fl = wave(4, 5, k, ref myDT);
            int i = 0;
            int j = 0;
            while (fl || fl1)
            {
                for (i = 0; i < 8; i++)
                {
                    if (!fl || !fl1) break;
                    for (j = 0; j < 8; j++)
                    {
                        try
                        {
                            if (k == Convert.ToInt32(myDT.Rows[i][j]))
                            {
                                fl = wave(i, j, k + 1, ref myDT);
                                fl1 = check(i, j, myDT);
                            }
                            if (!fl || !fl1) break;
                        }
                        catch (Exception)
                        {
                        }
                    }

                }

                k++;
                if (!fl1) break;
            }
            if (!fl1)//Трассировка
            {
                step = k;
                tracing(4, 0, 2, 5, ref myDT, step);
            }
        }
        public bool check(int a, int b, DataTable dt)
        {
            if (dt.Rows[a - 1][b].ToString() == "B" || dt.Rows[a + 1][b].ToString() == "B" || dt.Rows[a][b - 1].ToString() == "B" || dt.Rows[a][b + 1].ToString() == "B")
            {
                MessageBox.Show("Вы достигли точки В");
                return false;
            }
            else
                return true;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            bool fl = true;
            int step;

            if (p == 0)
            {
                p = 1;
                fl = wave(0, 4, p, ref myDT);
                //fl = wave(4, 5, p, ref myDT);

                return;
            }
            for (int i = 0; i < 8; i++)
            {
                if (!fl || !fl1) break;
                for (int j = 0; j < 8; j++)
                {
                    try
                    {
                        if (p == Convert.ToInt32(myDT.Rows[i][j]))
                        {

                            fl = wave(i, j, p + 1, ref myDT);
                            fl1 = check(i, j, myDT);
                        }
                        if (!fl || !fl1) break;
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            p++;
            if (!fl1)//Трассировка
            {
                step = p;
                tracing(4, 0, 2, 5, ref myDT, step);
            }
        }

        public void tracing(int iA, int jA, int iB, int jB, ref DataTable dt, int step1)
        {
            int Ai = iA, Aj = jA, Bi = iB, Bj = jB;
            bool fl = true, fl1 = true, fl2 = true;
            int count1 = 0, count2 = 0;
            int step = step1;
            while (fl)
            {
                while (fl1)
                {
                    if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                    {
                        //MessageBox.Show("Трассировка завершена");
                        fl = false;
                        break;
                    }



                    if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == (step - 1).ToString())
                    {
                        //dt.Rows[Bi - 1][Bj] = "|";
                        Bi = Bi - 1;
                        step -= 1;
                    }
                    else
                    {
                        if (Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == (step - 1).ToString())
                        {
                            //dt.Rows[Bi + 1][Bj] = "|";
                            Bi = Bi + 1;
                            step -= 1;
                        }
                        else
                        {
                            fl1 = false;
                            fl2 = true;
                            count1++;
                            break;
                        }
                    }
                }
                while (fl2)
                {
                    if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                    {
                        //MessageBox.Show("Трассировка завершена");
                        fl = false;
                        break;
                    }

                    if (Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == (step - 1).ToString())
                    {
                        //dt.Rows[Bi][Bj - 1] = "—";
                        Bj = Bj - 1;
                        step -= 1;
                    }
                    else
                    {
                        if (Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == (step - 1).ToString())
                        {
                            //dt.Rows[Bi][Bj + 1] = "—";
                            Bj = Bj + 1;
                            step -= 1;
                        }
                        else
                        {
                            fl2 = false;
                            fl1 = true;
                            count1++;
                            break;
                        }
                    }
                }
            }
            fl = true; fl1 = true; fl2 = true;
            Ai = iA; Aj = jA; Bi = iB; Bj = jB;
            step = step1;
            while (fl)
            {
                while (fl1)
                {
                    if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                    {
                        //MessageBox.Show("Трассировка завершена");
                        fl = false;
                        break;
                    }

                    if (Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == (step - 1).ToString())
                    {
                        //dt.Rows[Bi][Bj - 1] = "—";
                        Bj = Bj - 1;
                        step -= 1;
                    }
                    else
                    {
                        if (Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == (step - 1).ToString())
                        {
                            //dt.Rows[Bi][Bj + 1] = "—";
                            Bj = Bj + 1;
                            step -= 1;
                        }
                        else
                        {
                            fl2 = false;
                            fl1 = true;
                            count2++;
                            break;
                        }

                    }
                }
                while (fl1)
                {
                    if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                    {
                        //MessageBox.Show("Трассировка завершена");
                        fl = false;
                        break;
                    }



                    if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == (step - 1).ToString())
                    {
                        //dt.Rows[Bi - 1][Bj] = "|";
                        Bi = Bi - 1;
                        step -= 1;
                    }
                    else
                    {
                        if (Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == (step - 1).ToString())
                        {
                            //dt.Rows[Bi + 1][Bj] = "|";
                            Bi = Bi + 1;
                            step -= 1;
                        }
                        else
                        {
                            fl1 = false;
                            fl2 = true;
                            count2++;
                            break;
                        }
                    }
                }
            }


            fl = true; fl1 = true; fl2 = true;
            Ai = iA; Aj = jA; Bi = iB; Bj = jB;
            step = step1;
            while (fl)
            {
                if (count1 <= count2)
                {
                    while (fl1)
                    {
                        try
                        {
                            if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                            {
                                MessageBox.Show("Трассировка завершена");
                                fl = false;
                                break;
                            }



                            if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == (step - 1).ToString())
                            {
                                dt.Rows[Bi - 1][Bj] = "|";
                                Bi = Bi - 1;
                                step -= 1;
                            }
                            else
                            {
                                if (Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == (step - 1).ToString())
                                {
                                    dt.Rows[Bi + 1][Bj] = "|";
                                    Bi = Bi + 1;
                                    step -= 1;
                                }
                                else
                                {
                                    fl1 = false;
                                    fl2 = true;
                                    break;
                                }
                            }
                        }
                        catch { }
                    }
                    while (fl2)
                    {
                        try
                        {

                            if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                            {
                                MessageBox.Show("Трассировка завершена");
                                fl = false;
                                break;
                            }

                            if (Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == (step - 1).ToString())
                            {
                                dt.Rows[Bi][Bj - 1] = "—";
                                Bj = Bj - 1;
                                step -= 1;
                            }
                            else
                            {
                                if (Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == (step - 1).ToString())
                                {
                                    dt.Rows[Bi][Bj + 1] = "—";
                                    Bj = Bj + 1;
                                    step -= 1;
                                }
                                else
                                {
                                    fl2 = false;
                                    fl1 = true;
                                    break;
                                }
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    while (fl1)
                    {
                        if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                        {
                            MessageBox.Show("Трассировка завершена");
                            fl = false;
                            break;
                        }

                        if (Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == (step - 1).ToString())
                        {
                            dt.Rows[Bi][Bj - 1] = "—";
                            Bj = Bj - 1;
                            step -= 1;
                        }
                        else
                        {
                            if (Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == (step - 1).ToString())
                            {
                                dt.Rows[Bi][Bj + 1] = "—";
                                Bj = Bj + 1;
                                step -= 1;
                            }
                            else
                            {
                                fl1 = false;
                                fl2 = true;
                                break;
                            }
                        }
                    }

                    while (fl2)
                    {
                        try
                        {
                            if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == "A" || Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == "A" || Bj != 0 && dt.Rows[Bi][Bj - 1].ToString() == "A" || Bj != 7 && dt.Rows[Bi][Bj + 1].ToString() == "A")
                            {
                                MessageBox.Show("Трассировка завершена");
                                fl = false;
                                break;
                            }



                            if (Bi != 0 && dt.Rows[Bi - 1][Bj].ToString() == (step - 1).ToString())
                            {
                                dt.Rows[Bi - 1][Bj] = "|";
                                Bi = Bi - 1;
                                step -= 1;
                            }
                            else
                            {
                                if (Bi != 7 && dt.Rows[Bi + 1][Bj].ToString() == (step - 1).ToString())
                                {
                                    dt.Rows[Bi + 1][Bj] = "|";
                                    Bi = Bi + 1;
                                    step -= 1;
                                }
                                else
                                {
                                    fl1 = false;
                                    fl2 = true;
                                    break;
                                }
                            }

                        }
                        catch { }
                    }
                }
            }

        }
    }
}
