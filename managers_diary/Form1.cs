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

            foreach (var line in File.ReadAllLines(@"D:\ComBox1.txt"))
            {
                var array = line.Split('\n');
                dataGridView1.Rows.Add(array);
            }

            StreamReader dataCB = new StreamReader(@"D:\ComBox2.txt");
            string xq = dataCB.ReadToEnd();
            string[] yq = xq.Split('\n');
            foreach (string sq in yq)
            {
                comboBox2.Items.Add(sq);
            }
            
        }

        private void generate_file_button_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            string fileStr = "Текущая дата: " + dateTime.ToString() + "\n"+ "Задание: " + task + "\n"+ "Время на выполнение: " + comboBox2.SelectedItem.ToString();

            saveFileDialog1.FileName = @"exercise.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
