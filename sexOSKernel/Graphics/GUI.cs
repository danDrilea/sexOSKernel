using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using sexOSKernel.Commands;
using System;
using System.Drawing;
using Sys = Cosmos.System;
using Console = System.Console;
using System.Collections.Generic;

namespace sexOSKernel.Graphics
{
    public class GUI
    {
        public static Canvas canvas;
        private Pen pen;

        private List<Tuple<Sys.Graphics.Point, Color>> savedPixels; // We use this to save what is behind the mouse

        private Sys.Graphics.Point lastMousePosition = new Sys.Graphics.Point(-1, -1); // Initialize to an invalid position
        private Color lastMousePositionColor = Color.White; // The background color

        public GUI() //constructor
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1024, 768, ColorDepth.ColorDepth32));
            canvas.Clear(Color.White);

            this.pen = new Pen(Color.Black);
            this.savedPixels = new List<Tuple<Sys.Graphics.Point, Color>>();

            MouseManager.ScreenHeight = 768;
            MouseManager.ScreenWidth = 1024;
        }

        public void handleGUIInputs()
        {
            // Basic screen update setup
            var mousePosition = GetMousePosition();

            // Check if the left mouse button is pressed
            if (MouseManager.MouseState == MouseState.Left)
            {
                // Draw something at the mouse position on click
                DrawOnClick(mousePosition);
            }
            else
            {
                // If not clicking, draw the cursor
                DrawMouseCursor(mousePosition);
            }

            canvas.Display();
        }

        private Sys.Graphics.Point GetMousePosition()
        {
            return new Sys.Graphics.Point((int)MouseManager.X, (int)MouseManager.Y);
        }

        private void DrawOnClick(Sys.Graphics.Point position)
        {
            // Implement drawing logic on mouse click, if needed
            // For example:
            Pen pen = new Pen(Color.Red);
            canvas.DrawFilledRectangle(pen, position.X, position.Y, 10, 10);
        }

        private void DrawMouseCursor(Sys.Graphics.Point position)
        {
            // Check if the mouse has moved
            if (lastMousePosition.X != -1 && lastMousePosition.Y != -1)
            {
                // Repaint the last mouse position with the background color
                Pen backgroundPen = new Pen(lastMousePositionColor);
                canvas.DrawFilledRectangle(backgroundPen, lastMousePosition.X, lastMousePosition.Y, 5, 5);
            }

            // Update the last mouse position
            lastMousePosition = position;

            // Draw the cursor at the new position
            Pen pen = new Pen(Color.Black);
            canvas.DrawFilledRectangle(pen, position.X, position.Y, 5, 5);
        }
    }
}
