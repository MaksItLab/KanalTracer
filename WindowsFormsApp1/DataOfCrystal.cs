using System;
using System.Windows.Forms;

namespace KanalTracer
{
    public partial class DataOfCrystal : Form
    {
        public DataOfCrystal()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\"; // Установка начальной директории для диалога
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Установка фильтра файлов
            openFileDialog1.FilterIndex = 2; // Установка индекса выбранного фильтра

            if (openFileDialog1.ShowDialog() == DialogResult.OK) // Отображение диалогового окна и проверка, что пользователь выбрал файл
            {
                textBox1.Text = openFileDialog1.FileName; // Вывод пути выбранного файла в текстовое поле
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
