using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System;

namespace sexOSKernel.Graphics
{
    public class GUI
    {
        private Canvas canvas;
        private Pen pen;

        private List<Tuple<Sys.Graphics.Point, Color>> savedPixels;//we use this to save what is behind the mouse

        private MouseState previousMouseState;
        private termopanBar termopanbar;

        private UInt32 pX, pY; //previous x, previous y

        public GUI() //constructor
        {
            this.canvas = FullScreenCanvas.GetCurrentFullScreenCanvas();
            this.canvas.Clear(Color.Black); //background GUI color

            this.pen = new Pen(Color.White); //foreGround GUI color
            this.previousMouseState = MouseState.None;

            //initial mouse pos
            this.pX = 3; 
            this.pY = 3;
            this.savedPixels = new List<Tuple<Sys.Graphics.Point, Color>>();

            this.termopanbar = new termopanBar(this.canvas);

            MouseManager.ScreenHeight = (UInt32)this.canvas.Mode.Rows;  //this initializes the mouse manager (weird af) - has to be unsigned
            MouseManager.ScreenWidth = (UInt32)this.canvas.Mode.Columns;
        }

        public void handleGUIInputs()
        {
            //so that we dont update mouse pozition when stationary
            if (this.pX != MouseManager.X && this.pY != MouseManager.Y)
            {
                //if out of bounds -> bad (2px margin limit) sexOS crashes if you tell it to draw out of bounds
                if (MouseManager.X < 2 || MouseManager.Y < 2 || MouseManager.X > (MouseManager.ScreenWidth - 2) || MouseManager.Y > (MouseManager.ScreenHeight - 2))
                    return;

                this.pX = MouseManager.X;
                this.pY = MouseManager.Y;

                //here we have the mouse design -> currently a cross (we could make something cooler)
                Sys.Graphics.Point[] points = new Sys.Graphics.Point[]
                {
                    new Sys.Graphics.Point((Int32)MouseManager.X, (Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X + 1, (Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X - 1, (Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X, (Int32)MouseManager.Y + 1),
                    new Sys.Graphics.Point((Int32)MouseManager.X, (Int32)MouseManager.Y - 1),
                };

                foreach (Tuple<Sys.Graphics.Point, Color> pixelData in this.savedPixels)
                    this.canvas.DrawPoint(new Pen(pixelData.Item2), pixelData.Item1); //new pen with old color and position

                this.savedPixels.Clear(); //clear the saved pixels for the new frame

                foreach (Sys.Graphics.Point p in points)
                {
                    //save the pixel position and color
                    this.savedPixels.Add(new Tuple<Sys.Graphics.Point, Color>(p, this.canvas.GetPointColor(p.X, p.Y)));
                    this.canvas.DrawPoint(this.pen, p);
                }
            }

            //event listener for single click 
            if(MouseManager.MouseState == MouseState.Left && this.previousMouseState != MouseState.Left)
            {
                System.Console.Beep(); //beeeeeeep
            }

            this.previousMouseState = MouseManager.MouseState;

        }
    }
}
