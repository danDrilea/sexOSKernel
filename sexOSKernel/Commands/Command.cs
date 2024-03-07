using System;

namespace sexOSKernel.Commands
{
    public class Command
    {
        /// <summary>
        /// Asta e doar un model pentru o comanda, nu are niciun folos decat sa fie mostenita de catre alte comenzi(Help/FuckCommand)
        /// </summary>
        public readonly String name;
        public Command(string name)
        {
            this.name = name;
        }

        public virtual String Execute(String[] args)
        {
            ///De aceea nu returneaza nimic aici
            ///Metoda e facuta ca sa primeasca override(virtual)
            return "";
        }

    }
}
