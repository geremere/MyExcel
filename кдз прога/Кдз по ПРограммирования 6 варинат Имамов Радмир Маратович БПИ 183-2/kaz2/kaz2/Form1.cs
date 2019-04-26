using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kaz2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ToolStripMenuItem fileItem = new ToolStripMenuItem("File");

            ToolStripMenuItem newItem = new ToolStripMenuItem("Open csv file");
            newItem.Click += button1_Click;
            fileItem.DropDownItems.Add(newItem);

            ToolStripMenuItem saveItem = new ToolStripMenuItem("Save");
            saveItem.Click += button2_Click;
            fileItem.DropDownItems.Add(saveItem);

            ToolStripMenuItem SortArea = new ToolStripMenuItem("Line sorting for area");
            SortArea.Click += SortingArea;
            fileItem.DropDownItems.Add(SortArea);

            ToolStripMenuItem SortDistrict = new ToolStripMenuItem("Line sorting for district");
            SortDistrict.Click += SortingDistrict;
            fileItem.DropDownItems.Add(SortDistrict);

            ToolStripMenuItem SortDistrictInArea = new ToolStripMenuItem("Sort by county");
            SortDistrictInArea.Click += SortingDistrictInArea;
            fileItem.DropDownItems.Add(SortDistrictInArea);

            ToolStripMenuItem Colum = new ToolStripMenuItem("Resize Colums");
            Colum.Click += Resize;
            fileItem.DropDownItems.Add(Colum);

            menuStrip1.Items.Add(fileItem);

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("About program");
            aboutItem.Click += aboutItem_Click;
            menuStrip1.Items.Add(aboutItem);

            DoubleBuffered = true;
        }

        /// <summary>
        /// сортируем округа по количеству в них районов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortingDistrictInArea(object sender, EventArgs e)
        {
            try
            {
                string[][] file = new string[dataGridView1.RowCount - 1][];
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    file[i] = new string[23];
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        file[i][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();           
                    }
                }
                string[][] newfile = new string[dataGridView1.RowCount - 1][];
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    newfile[i] = new string[23];
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        newfile[i][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                string[] test = new string[dataGridView1.ColumnCount];
                for (int i = 0; i < dataGridView1.RowCount - 2; i++)
                {
                    //newfile[i] = file[i];
                    for (int j = i + 1; j < dataGridView1.RowCount - 1; j++)
                    {
                        if (District.CountDistrictInArea(newfile, file[i][5]) <= District.CountDistrictInArea(newfile, file[j][5]))
                        {
                            test = file[i];
                            file[i] = file[j];
                            file[j] = test;
                        }
                    }
                }
                ReadCSVFileForDistrict(file);
                
            }
            catch(Exception)
            {
                MessageBox.Show("Откройте для начала файл", "Exception");
            }

        }

        /// <summary>
        /// ввод в таблицу для сортивке по ройонвм
        /// </summary>
        /// <param name="pathfile"></param>
        private void ReadCSVFileForDistrict(string[][] pathfile)
        {

            try
            {
                for (int i = 0; i < pathfile.GetLength(0); i++)
                {
                    for (int j = 0; j < pathfile[0].Length; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = pathfile[i][j];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception 3");
            }
        }

        /// <summary>
        /// сортировка по строке для районов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortingDistrict(object sender, EventArgs e)
        {
            try
            {
                string line = DialogResultString();
                if (line == null)
                    throw new FormatException();
                int count = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[6].Value.ToString().Contains(line))
                    {
                        count++;
                    }
                }

                string[][] file = new string[count][];

                int capacity = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[6].Value.ToString().Contains(line))
                    {
                        file[capacity] = new string[23];
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            file[capacity][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        capacity++;
                    }
                }
                ReadLNCSVFile(file);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Раскрывает все столбцы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        new void Resize(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
        }

        /// <summary>
        /// все виды сортировок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortingArea(object sender, EventArgs e)
        {
            try
            {
                string line = DialogResultString();
                if (line == null)
                    throw new FormatException();
                int count = 0;
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    if(dataGridView1.Rows[i].Cells[5].Value.ToString().Contains(line))
                    {
                        count++;
                    }                 
                }

                string[][] file = new string[count][];

                int capacity = 0;
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[5].Value.ToString().Contains(line))
                    {
                        file[capacity] = new string[23];
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            file[capacity][j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        capacity++;
                    }
                }
                ReadLNCSVFile(file);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Заполнение табицы после ее сортиовки
        /// </summary>
        /// <param name="pathfile"></param>
        private void ReadLNCSVFile(string[][] pathfile)
        {
                      
            try
            {
               
                for (int i = 0; i < pathfile.GetLength(0); i++)
                {
                    for (int j = 0; j < pathfile[0].Length; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = pathfile[i][j];
                    }
                }


                int count = dataGridView1.RowCount - 1 - pathfile.GetLength(0);
                for (int i = 0; i < count; i++)
                {
                    dataGridView1.Rows.RemoveAt(pathfile.GetLength(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception 3");
            }
        }

        /// <summary>
        /// ввод строки для фильтрации
        /// </summary>
        /// <returns></returns>
        private string DialogResultString()
        {
            Form3 testDialog = new Form3();
            string n;
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                n = testDialog.textBox1.Text;
            }
            else
            {
                testDialog.Close();
                return null;
            }
            testDialog.Dispose();
            return n;
        }

        /// <summary>
        /// о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void aboutItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("данное приложение рализует открытие .csv файла \n выполнил студент ВШЭ ФКН ПИ 183-2","описание работы прогмаммы");
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=pv4Raf3Bfsc");
        }

        /// <summary>
        /// парсинг
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string[][] Parsing(string[] file, uint n)
        {
            int[] ar = new int[0];
            string[][] newar= new string[n][];
            for (int i = 0; i < n; i++)
            {
                newar[i] = new string[23];
                string[] arr = new string[23];               
                int count = 0;
                arr[count] = "";
                for (int j = 0; j < file[i].Length; j++)
                {
                    if(file[i][j] == '"')
                    {
                        if(file[i][j+1] == '"' && file[i][j+2]!=';')
                        {
                            arr[count] += '"';
                            j += 2;
                            continue;
                        }
                        else
                        {
                            j++;
                            while(file[i][j]!='"')
                            {
                                arr[count] += file[i][j];
                                j++;
                            }
                            continue;
                        }
                    }
                    if (file[i][j] == ';')
                    {
                        newar[i][count] = arr[count]; 
                        count++;
                        if(count<23)
                            arr[count] = "";
                        continue;
                    }
                        arr[count] += file[i][j];
                }
            }
            return newar;
        }

        /// <summary>
        /// записываем информация в таблицу
        /// </summary>
        /// <param name="pathfile"></param>
        /// <returns></returns>
        private DataTable ReadCSVFile(string[][] pathfile)
        {
            //создаём таблицу
            DataTable dt = new DataTable("Cinemas");
            //создаём колонки
            for (int i = 0; i < pathfile[0].Length; i++)
            {
                DataColumn col = new DataColumn(pathfile[0][i], typeof(String));
                dt.Columns.AddRange(new DataColumn[] { col });
            }
            try
            {
                DataRow dr = null;
                for (int i = 1; i < pathfile.GetLength(0); i++)
                {
                    dr = dt.NewRow();
                    for (int j = 0; j < pathfile[0].Length; j++)
                    {
                        dr[pathfile[0][j]] = pathfile[i][j];
                    }
                    dt.Rows.Add(dr);                       
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception");
            }
            return dt;
        }

        /// <summary>
        /// открыть csv файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Файлы csv|*.csv";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string[] file = File.ReadAllLines(openFileDialog1.FileName);
                    uint n = uint.Parse(ShowMyDialogBox());
                    if (n < 2 || n > file.Length) throw new ArgumentException($"min rows= {2}\nmax rows= {file.Length} ");
                    Cinema[] data = ForClass(Parsing(file , n));
                    dataGridView1.DataSource = ReadCSVFile(Parsing(file, n));
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message,"Wrong in count of rows");
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong","Exception1");
            }

        }

        /// <summary>
        /// Ввод числа строк
        /// </summary>
        /// <returns></returns>
        public string ShowMyDialogBox()
        {
            Form2 testDialog = new Form2();
            string n;
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                 n = testDialog.textBox1.Text;
            }
            else
            {
                testDialog.Close();
                return null;
            }
            testDialog.Dispose();
            return n;
        }

        /// <summary>
        /// заполняем массив классов
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Cinema[] ForClass(string[][] line)
        {
            Cinema[] data=null;
            try
            {
                data = new Cinema[line.GetLength(0)];
                data[1] = new Cinema(new District(line[1][5], line[1][6]), line[1][0], line[1][1], line[1][2], line[1][3], line[1][4],
                    line[1][7], line[1][8], line[1][9], line[1][10], line[1][11], line[1][12],
                    line[1][13], line[1][14], line[1][15], line[1][16], line[1][17], line[1][18], line[1][19],
                    line[1][20], line[1][21], line[1][22]);

                for (int i = 2; i < line.GetLength(0); i++)
                {
                    District distric = new District(line[i][5], line[i][6]);
                    for (int j = 1; j < i; j++)
                    {
                        if (distric.AdmArea == data[j].distr.AdmArea && distric.district == data[j].distr.district)
                        {
                            data[i] = new Cinema(data[j].distr, line[i][0], line[i][1], line[i][2], line[i][3], line[i][4],
                            line[i][7], line[i][8], line[i][9], line[i][10], line[i][11], line[i][12],
                            line[i][13], line[i][14], line[i][15], line[i][16], line[i][17], line[i][18], line[i][19],
                            line[i][20], line[i][21], line[i][22]);
                        }
                        else
                        {
                            data[i] = new Cinema(new District(line[i][5], line[i][6]), line[i][0], line[i][1], line[i][2], line[i][3], line[i][4],
                            line[i][7], line[i][8], line[i][9], line[i][10], line[i][11], line[i][12],
                            line[i][13], line[i][14], line[i][15], line[i][16], line[i][17], line[i][18], line[i][19],
                            line[i][20], line[i][21], line[i][22]);

                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проблемма при заполнении класса Cinema", "Exception");
            }
            return data;

        }

        /// <summary>
        /// сохранение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Файлы csv|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл 
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл 
            File.WriteAllText(filename, NewString(dataGridView1), Encoding.UTF8);
            MessageBox.Show("Файл сохранен");
        }

        /// <summary>
        /// метод создающий строку для сохранения
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <returns></returns>
        private string NewString(DataGridView dataGridView1)
        {
            string res = "";
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                res += dataGridView1.Columns[i].HeaderText + ";";
            }
            res += Environment.NewLine;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1[j, i].Value != null)
                        res += '"' + dataGridView1[j, i].Value.ToString() + '"' + ";";
                    else
                        res += ";";
                }
                res += Environment.NewLine;
            }
            return res;
        }

        /// <summary>
        /// удаление строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = MessageBox.Show
                    (
                    "Delete the row to which the selected cell belongs?",
                    "Message",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question,
                     MessageBoxDefaultButton.Button1
                     );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("This row u cant deleate","Exception");
                    }
                }
            }
        }
    }
}
