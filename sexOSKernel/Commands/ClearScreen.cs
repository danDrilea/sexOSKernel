using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sexOSKernel.Commands
{
    public class ClearScreen : Command
    {
        public ClearScreen(String name, String description) : base(name, description) { }
        public override string Execute(string[] args)
        {
            Console.Clear();
            return base.Execute(args);
        }
    }
}
