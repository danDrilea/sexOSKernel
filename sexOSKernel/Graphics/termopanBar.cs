using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace sexOSKernel.Graphics
{
    public class termopanBar
    {
        private Pen pen;
        private Int32 rows, cols;

        public termopanBar(Canvas canvas)
        {
            this.pen = new Pen(Color.White);
            this.rows = canvas.Mode.Rows;
            this.cols = canvas.Mode.Columns;

            canvas.DrawRectangle(this.pen, 0, this.rows - 100, this.cols - 2, 99); // taskbar thing
            canvas.DrawRectangle(this.pen, 0, this.rows - 100, 100, 99); // button
            canvas.DrawLine(this.pen, 25, this.rows - 90, 75, this.rows - 10);// line inside button
        }

        public void tryProcessTermopanBarClick(Int32 mouseX, Int32 mouseY)
        {
            //collider for button
            if (new Rectangle(mouseX, mouseY, 1, 1).IntersectsWith(new Rectangle(0, this.rows - 100, 100, 99)))
            {
                Console.Beep();
            }
        }
    }
}
