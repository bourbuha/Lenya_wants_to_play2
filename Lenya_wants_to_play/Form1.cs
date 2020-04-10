using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lenya_wants_to_play
{
    public partial class Form1 : Form
    {
        Animator a;
        public Form1()
        {
            InitializeComponent();
            a = new Animator(panel1.CreateGraphics(), panel1.ClientRectangle);
            timer1.Start();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Program.trackBar1_Value = trackBar1.Value;
            a.Wind();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            a.Stop();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (a == null) return;
            a.Update(panel1.CreateGraphics(), panel1.ClientRectangle);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            a.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            a.Start();
        }
    }
}
