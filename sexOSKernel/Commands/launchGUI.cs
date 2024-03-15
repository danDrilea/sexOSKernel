using System;
using System.Collections.Generic;
using System.Text;
using sexOSKernel.Graphics;

namespace sexOSKernel.Commands
{
    public class launchGUI : Command
    {
        public launchGUI(String name, String description) : base(name, description) { }

        public override string Execute(string[] args)
        {
            if (Kernel.gui != null)
                return "GUI already launched!";

            Kernel.gui = new GUI();

            return "Launched GUI!";
        }
    }
}
