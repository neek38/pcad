﻿using System;
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
        List<TextBlock> TB = new List<TextBlock>();   //список текстбоксов.
        public WindowShaffera()
        {
            InitializeComponent();
            TB.Add(new TextBlock());           //нулевой элемент пустой,он не используется.
            TB.Add(TB1); TB.Add(TB2); TB.Add(TB3); TB.Add(TB4); TB.Add(TB5); TB.Add(TB6); TB.Add(TB7); TB.Add(TB8);
            TB.Add(TB9); TB.Add(TB10); TB.Add(TB11); TB.Add(TB12); TB.Add(TB13); TB.Add(TB14); TB.Add(TB15); TB.Add(TB16);
        }
        public event EventHandler WindowShaffera_Closed;  //передаёт в главное окно событие о закрытии данного окна
        private void Window_Closed(object sender, EventArgs e)
        {
            WindowShaffera_Closed(this, EventArgs.Empty);

        }
        int[,] R;
        int[] pos;
        int L12, L23, L34;
        bool end_po_stolbcam, end_po_strokam, obmen = false;
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
            pos = new int[17] { 0, 2, 4, 1, 3, 0, 6, 7, 0, 8, 0, 0, 5, 0, 0, 0, 0 }; //массив позиций на одну больше,что бы счёт вёлся с единицы.
            //int row = R.GetLength(0);
            //int col = R.GetLength(1);
            DRP();
            Step_Demo.IsEnabled = true; DoAll_Demo.IsEnabled = true;
            Step_Work.IsEnabled = false; DoAll_Work.IsEnabled = false;
            end_po_stolbcam = end_po_strokam = false;
            MatrixToTextbox(R);
            TextBox1.Text += "НАЧАЛО:  \n"; TextBox1.ScrollToEnd();
            Start_Demo.IsEnabled = false;
        }
        private void Do_All(object sender, RoutedEventArgs e)
        {
            if (end_po_stolbcam == true && end_po_strokam == true)
            {
                MessageBox.Show("Алгоритм закончен!");
            }
            else
            {
                while (end_po_stolbcam == false)   //пока не закончим по столбцам
                    Shaffera_po_stolbcam(R, pos);
                while (end_po_strokam == false)    //пока не закончим по строкам
                    Shaffera_po_strocam(R, pos);
            }
        }
        private void Step(object sender, RoutedEventArgs e)
        {
            if (end_po_stolbcam == true && end_po_strokam == true)
            {
                MessageBox.Show("Алгоритм закончен!");
            }
            else
            {
                Start_Work.IsEnabled = false;
                if (end_po_stolbcam == false)           //если по столбцам не закончилось,делаем по столбцам
                {
                    Shaffera_po_stolbcam(R, pos);
                    return;
                }
                if (end_po_strokam == false && end_po_stolbcam == true)  //ждём когда закончатся столбцы и делаем по строкам
                    Shaffera_po_strocam(R, pos);
            }
        }

        private void Work_Start(object sender, RoutedEventArgs e)
        {

        }
        private void Shaffera_po_stolbcam(int[,] R, int[] pos)
        {
            if (pos[9] != 0 || pos[10] != 0 || pos[11] != 0 || pos[12] != 0 || pos[13] != 0 || pos[14] != 0 || pos[15] != 0 || pos[16] != 0)  //если ложь,значит столбцов меньше трёх
            {
                Stolbci.Visibility = Visibility.Visible; Stroki.Visibility = Visibility.Hidden;
                //----------------------Считаем связи между столбцами------------------------------------------------ 
                int[] rCol = new int[6];   //[0]=12,[1]=13,[2]=14,[3]=23,[4]=24,[5]=34
                string[] formula1 = new string[6], value1 = new string[6];
                int buf; string bufstr;
                int fl = 0; //признак по столбцам
                if (obmen == false)   //шаг для обмена
                {
                    TextBox1.Text += "Объединяем элементы по столбцам и считаем связи между столбцами:\n"; TextBox1.ScrollToEnd();
                    for (int i = 1; i < 5; i++)
                        for (int j = 1; j < 5; j++)
                        {
                            rCol[0] += R[pos[i], pos[j + 4]];
                            rCol[1] += R[pos[i], pos[j + 8]];
                            rCol[2] += R[pos[i], pos[j + 12]];
                            rCol[3] += R[pos[i + 4], pos[j + 8]];
                            rCol[4] += R[pos[i + 4], pos[j + 12]];
                            rCol[5] += R[pos[i + 8], pos[j + 12]];
                        }
                    Formula1(R, pos, rCol, formula1, value1, fl);    //текстовка 
                    //----------------------Считаем дельтаL------------------------------------------------   
                    TextBox1.Text += "Считаем дельта L:\n"; TextBox1.ScrollToEnd();
                    L12 = (rCol[1] - rCol[3]) + (rCol[2] - rCol[4]);
                    L23 = -(rCol[0] - rCol[1]) + (rCol[4] - rCol[5]);
                    L34 = -(rCol[1] - rCol[2]) - (rCol[3] - rCol[5]);
                    TextBox1.Text += "L12=(T13-T23)+(T14-T24)=(" + rCol[1].ToString() + '-' + rCol[3].ToString() + ")+(" + rCol[2].ToString() + '-' + rCol[4].ToString() + ")=" + L12.ToString() + "\n";
                    TextBox1.Text += "L23=-(T12-T13)+(T24-T34)=-(" + rCol[0].ToString() + '-' + rCol[1].ToString() + ")+(" + rCol[4].ToString() + '-' + rCol[5].ToString() + ")=" + L23.ToString() + "\n";
                    TextBox1.Text += "L34=-(T13-T14)-(T23-T24)=(-" + rCol[0].ToString() + '-' + rCol[2].ToString() + ")-(" + rCol[3].ToString() + '-' + rCol[4].ToString() + ")=" + L34.ToString() + "\n"; TextBox1.ScrollToEnd();
                }
                //----------------------Ищем максимальный среди положительных------------------------------- 
                if (L12 > 0 && L12 > L23 && L12 > L34)
                {
                    if (obmen == false)
                    {
                        TextBox1.Text += "Максимально положительный - L12,меняем местами 1 и 2 столбцы\n\n"; TextBox1.ScrollToEnd();
                        obmen = true;
                    }
                    else
                    {
                        for (int i = 1; i < 5; i++)
                        {
                            buf = pos[i]; pos[i] = pos[i + 4]; pos[i + 4] = buf;                           //меняем местами в массиве позиций
                            bufstr = TB[i].Text; TB[i].Text = TB[i + 4].Text; TB[i + 4].Text = bufstr;     //меняем визуал
                        }
                        obmen = false;
                        TextBox1.Text += "Следующий шаг:\n";
                    }
                }
                if (L23 > 0 && L23 > L12 && L23 > L34)
                {
                    if (obmen == false)
                    {
                        TextBox1.Text += "Максимально положительный - L23,меняем местами 2 и 3 столбцы\n"; TextBox1.ScrollToEnd();
                        obmen = true;
                    }
                    else
                    {
                        for (int i = 5; i < 9; i++)
                        {
                            buf = pos[i]; pos[i] = pos[i + 4]; pos[i + 4] = buf;                           //меняем местами в массиве позиций
                            bufstr = TB[i].Text; TB[i].Text = TB[i + 4].Text; TB[i + 4].Text = bufstr;     //меняем визуал
                        }
                        obmen = false;
                        TextBox1.Text += "Следующий шаг:\n";
                    }
                }
                if (L34 > 0 && L34 > L12 && L34 > L23)
                {
                    if (obmen == false)
                    {
                        TextBox1.Text += "Максимально положительный - L34,меняем местами 3 и 4 столбцы\n"; TextBox1.ScrollToEnd();
                        obmen = true;
                    }
                    else
                    {
                        for (int i = 9; i < 13; i++)
                        {
                            buf = pos[i]; pos[i] = pos[i + 4]; pos[i + 4] = buf;                           //меняем местами в массиве позиций
                            bufstr = TB[i].Text; TB[i].Text = TB[i + 4].Text; TB[i + 4].Text = bufstr;     //меняем визуал
                        }
                        obmen = false;
                        TextBox1.Text += "Следующий шаг:\n";
                    }
                }
                if (L12 <= 0 && L23 <= 0 && L34 <= 0)  //Если нет положительных L,значит конец по столбцам
                {
                    end_po_stolbcam = true;
                    TextBox1.Text += "Положительных L больше нет,переходим к строкам \n\n\n"; TextBox1.ScrollToEnd();
                }
            }
        }
        private void Shaffera_po_strocam(int[,] R, int[] pos)
        {
            if (pos[3] != 0 || pos[4] != 0 || pos[7] != 0 || pos[8] != 0 || pos[11] != 0 || pos[12] != 0 || pos[15] != 0 || pos[16] != 0) //если ложь,значит строк меньше трёх
            {
                Stroki.Visibility = Visibility.Visible; Stolbci.Visibility = Visibility.Hidden;
                //----------------------Считаем связи cтроками------------------------------------------------   
                int[] rRow = new int[6];   //[0]=12,[1]=13,[2]=14,[3]=23,[4]=24,[5]=34
                string[] formula1 = new string[6], value1 = new string[6];
                int buf; string bufstr;
                int fl = 1; //признак по строкам
                if (obmen == false)   //шаг для обмена
                {
                    TextBox1.Text += "Объединяем элементы по строкам и считаем связи между строками:\n"; TextBox1.ScrollToEnd();
                    for (int i = 1; i < 17; i += 4)
                        for (int j = 1; j < 17; j += 4)
                        {
                            rRow[0] += R[pos[i], pos[j + 1]];
                            rRow[1] += R[pos[i], pos[j + 2]];
                            rRow[2] += R[pos[i], pos[j + 3]];
                            rRow[3] += R[pos[i + 1], pos[j + 2]];
                            rRow[4] += R[pos[i + 1], pos[j + 3]];
                            rRow[5] += R[pos[i + 2], pos[j + 3]];
                        }
                    Formula1(R, pos, rRow, formula1, value1, fl);    //текстовка 
                    //----------------------Считаем дельтаL------------------------------------------------   
                    TextBox1.Text += "Считаем дельта L:\n"; TextBox1.ScrollToEnd();
                    L12 = (rRow[1] - rRow[3]) + (rRow[2] - rRow[4]);
                    L23 = -(rRow[0] - rRow[1]) + (rRow[4] - rRow[5]);
                    L34 = -(rRow[1] - rRow[2]) - (rRow[4] - rRow[4]);
                    TextBox1.Text += "L12=(T13-T23)+(T14-T24)=(" + rRow[1].ToString() + '-' + rRow[3].ToString() + ")+(" + rRow[2].ToString() + '-' + rRow[4].ToString() + ")=" + L12.ToString() + "\n";
                    TextBox1.Text += "L23=-(T12-T13)+(T24-T34)=-(" + rRow[0].ToString() + '-' + rRow[1].ToString() + ")+(" + rRow[4].ToString() + '-' + rRow[5].ToString() + ")=" + L23.ToString() + "\n";
                    TextBox1.Text += "L34=-(T13-T14)-(T23-T24)=(-" + rRow[0].ToString() + '-' + rRow[2].ToString() + ")-(" + rRow[3].ToString() + '-' + rRow[4].ToString() + ")=" + L34.ToString() + "\n"; TextBox1.ScrollToEnd();
                }
                //----------------------Ищем максимальный среди положительных-------------------------------
                if (L12 > 0 && L12 > L23 && L12 > L34)
                {
                    if (obmen == false)
                    {
                        TextBox1.Text += "Максимально положительный - L12,меняем местами 1 и 2 строки\n\n"; TextBox1.ScrollToEnd();
                        obmen = true;
                    }
                    else
                    {
                        for (int i = 1; i < 14; i += 4)
                        {
                            buf = pos[i]; pos[i] = pos[i + 1]; pos[i + 1] = buf;                           //меняем местами в массиве позиций
                            bufstr = TB[i].Text; TB[i].Text = TB[i + 1].Text; TB[i + 1].Text = bufstr;     //меняем визуал
                        }
                        obmen = false;
                        TextBox1.Text += "Следующий шаг:\n";
                    }
                }
                if (L23 > 0 && L23 > L12 && L23 > L34)
                {
                    if (obmen == false)
                    {
                        TextBox1.Text += "Максимально положительный - L23,меняем местами 2 и 3 строки\n\n"; TextBox1.ScrollToEnd();
                        obmen = true;
                    }
                    else
                    {
                        for (int i = 2; i < 15; i += 4)
                        {
                            buf = pos[i]; pos[i] = pos[i + 1]; pos[i + 1] = buf;                           //меняем местами в массиве позиций
                            bufstr = TB[i].Text; TB[i].Text = TB[i + 1].Text; TB[i + 1].Text = bufstr;     //меняем визуал
                        }
                        obmen = false;
                        TextBox1.Text += "Следующий шаг:\n";
                    }
                }
                if (L34 > 0 && L34 > L12 && L34 > L23)
                {
                    if (obmen == false)
                    {
                        obmen = true;
                        TextBox1.Text += "Максимально положительный - L34,меняем местами 3 и 4 строки\n\n"; TextBox1.ScrollToEnd();
                    }
                    else
                    {
                        for (int i = 3; i < 16; i += 4)
                        {
                            buf = pos[i]; pos[i] = pos[i + 1]; pos[i + 1] = buf;                           //меняем местами в массиве позиций
                            bufstr = TB[i].Text; TB[i].Text = TB[i + 1].Text; TB[i + 1].Text = bufstr;     //меняем визуал
                        }
                        obmen = false;
                        TextBox1.Text += "Следующий шаг:\n";
                    }
                }
                if (L12 <= 0 && L23 <= 0 && L34 <= 0)   //Если нет положительных L,значит конец по строкам
                {
                    end_po_strokam = true;
                    Start_Demo.IsEnabled = true;
                    Start_Work.IsEnabled = true;
                    TextBox1.Text += "Положительных L больше нет \nАлгоритм закончен!\n\n"; TextBox1.ScrollToEnd();
                }
            }
        }
        private void Formula1(int[,] R, int[] pos, int[] T, string[] formula, string[] value, int fl)
        {
            formula[0] = "T12="; formula[1] = "T13="; formula[2] = "T14="; formula[3] = "T23="; formula[4] = "T24="; formula[5] = "T34=";
            value[0] = value[1] = value[2] = value[3] = value[4] = value[5] = "";
            //----------------------Формируем формулу и значения-------------------------------
            if (fl == 0)      //считаем для столбцов
            {
                for (int i = 1; i < 5; i++)
                    for (int j = 1; j < 5; j++)
                    {
                        if (pos[i] != 0 && pos[j + 4] != 0)      //проверки на наличие элементов в указанных позициях
                        {
                            formula[0] += "r" + pos[i].ToString() + pos[j + 4].ToString() + "+";
                            value[0] += R[pos[i], pos[j + 4]].ToString() + "+";
                        }
                        if (pos[i] != 0 && pos[j + 8] != 0)
                        {
                            formula[1] += "r" + pos[i].ToString() + pos[j + 8].ToString() + "+";
                            value[1] += R[pos[i], pos[j + 8]].ToString() + "+";
                        }
                        if (pos[i] != 0 && pos[j + 12] != 0)
                        {
                            formula[2] += "r" + pos[i].ToString() + pos[j + 12].ToString() + "+";
                            value[2] += R[pos[i], pos[j + 12]].ToString() + "+";
                        }
                        if (pos[i + 4] != 0 && pos[j + 8] != 0)
                        {
                            formula[3] += "r" + pos[i + 4].ToString() + pos[j + 8].ToString() + "+";
                            value[3] += R[pos[i + 4], pos[j + 8]].ToString() + "+";
                        }
                        if (pos[i + 4] != 0 && pos[j + 12] != 0)
                        {
                            formula[4] += "r" + pos[i + 4].ToString() + pos[j + 12].ToString() + "+";
                            value[4] += R[pos[i + 4], pos[j + 12]].ToString() + "+";
                        }
                        if (pos[i + 8] != 0 && pos[j + 12] != 0)
                        {
                            formula[5] += "r" + pos[i + 8].ToString() + pos[j + 12].ToString() + "+";
                            value[5] += R[pos[i + 8], pos[j + 12]].ToString() + "+";
                        }
                    }
            }
            else    //считаем для строк
            {
                for (int i = 1; i < 17; i += 4)
                    for (int j = 1; j < 17; j += 4)
                    {
                        if (pos[i] != 0 && pos[j + 1] != 0)      //проверки на наличие элементов в указанных позициях
                        {
                            formula[0] += "r" + pos[i].ToString() + pos[j + 1].ToString() + "+";
                            value[0] += R[pos[i], pos[j + 1]].ToString() + "+";
                        }
                        if (pos[i] != 0 && pos[j + 2] != 0)
                        {
                            formula[1] += "r" + pos[i].ToString() + pos[j + 2].ToString() + "+";
                            value[1] += R[pos[i], pos[j + 2]].ToString() + "+";
                        }
                        if (pos[i] != 0 && pos[j + 3] != 0)
                        {
                            formula[2] += "r" + pos[i].ToString() + pos[j + 3].ToString() + "+";
                            value[2] += R[pos[i], pos[j + 3]].ToString() + "+";
                        }
                        if (pos[i + 1] != 0 && pos[j + 2] != 0)
                        {
                            formula[3] += "r" + pos[i + 1].ToString() + pos[j + 2].ToString() + "+";
                            value[3] += R[pos[i + 1], pos[j + 2]].ToString() + "+";
                        }
                        if (pos[i + 1] != 0 && pos[j + 3] != 0)
                        {
                            formula[4] += "r" + pos[i + 1].ToString() + pos[j + 3].ToString() + "+";
                            value[4] += R[pos[i + 1], pos[j + 3]].ToString() + "+";
                        }
                        if (pos[i + 2] != 0 && pos[j + 3] != 0)
                        {
                            formula[5] += "r" + pos[i + 2].ToString() + pos[j + 3].ToString() + "+";
                            value[5] += R[pos[i + 2], pos[j + 3]].ToString() + "+";
                        }
                    }
            }
            //----------------------Убираем в конце '+'-------------------------------
            for (int i = 0; i < 6; i++)
            {
                formula[i] = formula[i].Remove(formula[i].Length - 1);
                if (value[i].Length > 0)
                    value[i] = value[i].Remove(value[i].Length - 1);
            }
            //----------------------Вывод формулы-------------------------------
            for (int i = 0; i < 6; i++)
            {
                if (value[i].Length > 0)
                    TextBox1.Text += formula[i] + "=" + value[i] + "=" + T[i] + "\n"; TextBox1.ScrollToEnd();
            }
        }
        private void DRP()
        {
            //----------------------------------Обнуление ДРП---------------------------------------------------------------------------------
            TB1.Text = TB2.Text = TB3.Text = TB4.Text = TB5.Text = TB6.Text = TB7.Text = TB8.Text =
            TB9.Text = TB10.Text = TB11.Text = TB12.Text = TB13.Text = TB14.Text = TB15.Text = TB16.Text = "";
            //----------------------------------Заполнение ДРП---------------------------------------------------------------------------------
            for (int i = 0; i < 16; i++)
            {
                if (pos[i] != 0)
                    TB[i].Text = "D" + pos[i].ToString();
            }
        }
        private void MatrixToTextbox(int[,] R)
        {
            if (R.GetLength(1) < 12) Table.FontSize = 16;   //размер шрифта в зависимости от кол-ва элементов матрицы.
            if (R.GetLength(1) > 12) Table.FontSize = 14;
            if (R.GetLength(1) > 14) Table.FontSize = 12;
            if (R.GetLength(1) > 16) Table.FontSize = 11;
            string probel;
            Table.Text = "    ";
            for (int i = 1; i < R.GetLength(1); i++)        //первая строчка с D-шками
            {
                if (i > 9)
                    probel = " ";
                else
                    probel = "  ";
                Table.Text += "D" + i.ToString() + probel;
            }
            Table.Text += "\n";
            for (int row = 1; row < R.GetLength(0); row++)  //каждая следующая строка
            {
                if (row > 9)
                    probel = " ";
                else
                    probel = "  ";
                Table.Text += "D" + row.ToString() + probel;
                for (int col = 1; col < R.GetLength(1); col++) //каждый элемент строки
                {
                    if (R[row, col] > 9)
                        probel = "  ";
                    else
                        probel = "   ";
                    Table.Text += R[row, col] + probel;
                }
                Table.Text += "\n";
            }
        }
        private void Clear_TextBox1(object sender, RoutedEventArgs e)
        {
            TextBox1.Clear();
        }
    }
}
