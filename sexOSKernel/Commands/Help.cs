using System;

namespace sexOSKernel.Commands
{
    public class Help : Command
    {
        public Help(String name) : base(name) { }
        public override string Execute(String[] args)
        {
            return "There is no help for you!";
        }

    }
}
