using Cosmos.System.Graphics;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Text;
using Cosmos.System;
using Sys = Cosmos.System;

namespace sexOSKernel.UI
{
    // Defines the GUI class within the sexOSKernel.UI namespace for a Cosmos-based OS project.
    public class GUI
    {
        private Canvas canvas; // Canvas object for drawing graphics on the screen.
        private Pen pen; // Pen object for specifying the color and style of graphics drawn.

        private MouseState prevMouseState; // Tracks the previous state of the mouse.
        private UInt32 pX, pY; // Variables to store the previous mouse X and Y positions.
        private List<Tuple<Sys.Graphics.Point, Color>> savedPixels; // List to store pixel data (position and color) for restoring pixels under the mouse pointer.
        private TabBar tabBar; // Instance of TabBar, likely managing tabs or window-like elements in the GUI.

        public GUI()
        {
            // Initializes the full-screen canvas and sets its background to black.
            this.canvas = FullScreenCanvas.GetFullScreenCanvas();
            this.canvas.Clear(Color.Black);

            // Initializes the pen with white color for drawing.
            this.pen = new Pen(Color.White);

            // Initializes mouse state and position variables.
            this.prevMouseState = MouseState.None;
            this.pX = 3; // Arbitrary starting position, to be updated with actual mouse position.
            this.pY = 3;
            this.savedPixels = new List<Tuple<Sys.Graphics.Point, Color>>();

            // Initializes the TabBar for managing GUI elements like tabs.
            this.tabBar = new TabBar(this.canvas);

            // Sets the MouseManager screen dimensions to match the canvas size for accurate mouse position tracking.
            MouseManager.ScreenHeight = (UInt32)this.canvas.Mode.Rows;
            MouseManager.ScreenWidth = (UInt32)this.canvas.Mode.Columns;
        }

        // Handles input for the GUI, such as mouse movement and clicks.
        public void handelGUIInputs()
        {
            // Checks if the mouse has moved to a new position.
            if (this.pX != MouseManager.X && this.pY != MouseManager.Y)
            {
                // Prevents drawing if the mouse is too close to the screen edges.
                if (MouseManager.X < 2 || MouseManager.Y < 2 || MouseManager.X > (MouseManager.ScreenWidth - 2) || MouseManager.Y > (MouseManager.ScreenHeight - 2))
                {
                    return;
                }

                // Updates the mouse position variables.
                this.pX = MouseManager.X;
                this.pY = MouseManager.Y;

                // Points around the current mouse position for drawing a simple pointer or cursor.
                Sys.Graphics.Point[] points = new Sys.Graphics.Point[]
                {
                    new Sys.Graphics.Point((Int32)MouseManager.X,(Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X+1,(Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X-1,(Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X,(Int32)MouseManager.Y+1),
                    new Sys.Graphics.Point((Int32)MouseManager.X,(Int32)MouseManager.Y-1)
                };

                // Restores the pixels under the previous mouse pointer position.
                foreach (Tuple<Sys.Graphics.Point, Color> pixelData in this.savedPixels)
                {
                    this.canvas.DrawPoint(new Pen(pixelData.Item2), pixelData.Item1);
                }

                // Clears saved pixels after restoration.
                this.savedPixels.Clear();

                // Saves and draws the new mouse pointer pixels.
                foreach (Sys.Graphics.Point p in points)
                {
                    this.savedPixels.Add(new Tuple<Sys.Graphics.Point, Color>(p, this.canvas.GetPointColor(p.X, p.Y)));
                    this.canvas.DrawPoint(this.pen, p);
                }
            }

            // Checks for a left mouse click and processes it if the previous state wasn't a left click.
            if (MouseManager.MouseState == MouseState.Left && this.prevMouseState != MouseState.Left)
            {
                this.tabBar.tryProcessTabBarClick((Int32)MouseManager.X, (Int32)MouseManager.Y);
            }

            // Updates the previous mouse state for the next cycle of input handling.
            this.prevMouseState = MouseManager.MouseState;
        }
    }
}
