using Cosmos.Core.Memory;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using Sys = Cosmos.System;

namespace sexOSKernel.Graphics
{
    public class GUI
    {
        public bool ShouldExitGUI { get; private set; } = false;
        public static Canvas canvas;
        private Pen pen;
        private Pen termopanPen;
        private List<Tuple<Sys.Graphics.Point, Color>> savedPixels; // We use this to save what is behind the mouse
        private Sys.Graphics.Point lastMousePosition = new Sys.Graphics.Point(-1, -1); // Initialize to an invalid position
        private Color lastMousePositionColor = Color.White; // The background color
        private int rows, cols;

        public GUI() //constructor
        {
            // Attempt to create a canvas with a resolution of 400x300
            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1024, 768, ColorDepth.ColorDepth32));
            canvas.Clear(Color.White);

            this.pen = new Pen(Color.Black);
            this.termopanPen = new Pen(Color.BlueViolet);
            this.savedPixels = new List<Tuple<Sys.Graphics.Point, Color>>();

            // Adjust rows and cols according to the new resolution
            this.rows = canvas.Mode.Rows;
            this.cols = canvas.Mode.Columns;

            // Set mouse manager screen dimensions to match the canvas resolution
            MouseManager.ScreenWidth = 1024;
            MouseManager.ScreenHeight = 768;

            // Draw a 100x100 square at the top-left corner of the screen
            DrawSaveAreaIndicator();
        }

        private void DrawSaveAreaIndicator()
        {
            // Use a different pen color to distinguish the save area
            Pen saveAreaPen = new Pen(Color.Green);
            // Draw a 100x100 square starting at the top left corner (0,0)
            canvas.DrawRectangle(saveAreaPen, 0, 0, 800, 600);
        }

        public void handleGUIInputs()
        {
            var mousePosition = GetMousePosition();

            if (MouseManager.MouseState == MouseState.Left)
            {
                DrawOnClick(mousePosition);
            }
            else
            {
                DrawMouseCursor(mousePosition);
            }

            if (KeyboardManager.KeyAvailable)
            {
                var key = KeyboardManager.ReadKey();
                if (key.Key == ConsoleKeyEx.Escape)
                {
                    ShouldExitGUI = true;
                }
                if (key.Key == ConsoleKeyEx.S) // Trigger save on 'S' key press
                {
                    SaveCanvasState("0:\\CanvasState.bin"); // File path where canvas state will be saved
                }
                if (key.Key == ConsoleKeyEx.L) // Trigger load on 'L' key press
                {
                    LoadCanvasState("0:\\CanvasState.bin"); // File path where canvas state is saved
                }
            }
            CheckForExit();
            canvas.Display();
        }
        public void SaveCanvasState(string filePath)
        {
            int width = 800;  // Specified canvas width for the area of interest
            int height = 600;  // Specified canvas height for the area of interest
            var pixelData = new byte[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Use canvas.GetPointColor to get the color of the pixel at (x, y)
                    var color = canvas.GetPointColor(x, y);

                    // If the pixel color is red, mark it as 1 in pixelData; otherwise, mark it as 0
                    pixelData[y * width + x] = (color.R == 255 && color.G == 0 && color.B == 0) ? (byte)1 : (byte)0;

                }
            }

            try
            {
                // Create or open the file for writing the pixel data
                var fileStream = Sys.FileSystem.VFS.VFSManager.CreateFile(filePath).GetFileStream();
                if (fileStream.CanWrite)
                {
                    // Write the entire pixelData array to the file
                    fileStream.Write(pixelData, 0, pixelData.Length);
                }
                fileStream.Close(); // Always close the file stream after finishing
                Heap.Collect();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error saving canvas state: " + e.Message);
            }
        }

        public void LoadCanvasState(string filePath)
        {
            try
            {
                var fileStream = Sys.FileSystem.VFS.VFSManager.GetFile(filePath).GetFileStream();
                if (fileStream.CanRead)
                {
                    int width = 800; // The width of the saved canvas area
                    int height = 600; // The height of the saved canvas area
                    var pixelData = new byte[width * height]; // One byte per pixel
                    Pen redPen = new Pen(Color.Red);

                    // Read the pixel data from the file
                    fileStream.Read(pixelData, 0, pixelData.Length);
                    fileStream.Close(); // Close the file stream after reading
                    Heap.Collect();
                    // Iterate through the pixel data and redraw the canvas based on the saved state
                    for (int y = 0; y < height; y++)
                    {   
                        for (int x = 0; x < width; x++)
                        {
                            // If the byte for this pixel is 1, draw a red rectangle (pixel) at its position
                            if (pixelData[y * width + x] == 1)
                            {
                                canvas.DrawFilledRectangle(redPen, x, y, 1, 1); // Draw a 1x1 red rectangle
                            }
                        }
                    }
                    canvas.Display(); // Refresh the canvas to display the updated state
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error loading canvas state: " + e.Message);
            }
        }


        private void CheckForExit()
        {
            if (KeyboardManager.KeyAvailable)
            {
                if (KeyboardManager.ReadKey().Key == ConsoleKeyEx.Escape)
                {
                    ShouldExitGUI = true;
                }
            }
        }

        private Sys.Graphics.Point GetMousePosition()
        {
            return new Sys.Graphics.Point((int)MouseManager.X, (int)MouseManager.Y);
        }

        private void DrawOnClick(Sys.Graphics.Point position)
        {
            Pen pen = new Pen(Color.Red);
            canvas.DrawFilledRectangle(pen, position.X, position.Y, 4, 4);
        }

        private void DrawMouseCursor(Sys.Graphics.Point position)
        {
            if (lastMousePosition.X != -1 && lastMousePosition.Y != -1)
            {
                Pen backgroundPen = new Pen(lastMousePositionColor);
                canvas.DrawFilledRectangle(backgroundPen, lastMousePosition.X, lastMousePosition.Y, 2, 2);
            }

            lastMousePosition = position;
            Pen pen = new Pen(Color.Black);
            canvas.DrawFilledRectangle(pen, position.X, position.Y, 2, 2);
        }
    }
}