using Cosmos.System.Graphics;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Text;
using Cosmos.System;
using Sys = Cosmos.System;

namespace sexOSKernel.UI
{

    public class GUI
    {
        private Canvas canvas;
        private Pen pen;

        private MouseState prevMouseState;
        private UInt32 pX, pY;
        private List<Tuple<Sys.Graphics.Point, Color>> savedPixels;
        private TabBar tabBar;

        public GUI()
        {

            this.canvas = FullScreenCanvas.GetFullScreenCanvas();
            this.canvas.Clear(Color.Black);

            this.pen = new Pen(Color.White);
            this.prevMouseState = MouseState.None;

            this.pX = 3;
            this.pY = 3;
            this.savedPixels = new List<Tuple<Sys.Graphics.Point, Color>>();

            this.tabBar = new TabBar(this.canvas);

            MouseManager.ScreenHeight = (UInt32)this.canvas.Mode.Rows;
            MouseManager.ScreenWidth = (UInt32)this.canvas.Mode.Columns;

        }

        public void handelGUIInputs()
        {
            if (this.pX != MouseManager.X && this.pY != MouseManager.Y)
            {

                if (MouseManager.X < 2 || MouseManager.Y < 2 || MouseManager.X > (MouseManager.ScreenWidth - 2) || MouseManager.Y > (MouseManager.ScreenHeight - 2))
                {
                    return;
                }

                this.pX = MouseManager.X;
                this.pY = MouseManager.Y;

                Sys.Graphics.Point[] points = new Sys.Graphics.Point[]
                {
                new Sys.Graphics.Point((Int32)MouseManager.X,(Int32)MouseManager.Y),
                new Sys.Graphics.Point((Int32)MouseManager.X+1,(Int32)MouseManager.Y),
                new Sys.Graphics.Point((Int32)MouseManager.X-1,(Int32)MouseManager.Y),
                new Sys.Graphics.Point((Int32)MouseManager.X,(Int32)MouseManager.Y+1),
                new Sys.Graphics.Point((Int32)MouseManager.X,(Int32)MouseManager.Y-1)
                };
                foreach (Tuple<Sys.Graphics.Point, Color> pixelData in this.savedPixels)
                {
                    this.canvas.DrawPoint(new Pen(pixelData.Item2), pixelData.Item1);
                }

                this.savedPixels.Clear();

                foreach (Sys.Graphics.Point p in points)
                {
                    this.savedPixels.Add(new Tuple<Sys.Graphics.Point, Color>(p, this.canvas.GetPointColor(p.X, p.Y)));
                    this.canvas.DrawPoint(this.pen, p);

                }
            }

            if(MouseManager.MouseState==MouseState.Left&&this.prevMouseState!=MouseState.Left)
            {
                System.Console.Beep();
            }

            this.prevMouseState  = MouseManager.MouseState;


        }



    }
}
