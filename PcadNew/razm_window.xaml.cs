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
//using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PcadNew
{
    /// <summary>
    /// Логика взаимодействия для razm_window.xaml
    /// </summary>
    public partial class razm_window : Window
    {
        int[,] R_matr = new int[,] { { } };
        int[,] D_matr = new int[16, 16];
        int[,] Uzel_matr = new int[,] { { } };
        int[] Pol_mas = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int step = 1;
        int demo_click = 0;
        List<TextBlock> texts = new List<TextBlock>() { };
        List<Rectangle> bloks = new List<Rectangle>() { };
        List<DRP> newp = new List<DRP>();
        List<DRP> resp = new List<DRP>();
        List<U_R> newr = new List<U_R>();
        DRP prod = new DRP();
        DRP prod1 = new DRP();
        int[,] r_matr_json = new int[,] {     {0,1,1,0,1,1,1,2,3,1,2,0},
                                              {1,0,2,1,1,1,1,2,3,2,1,2},
                                              {1,2,0,1,0,1,2,1,2,3,4,1},
                                              {0,1,1,0,3,1,1,2,3,1,2,1},
                                              {1,1,0,3,0,1,1,0,1,2,3,1},
                                              {1,1,1,1,1,0,0,1,2,1,2,3},
                                              {1,1,2,1,1,0,0,1,2,1,0,1},
                                              {2,2,1,3,0,1,1,0,1,2,3,1},
                                              {3,3,2,3,1,2,2,1,0,1,2,3},
                                              {1,2,3,1,2,1,1,2,1,0,2,3},
                                              {2,1,4,2,3,2,0,3,2,2,0,3},
                                              {0,2,1,1,1,3,1,1,3,3,3,0} };

        public razm_window()
        {
            InitializeComponent();
            texts.Add(pos1);
            bloks.Add(p1);
            texts.Add(pos2);
            bloks.Add(p2);
            texts.Add(pos3);
            bloks.Add(p3);
            texts.Add(pos4);
            bloks.Add(p4);
            texts.Add(pos5);
            bloks.Add(p5);
            texts.Add(pos6);
            bloks.Add(p6);
            texts.Add(pos7);
            bloks.Add(p7);
            texts.Add(pos8);
            bloks.Add(p8);
            texts.Add(pos9);
            bloks.Add(p9);
            texts.Add(pos10);
            bloks.Add(p10);
            texts.Add(pos11);
            bloks.Add(p11);
            texts.Add(pos12);
            bloks.Add(p12);
            texts.Add(pos13);
            bloks.Add(p13);
            texts.Add(pos14);
            bloks.Add(p14);
            texts.Add(pos15);
            bloks.Add(p15);
            texts.Add(pos16);
            bloks.Add(p16);
            UzBox.IsEnabled = false;
            Zagr.IsEnabled = false;
            json_set();
            if (resp.Count > 0)
            {
                UzBox.IsEnabled = true;
                Zagr.IsEnabled = true;
                //  UzBox.Items[1] = "Выберите узел";
                for (int c = 0; c < resp.Count; c++)
                    UzBox.Items.Add("Узел: " + (c + 1));
                // UzBox.Items[0] = "Выберите узел";
              //  UzBox.Text = "Выберите узел";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            n_bt.IsEnabled = true;
            s_bt.IsEnabled = true;
            step = 1;
            int i = 0, k = 0;
            int[,] demo_r_matr = new int[,] { {0,4,1,0,2,3},
                                              {4,0,2,3,2,1},
                                              {1,2,0,1,0,1},
                                              {0,3,1,0,3,1},
                                              {2,1,0,3,0,1},
                                              {3,2,1,1,1,0}
                                      };
            int[,] demo_uzel_matr = new int[,] { { 1, 3 }, { 7, 4 }, { 2, 5 } };
            int[] demo_pol_mas = { 1, 2, 3, 4, 5, 7, 0, 0, 0, 0, 0 };
            for (i = 0, k = 0; i < demo_pol_mas.Length; i++)
                if (demo_pol_mas[i] > 0)
                    k++;
            int[] mas = new int[k];
            for (i = 0, k = 0; i < demo_pol_mas.Length; i++)
                if (demo_pol_mas[i] > 0)
                { mas[k] = demo_pol_mas[i]; k++; }
            Pol_mas = mas;
            R_matr = demo_r_matr;
            Uzel_matr = U_calc(Pol_mas);
            int L = Pol_mas.Length;
            //   D_matr = new int[16,16];
            D_calc(D_matr, Uzel_matr);
            // filler(demo_Uzel_matr);
            demo_click++;
            filler(R_matr, D_matr, Uzel_matr, Pol_mas); tb.Clear();
        }
        private int worker(int[,] r_matr, int[,] d_matr, int[,] u_matr, int[] pol_mas)
        {
            //int[] pmas = pol_mas;
            //Array.Sort(pmas);
            int Size = r_matr.GetLength(0);
            bool ch_f = false; //флаг обмена
            int fl = 1;
            int c1 = 0, c2 = 0;
            int ch_p1 = 0, ch_p2 = 0, ch_p3 = 0, ch_p4 = 0, ans_max = 0; //переменные для обмена
            int i = 0, j = 0, x = 0; //переменны для циклов
            int ans = 0; //переменная ответа
            char[] sep = { ' ' }; //разделитель для split`а
            string temp_char = ""; //временная строка для буквенной части просчета
            string temp_num = ""; //временная строка для цифровой части просчета
            string res = " "; //строка для результата
            int[] temp_L = new int[u_matr.Length]; //временная переменная для значений L
            for (i = 0; i < u_matr.GetLength(0); i++)
            {
                for (j = 0; j < u_matr.GetLength(1); j++)
                {
                    try
                    {
                        int t = j + 1;
                        for (int k = i; k < u_matr.GetLength(0); k++)
                        {
                            for (int c = t; c < u_matr.GetLength(1); c++)
                            {

                                temp_char = "ΔL[" + u_matr[i, j] + "][" + u_matr[k, c] + "]=";
                                if (u_matr[i, j] != 0 && u_matr[k, c] != 0)
                                {
                                    int w = 0;

                                    for (x = 0; x < Size; x++)
                                    {
                                        c1 = looking(u_matr[i, j], pol_mas);
                                        c2 = looking(u_matr[k, c], pol_mas);
                                        if (x != c1 && x != c2)
                                        {
                                            temp_L[w] = (r_matr[c1, x] - r_matr[c2, x]) * (d_matr[c1, x] - d_matr[c2, x]);
                                            w++;
                                            temp_char = temp_char + "(R" + u_matr[i, j]+"." + pol_mas[x].ToString() + '-' + 'R' + u_matr[k, c]+"." + pol_mas[x].ToString() + ")+(D" + u_matr[i, j]+"." + pol_mas[x].ToString() + "-D" + u_matr[k, c]+"." + pol_mas[x].ToString() + ")+";
                                            temp_num = temp_num + '(' + r_matr[c1, x] + '-' + r_matr[c2, x].ToString() + ")*(" + d_matr[c1, x].ToString() + '-' + d_matr[c2, x].ToString() + ")+";
                                        }
                                    }

                                    for (int z = 0; z < temp_L.Length; z++)
                                    {
                                        ans += temp_L[z];                           
                                    }
                                 
                                    res = ans.ToString();
                                    temp_char = temp_char.TrimEnd(new char[] { '+' });
                                    temp_num = temp_num.TrimEnd(new char[] { '+' });
                                    if (ans > 0)
                                    {
                                        if (fl == 1 || ans > ans_max)
                                        {
                                            //ch_d = c1;
                                            //ch_d1 = c2;
                                            ch_p1 = i;
                                            ch_p2 = j;
                                            ch_p3 = k;
                                            ch_p4 = c;
                                            ch_f = true;
                                            fl = 0;
                                            ans_max = ans;
                                        }
                                    }
                                    tb.Text += (temp_char + '=' + temp_num + '=' + res + Environment.NewLine);
                                    ans = 0;
                                    res = "";
                                    temp_num = "";
                                }
                            }
                            t = 0;
                        }
                    }
                    catch (Exception)
                    {
                        temp_char = "";
                    }
                }
            }
            if (ch_f == true)
            {
                Exchange(ch_p1, ch_p2, ch_p3, ch_p4);
            }
            return ans_max;
        }
        int looking(int a, int[] pol_mas)
        {
            int i, kas = 0;
            int Size = pol_mas.Length;
            for (i = 0; i < Size; i++)
                if (pol_mas[i] == a)
                    kas = i;
            return kas;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tb.Clear();
            for (int i = 0; i < 16; i++)
            { texts[i].Text = ""; }
            text_D.Text = "";
            text_R.Text = "";
            n_bt.IsEnabled = false;
            s_bt.IsEnabled = false;
            UzBox.Items.Clear();
            Zagr.IsEnabled = false;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int i, k;
            k = UzBox.SelectedIndex;
            if (step > 1)
                filler(R_matr, D_matr, Uzel_matr, Pol_mas);
            tb.Text = tb.Text + "Шаг:" + step + Environment.NewLine;
            i = worker(R_matr, D_matr, Uzel_matr, Pol_mas);
            step++;
            if (i <= 0)
            {
                tb.Text += "Конец!";
                tb.ScrollToEnd();
              //  demo_click = 0;
                step = 1;
                n_bt.IsEnabled = false;
                s_bt.IsEnabled = false;
                if (demo_click == 0)
                    json_result();
                demo_click = 0;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            tb.Text = tb.Text + "Шаг:" + step + Environment.NewLine;
            i = worker(R_matr, D_matr, Uzel_matr, Pol_mas);
            while (i > 0)
            {
                step++;
                tb.Text = tb.Text + "Шаг:" + step + Environment.NewLine;
                i = worker(R_matr, D_matr, Uzel_matr, Pol_mas);
            }
            if (i <= 0)
            {
                tb.Text += "Конец!";
                tb.ScrollToEnd();
                d_bt.Content = "Demo";
                filler(R_matr, D_matr, Uzel_matr, Pol_mas);
                n_bt.IsEnabled = false;
                s_bt.IsEnabled = false;
                //demo_click = 0;
                step = 1;
                if (demo_click == 0)
                    json_result();
                demo_click = 0;
            }
        }
        private void filler(int[,] R_matr, int[,] D_matr, int[,] U_matr, int[] Pol_mas)
        {
            int k = 0;
            int i = 0, j = 0;
            string R_string = "", D_string = "";
            string shapka_string = "";
            text_R.Text = "";
            text_D.Text = "";
            for (i = 0; i < U_matr.GetLength(1); i++)
            {
                for (j = 0; j < U_matr.GetLength(0); j++, k++)
                {
                    if (U_matr[j, i] != 0)
                    {
                        texts[k].Visibility = System.Windows.Visibility.Visible;
                        texts[k].Text = "D" + U_matr[j, i].ToString();
                    }
                    else
                    {
                        texts[k].Visibility = System.Windows.Visibility.Visible;
                        texts[k].Text = "";
                    }
                }
            }
            int cont = Pol_mas.Length;
            for (i = 0; i < 16; i++)
            { bloks[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.White); }
            for (i = 0; i < Pol_mas.Length; i++)
            {
                if (Pol_mas[i] != 0)
                {
                    shapka_string = shapka_string + 'D' + Pol_mas[i];
                    if (Pol_mas[i] >= 10)
                        shapka_string += " ";
                    else
                        shapka_string += "  ";
                }
                else
                    cont--;
            }
            text_R.Text = text_R.Text + "    " + shapka_string + Environment.NewLine;
            for (i = 0; i < Pol_mas.Length; i++)
            {
                R_string = 'D' + Pol_mas[i].ToString();
                if (Pol_mas[i] >= 10)
                    R_string += " ";
                else
                    R_string += "  ";
                for (j = 0; j < Pol_mas.Length; j++)
                {
                    R_string = R_string + R_matr[i, j] + "   ";
                }
                text_R.Text = text_R.Text + R_string + Environment.NewLine;
                R_string = "";
            }
            text_D.Text = text_D.Text + "    " + shapka_string + Environment.NewLine;
            for (i = 0; i < Pol_mas.Length; i++)
            {

                D_string = 'D' + Pol_mas[i].ToString();
                if (Pol_mas[i] >= 10)
                    D_string += " ";
                else
                    D_string += "  ";
                for (j = 0; j < Pol_mas.Length; j++) //D_matr.GetLength(1)
                {
                    D_string = D_string + D_matr[i, j] + "   ";
                }
                text_D.Text = text_D.Text + D_string + Environment.NewLine;
                D_string = "";

            }
        }

        private void Base_Click(object sender, RoutedEventArgs e)
        {
            resp.Clear();
            DRP my;
            UzBox.Items.Clear();
            for (int i = 0; i < newp.Count; i++)
            {
                my = new DRP();
                my.Name = newp[i].Name;
                my.Uzel = newp[i].Uzel;
                resp.Add(my);
            }
            UzBox.IsEnabled = true;
            Zagr.IsEnabled = true;
        //    UzBox.Items[-1] = "Выберите узел";
            for (int c = 0; c < resp.Count; c++)
                UzBox.Items.Add("Узел: " + (c + 1));
        }
        private void Exchange(int ch_p1, int ch_p2, int ch_p3, int ch_p4)
        {
            int buf; //буфер
            int Size = R_matr.GetLength(0);
            tb.Text = tb.Text + "Обмениваем " + "D" + Uzel_matr[ch_p1, ch_p2].ToString() + " и " + "D" + Uzel_matr[ch_p3, ch_p4].ToString();
            tb.Text += Environment.NewLine;
            int ch_d = looking(Uzel_matr[ch_p1, ch_p2], Pol_mas);
            int ch_d1 = looking(Uzel_matr[ch_p3, ch_p4], Pol_mas);
            tb.ScrollToEnd();
            for (int i = 0; i < Size; i++)
            {
                buf = D_matr[ch_d, i];
                D_matr[ch_d, i] = D_matr[ch_d1, i];
                D_matr[ch_d1, i] = buf;
            }
            for (int i = 0; i < Size; i++)
            {
                buf = D_matr[i, ch_d];
                D_matr[i, ch_d] = D_matr[i, ch_d1];
                D_matr[i, ch_d1] = buf;
            }

            buf = Uzel_matr[ch_p1, ch_p2];
            Uzel_matr[ch_p1, ch_p2] = Uzel_matr[ch_p3, ch_p4];
            Uzel_matr[ch_p3, ch_p4] = buf;
            //filler(R_matr, D_matr, Uzel_matr, Pol_mas);
            colorize(Uzel_matr[ch_p1, ch_p2], Uzel_matr[ch_p3, ch_p4]);
        }
        private void D_calc(int[,] d_matr, int[,] uz_matr)
        {
            int y, u;
            for (int i = 0; i < uz_matr.GetLength(0); i++)
                for (int j = 0; j < uz_matr.GetLength(1); j++)
                {
                    for (int a = 0; a < uz_matr.GetLength(0); a++)
                        for (int b = 0; b < uz_matr.GetLength(1); b++)
                        {
                            if (uz_matr[a, b] != 0 && uz_matr[i, j] != 0)
                            {
                                y = looking(uz_matr[a, b], Pol_mas);
                                u = looking(uz_matr[i, j], Pol_mas);
                                d_matr[y, u] = Math.Abs(Math.Abs(a - i) + Math.Abs(b - j));
                                d_matr[u, y] = Math.Abs(Math.Abs(a - i) + Math.Abs(b - j));
                            }
                        }
                }
        }
        void looking_drp(int a, int[,] drp_matr, out int y, out int u)
        {
            y = -1;
            u = -1;
            int i, j, kas = 0;
            for (i = 0; i < drp_matr.GetLength(1); i++)
                for (j = 0; j < drp_matr.GetLength(0); j++)
                    if (drp_matr[j, i] == a)
                    { y = j; u = i; }
        }
        private int[,] U_calc(int[] po_mas)
        {
            //  int k = 0;
            int[,] Uz_matr = new int[4, 4];
            for (int j = 0, u = 0; j < 4; j++)
                for (int i = 0; i < 4; i++, u++)
                {
                    // if(Pol_mas[u]!=0)
                    if (u < po_mas.Length)
                        Uz_matr[i, j] = po_mas[u];
                    else
                    {
                        Uz_matr[i, j] = 0;
                    }
                }
            return Uz_matr;
        }
        private void colorize(int d1, int d2)
        {
            int a1, a2, b1, b2;
            looking_drp(d1, Uzel_matr, out a1, out a2);
            looking_drp(d2, Uzel_matr, out b1, out b2);
            a1 = a1 + a2 * 4;
            b1 = b1 + b2 * 4;
            bloks[a1].Fill = bloks[b1].Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
        }
        private void json_set()
        {
            newr.Clear();
            newp.Clear();
            U_R strok;
            int[] hr;
            for (int k = 0; k < 12; k++)
            {
                strok = new U_R();
                hr = new int[12];
                // strok.Name = k + 1;
                //for (int z = 0; z < 12; z++)
                //    hr[z] = r_matr_json[k, z];
                strok.R_str = r_matr_json;
                newr.Add(strok);
            }
            //prod.Uzel = new int[] { 1, 9, 10, 4, 0, 0, 6, 5,0,0,0 };
            //prod.Name = 1;
            //prod1.Uzel = new int[] { 7, 2, 3, 13, 12, 11, 8, 0, 14 };
            //prod.Name = 2;
            string json = JsonConvert.SerializeObject(prod);
            string json1 = JsonConvert.SerializeObject(prod1);
            StreamWriter sw = new StreamWriter("InputR_mat.json");
            //sw.WriteLine(json);
            //sw.WriteLine(json1);
            //sw.Close();
            //sw = new StreamWriter("testR.json");
            //for (int k = 0; k < 12; k++)
            //{
            //    strok = newr[k];
                string rmat = JsonConvert.SerializeObject(r_matr_json);
                sw.WriteLine(rmat);
            //}
            sw.Close();
            newr.Clear();
            StreamReader sr = new StreamReader("InputUz.json");
            for (int i = 0; !sr.EndOfStream; i++)
            {

                json1 = sr.ReadLine();
                DRP p = JsonConvert.DeserializeObject<DRP>(json1);
                newp.Add(p);
            }
            sr.Close();
            sr = new StreamReader("InputR_mat.json");
            for (int i = 0; !sr.EndOfStream; i++)
            {

                json1 = sr.ReadLine();
                int[,] pr = JsonConvert.DeserializeObject<int[,]>(json1);
                // newr.Add(pr);
                r_matr_json = pr;             
            }
            sr.Close();
            DRP my;
            for (int i = 0; i < newp.Count; i++)
            {
                my = new DRP();
                my.Name = newp[i].Name;
                my.Uzel = newp[i].Uzel;              
                resp.Add(my);
            }
        }
        public class DRP
        {
            public int[] Uzel { get; set; }
            public int Name { get; set; }
        }
        public class U_R
        {
            public int[,] R_str { get; set; }
         //   public int Name { get; set; }
        }
        private void json_result()
        {
            DRP strok;
            int o = -1;
            o = UzBox.SelectedIndex;
            int[] r = new int[16];
            for (int j = 0, k = 0; j < Uzel_matr.GetLength(1); j++)
                for (int i = 0; i < Uzel_matr.GetLength(0); i++, k++)
                { r[k] = Uzel_matr[i, j]; }
            //newp[0].Uzel[2] = 0;
            resp[o].Uzel = r;
            StreamWriter sw = new StreamWriter("InputUz.json");
            for (int k = 0; k<resp.Count; k++)
            {
                strok = resp[k];
                string rmat = JsonConvert.SerializeObject(strok);
                sw.WriteLine(rmat);
            }
            sw.Close();

        }

        private void Zagr_Click(object sender, RoutedEventArgs e)
        {
            int i = -1;
            i = UzBox.SelectedIndex;
            if(i!=-1)
            {
                int[] p = resp[i].Uzel;
                i = 0;
                int  k = 0, s = 0;
                int[] iw = new int[r_matr_json.GetLength(0)];
                for (i = 0; i < p.Length; i++)
                    if (p[i] != 0)
                        k++;
                int[] posit_mas = new int[k];
                k = 0;
                for (i = 0; i < p.Length; i++)
                    if (p[i] != 0)
                    { posit_mas[k] = p[i]; k++; }
                Array.Sort(posit_mas);
                Pol_mas = posit_mas;
                k = posit_mas.Length;
                R_matr = new int[k, k];
                for (i = 0; i < posit_mas.Length; i++)
                    for (k = 0,s=0; k < r_matr_json.GetLength(0); k++)
                        if (k+1 == posit_mas[i])
                        {
                            for (int u = 0; u < r_matr_json.GetLength(0); u++)
                                iw[u] = r_matr_json[k, u];
                           // iw = newr[k].R_str; s = 0;
                            for (int z = 0; z < iw.Length; z++)
                                for (int y = 0; y < posit_mas.Length; y++)
                                    if (z == posit_mas[y] - 1)
                                    { R_matr[i, s] = iw[z]; s++; }
                        }
                n_bt.IsEnabled = true;
                s_bt.IsEnabled = true;
                Base.IsEnabled = true;
                Uzel_matr = U_calc(p);
                D_calc(D_matr, Uzel_matr);
                filler(R_matr, D_matr, Uzel_matr, Pol_mas); tb.Clear();
            }
        }

        private void UzBox_Initialized(object sender, EventArgs e)
        {
        }
    }
}
