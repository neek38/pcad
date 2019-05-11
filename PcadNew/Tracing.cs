using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PcadNew
{
    public class Tracing
    {
        public DataTable myDT; // datatable of mainGrid
        public Wave mainWave { get; set; } 
        public List<Wave> bufList { get; set; }
        public int counter { get; set; }
        public bool Point_B_is_Reached { get; set; }
        public Tracing(bool auto,DataTable dt,int counter,ref List<Wave> waves)
        {
            myDT = new DataTable();
            myDT = dt;
            mainWave = null;
            this.counter = counter;
            Point_B_is_Reached = false;
            if (auto)
            {
                // Auto tracint
                bufList = new List<Wave>(0);
                Drow_drp(false);
                while (!Point_B_is_Reached)
                    Build();
                while (mainWave.Parent != null)
                {
                    myDT.Rows[mainWave.i][mainWave.j] = "X";
                    mainWave = mainWave.Parent;
                }
                EndTrace();
            }
            else
            {
                bufList = waves;
                if (counter == 1)
                {
                    Drow_drp(false);
                }
                if (!Point_B_is_Reached)
                {
                    if (!Build())
                    {
                        waves = bufList;
                        return;
                    }
                }
                while (mainWave.Parent != null)
                {
                    myDT.Rows[mainWave.i][mainWave.j] = "X";
                    mainWave = mainWave.Parent;
                }
                EndTrace();
            }
        }
        public void Drow_drp(bool mode)
        {
            bool point_A_was_found = false;
            int rowA = -1, colA = -1;
            find_point_a(ref rowA, ref colA, ref point_A_was_found, 'A');
            if (point_A_was_found)
            {
                mainWave = new Wave(rowA, colA, null);
                bufList.Add(mainWave);
            }
            else
            {
                MessageBox.Show("Точка А не была найдена!");
                return;
            }
        }
        public void find_point_a(ref int rowA, ref int colA, ref bool pointA_was_found, char letter)
        {
            for (int i = 0; i < myDT.Rows.Count; i++)
            {
                for (int j = 0; j < myDT.Columns.Count; j++)
                {
                    if (myDT.Rows[i][j].ToString() == letter.ToString())
                    {
                        rowA = i;
                        colA = j;
                        pointA_was_found = true;
                        break;
                    }
                }
            }
        }
        bool Build() // True если достигли точки Б, иначе False
        {
            List<Wave> inBufList = bufList; //Лист дочерних точек, созданных в предыдущем шаге
            bufList = new List<Wave>(0);
            foreach (var temp in inBufList)
            {
                try
                {
                    //Down
                    if (myDT.Rows[temp.i - 1][temp.j].ToString() == "B")
                    {
                        //Toчка Б достугнута
                        Point_B_is_Reached = true;
                        mainWave = temp;
                        return true;
                    }
                    if (myDT.Rows[temp.i - 1][temp.j].ToString() == "")
                    {
                        temp.myWave.Add(new Wave(temp.i - 1, temp.j, temp));
                        bufList.Add(temp.myWave.Last());
                        myDT.Rows[temp.i - 1][temp.j] = counter % 3;
                    }
                }
                catch (Exception exc) { };

                try
                {
                    //Right
                    if (myDT.Rows[temp.i][temp.j + 1].ToString() == "B")
                    {
                        Point_B_is_Reached = true;
                        mainWave = temp;
                        return true;
                    }
                    if (myDT.Rows[temp.i][temp.j + 1].ToString() == "")
                    {
                        temp.myWave.Add(new Wave(temp.i, temp.j + 1, temp));
                        bufList.Add(temp.myWave.Last());
                        myDT.Rows[temp.i][temp.j + 1] = counter % 3;
                    }
                }
                catch (Exception exc) { };

                try
                {
                    //Up
                    if (myDT.Rows[temp.i + 1][temp.j].ToString() == "B")
                    {
                        Point_B_is_Reached = true;
                        mainWave = temp;
                        return true;
                    }
                    if (myDT.Rows[temp.i + 1][temp.j].ToString() == "")
                    {
                        temp.myWave.Add(new Wave(temp.i + 1, temp.j, temp));
                        bufList.Add(temp.myWave.Last());
                        myDT.Rows[temp.i + 1][temp.j] = counter % 3;
                    }
                }
                catch (Exception exc) { };

                try
                {
                    //Left
                    if (myDT.Rows[temp.i][temp.j - 1].ToString() == "B")
                    {
                        Point_B_is_Reached = true;
                        mainWave = temp;
                        return true;
                    }
                    if (myDT.Rows[temp.i][temp.j - 1].ToString() == "")
                    {
                        temp.myWave.Add(new Wave(temp.i, temp.j - 1, temp));
                        bufList.Add(temp.myWave.Last());
                        myDT.Rows[temp.i][temp.j - 1] = counter % 3;
                    }
                }
                catch (Exception exc) { };
            }
            counter++;
            return false;
        }
        void EndTrace()
        {
            MessageBox.Show("Трассировка окончена", "", MessageBoxButton.OK);
        }
    }
    public class Wave
    {
        public int i { get; set; }
        public int j { get; set; }
        public List<Wave> myWave = new List<Wave>(0);
        public Wave Parent;
        public Wave() { }
        public Wave(int i, int j, Wave Parent)
        {
            this.i = i;
            this.j = j;
            this.Parent = Parent;
        }
    }
}
