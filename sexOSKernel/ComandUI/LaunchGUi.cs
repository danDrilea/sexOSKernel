using Cosmos.HAL.BlockDevice.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sexOSKernel.ComandUI
{
    public class LaunchGUi : Command
    {
        public LaunchGUi(String name) : base (name) { }

        public override String exexute(String[] args)
        {
            return "Launched GUI";
        }
    }
}
