using System;
using System.Collections.Generic;

namespace sexOSKernel.Commands
{
    public class Help : Command
    {
        private List<Command> commands;
        public Help(String name, String description, List<Command> commands) : base(name, description)   //Help ia ca argument si lista de comenzi, ca sa arate dinamic cate comenzi avem
        {
            this.commands = commands;
        }
        public override string Execute(String[] args)
        {
            string commandList = "Available commands:";

            // List all command names
            foreach (Command cmd in commands)
            {
                commandList += "\n- " + cmd.name + "  -" + cmd.description; //am adaugat si o descriere pentru fiecare comanda
            }

            return commandList;
        }

    }
}