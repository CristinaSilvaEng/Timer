using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer.Presenter;

namespace Timer.View
{
    public partial class MainWindow : Form
    {
        MainPresenter presenter;

        public MainWindow()
        {
            InitializeComponent();

            presenter = new MainPresenter();
        }

        private void uxBtnSetTimer_Click(object sender, EventArgs e)
        {
            int hours = Int32.Parse(uxLblHours.Text);
            int minutes = Int32.Parse(uxLblMinutes.Text);
            int seconds = Int32.Parse(uxLblSeconds.Text);
        }
    }
}
