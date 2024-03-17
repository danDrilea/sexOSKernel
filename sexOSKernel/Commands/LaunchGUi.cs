using Cosmos.HAL.BlockDevice.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sexOSKernel.UI;

namespace sexOSKernel.Commands
{
    public class LaunchGUi : Command
    {
        public LaunchGUi(String name, String description) : base (name, description) { }

        public override String execute(String[] args)
        {
            if(Kernel.gui!= null)
            {
                return "Esti deja in GUI coaie!";
            }

            Kernel.gui = new GUI();

            return "Porneste GUI";
        }
    }
}
