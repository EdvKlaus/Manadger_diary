using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace managers_diary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string task = "Задание не выбрано";
        private void Form1_Load(object sender, EventArgs e)
        {
            //Заполнение таблицы для выбора задания
            foreach (var line in File.ReadAllLines(@"D:\ComBox1.txt"))
            {
                var array = line.Split('\n');
                dataGridView1.Rows.Add(array);
            }

            //Заполнение ComboBox-а отвечающего за выбор времени выполнения
            StreamReader dataCB = new StreamReader(@"D:\ComBox2.txt");
            string xq = dataCB.ReadToEnd();
            string[] yq = xq.Split('\n');
            foreach (string sq in yq)
            {
                comboBox2.Items.Add(sq);
            }
            
        }

        //Метод отвечающий за создание файла
        private void generate_file_button_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) {
                MessageBox.Show("Выберите время на выполнение", "Ошибка");
                
            }
            else
            {
                //Создание сообщения для записи в файл
                DateTime dateTime = DateTime.Now;

                string fileStr = "Текущая дата: " + dateTime.ToString() + "\n" + "Задание: " + task + "\n" + "Время на выполнение: " + comboBox2.SelectedItem.ToString();

                saveFileDialog1.FileName = @"exercise.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) //Открытие диалогового окна для выбора пути сохранения файла
                {
                    try
                    {//Запись данных в файл
                        var writer = new StreamWriter(
                        saveFileDialog1.FileName, false, Encoding.GetEncoding(1251));

                        writer.Write(fileStr);
                        writer.Close();
                    }
                    catch (Exception Ситуация)
                    {
                        MessageBox.Show(Ситуация.Message,
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //Определение выбранной задачи
        {
            try
            {
                task = Convert.ToString(dataGridView1[0, e.RowIndex].Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
