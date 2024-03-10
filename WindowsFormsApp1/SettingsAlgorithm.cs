using System;
using System.Windows.Forms;

namespace KanalTracer
{
    public partial class SettingsAlgorithm : Form
    {
        public SettingsAlgorithm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Hide();

        }
    }
}
