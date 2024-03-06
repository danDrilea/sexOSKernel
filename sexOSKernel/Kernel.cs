using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace sexOSKernel
{
    public class Kernel : Sys.Kernel
    {
        //imi bag pula in ea de viata
        protected override void BeforeRun()
        {
            Console.WriteLine("sexOS booted successfully. Type a line of text to get it echoed back.");
            Console.Write("                ___  ____  \r\n ___  _____  __/ _ \\/ ___| \r\n/ __|/ _ \\ \\/ / | | \\___ \\ \r\n\\__ \\  __/>  <| |_| |___) |\r\n|___/\\___/_/\\_\\\\___/|____/ \n");
        }

        protected override void Run()
        {
            ///sugi pula comentariu
            /////hai sa vedem daca INCA MERGE
            Console.Write("MA AUZI SUGI PULA??");
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
    }
}
