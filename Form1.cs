using OnlyEnergySave.Modules.Power;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlyEnergySave
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                while (checkBox1.Checked)
                {
                    label1.Text = $"Напруга: {BatteryStatus.VoltageBattery().ToString()}V";
                    label1.Visible = true;
                    await Task.Delay(5000);
                }
            }
            else
                label1.Visible = false;
        }

        async private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                while (checkBox2.Checked)
                {
                    label2.Text = $"Заряд: {BatteryStatus.PercentBattery().ToString()}%";
                    label2.Visible = true;
                    await Task.Delay(5000);
                }
            }
            else
            {
                label2.Visible = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                TurboBoostManager.SetCpuLimit(99);
                label3.Text = $"Потужність процесора: 99%";
                trackBar1.Value = 99;
                trackBar1.Enabled = true;
            }
            else
            {
                TurboBoostManager.SetCpuLimit(100);
                label3.Text = $"Потужність процесора: 100%";
                trackBar1.Enabled = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Visible) 
            {
                label3.Text = $"Потужність процесора: {trackBar1.Value}%";
            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            TurboBoostManager.SetCpuLimit((uint)trackBar1.Value);
        }
    }
}