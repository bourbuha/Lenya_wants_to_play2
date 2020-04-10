using System;
using System.Threading;
using System.Drawing;

namespace Lenya_wants_to_play
{
    class Drop
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsAlive {get { return t != null && t.IsAlive; }}
        private int width, heigth;
        private bool stop = false;
        private Thread t = null;
        private int dy;
        private int dx;
        private static Random rand = null;
        private static Random rand2 = null;

        public Drop(Rectangle r)
        {
            Update(r);
            if (rand == null) rand = new Random();
            X = rand.Next(-r.Width, r.Width * 2);
            Y = 0;
            if (rand2 == null) rand2 = new Random();
            dy = rand2.Next(3, 18);
            dx = Program.trackBar1_Value;
        }

        private void Move()
        {
            while (!stop)
            {
                Thread.Sleep(100);
                Y += dy;
                X += dx;
                if (Y > width * 2)
                {
                    stop = true;
                }
            }
        }
        public void Start()
        {
            if (t == null || !t.IsAlive)
            {
                stop = false;
                ThreadStart th = new ThreadStart(Move);
                t = new Thread(th);
                t.Start();
            }
        }
        public void Stop(){stop = true;}

        public void Update(Rectangle r)
        {
            width = r.Width;
            heigth = r.Height;
        }
        public void Wind()
        {
            dx = Program.trackBar1_Value;
        }
    }
}