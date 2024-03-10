using System;
using System.Windows.Forms;


namespace KanalTracer
{
    public partial class MainWindow : Form
    {
        string state = "null";

        MainWindow shablon;
        ToolStripLabel timeLabel = new ToolStripLabel();
        ToolStripLabel infoLabel = new ToolStripLabel();
        


        private System.Windows.Forms.TextBox textBox1 = new System.Windows.Forms.TextBox();

        public MainWindow()
        {
            if (state == "null")
            {
                
            }
            InitializeComponent();
        }

        public void LoadForm1()
        {
           
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Проект создан";
            statusStrip1.Items.Add(infoLabel);
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help win = new Help();
            win.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram win = new AboutProgram();
            win.Show();
        }

        private void настройкаАлгоритмаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsAlgorithm win = new SettingsAlgorithm();
            win.Show();
        }

        private void запускАлгоритмаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Алгоритм запущен";
            statusStrip1.Items.Add(infoLabel);

        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Отмена последнего действия";
            statusStrip1.Items.Add(infoLabel);
            
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Повтор последнего действия";
            statusStrip1.Items.Add(infoLabel);
            
        }

        private void данныеОКристаллеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataOfCrystal win = new DataOfCrystal();
            win.Show();
        }

        private void данныеОКристаллеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataOfChannels win = new DataOfChannels();
            win.Show();
        }


        private void данныеОСхемеСоединенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataOfSchemeConnection win = new DataOfSchemeConnection();
            win.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\"; // Установка начальной директории для диалога
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Установка фильтра файлов
            openFileDialog1.FilterIndex = 2; // Установка индекса выбранного фильтра

            if (openFileDialog1.ShowDialog() == DialogResult.OK) // Отображение диалогового окна и проверка, что пользователь выбрал файл
            {
                textBox1.Text = openFileDialog1.FileName; // Вывод пути выбранного файла в текстовое поле
            }
            infoLabel.Text = "Проект открыт";
            statusStrip1.Items.Add(infoLabel);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Проект сохранен";
            statusStrip1.Items.Add(infoLabel);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            infoLabel.Text = "Проект сохранен";
            statusStrip1.Items.Add(infoLabel);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Проект удален";
            statusStrip1.Items.Add(infoLabel);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Проект закрыт";
            statusStrip1.Items.Add(infoLabel);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
