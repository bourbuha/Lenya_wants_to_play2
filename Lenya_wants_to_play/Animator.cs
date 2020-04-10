using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Drawing2D;


namespace Lenya_wants_to_play
{
    class Animator
    {
        public int ct=0,cnt=0;
        private Graphics m;
        private int width, heigth;
        private List<Drop> drops = new List<Drop>();
        private Thread t;
        private bool stop = false;
        private BufferedGraphics bg;
        public Animator(Graphics g, Rectangle r){Update(g, r);}
        public void Update(Graphics g, Rectangle r)
        {
            m = g;
            width = r.Width;
            heigth = r.Height;
            bg = BufferedGraphicsManager.Current.Allocate(m, new Rectangle(0, 0, width, heigth));
            Monitor.Enter(drops);
            foreach (var dixd in drops){dixd.Update(r);}
            Monitor.Exit(drops);
        }
        private void Animate()
        {
            while (!stop)
            {
                Graphics g = bg.Graphics;
                g.Clear(Color.White);
                Monitor.Enter(drops);
                ct = drops.Count;
                for(int i = 0; i < ct; i++)
                {
                    if (!drops[i].IsAlive) { drops.Remove(drops[i]); i--;ct--; cnt++}
                }
                foreach (var dixd in drops)
                {
                    var drop = new GraphicsPath();
                    drop.StartFigure();
                    drop.AddLine(dixd.X, dixd.Y, dixd.X + 3, dixd.Y + 10);
                    drop.AddArc(dixd.X - 3, dixd.Y + 10, 6, 5, 0, 180);
                    drop.AddLine(dixd.X - 3, dixd.Y + 10, dixd.X, dixd.Y);
                    drop.CloseFigure();
                    Brush br = new SolidBrush(Color.Blue);
                    g.FillPath(br, drop);
                    Pen p = new Pen(Color.FromArgb(190, 255, 255), 1);
                    g.DrawPath(p, drop);
                }
                Monitor.Exit(drops);
                try{bg.Render();}
                catch (Exception e) {}
                Thread.Sleep(30);
            }
        }
        public void Start()
        {
            if (t == null || !t.IsAlive)
            {
                ThreadStart th = new ThreadStart(Animate);
                t = new Thread(th);
                t.Start();
            }
            var rect = new Rectangle(0, 0, width, heigth);
            Drop dixd = new Drop(rect);
            dixd.Start();
            Monitor.Enter(drops);
            drops.Add(dixd);
            Monitor.Exit(drops);
        }
        public void Stop()
        {
            stop = true;
            Monitor.Enter(drops);
            foreach (var d in drops){d.Stop();}
            drops.Clear();
            Monitor.Exit(drops);
        }
        public void Wind()
        {
            Monitor.Enter(drops);
            foreach (var d in drops){d.Wind();}
            Monitor.Exit(drops);
        }
    }
}