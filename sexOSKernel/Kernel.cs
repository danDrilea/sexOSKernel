using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Sys = Cosmos.System;
using sexOSKernel.Commands;
using Cosmos.System.FileSystem;
namespace sexOSKernel//<------ INCEPUT SCOPE KERNEL
{
    ///nu IESITI DIN NAMESPACE CA FACE ASTA URAT
    ///practic ce e in scope-ul lui sexOSKernel il foloseste, daca iesi din scope nu mai recunoaste nimic
    public class Kernel : Sys.Kernel
    {
        public CosmosVFS vfs;//file system class
        private CommandManager commandManager;
        //imi bag pula in ea de viata
        protected override void BeforeRun()
        {
            this.vfs = new CosmosVFS();//register vfs
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.vfs);
            this.commandManager = new CommandManager();//adauga comenzile ca sa fie recunoscute in scope
            // Beginning of "Never Gonna Give You Up" Chorus
            Console.Write("                ___  ____  \r\n ___  _____  __/ _ \\/ ___| \r\n/ __|/ _ \\ \\/ / | | \\___ \\ \r\n\\__ \\  __/>  <| |_| |___) |\r\n|___/\\___/_/\\_\\\\___/|____/ \n");
            Console.Write("sexOS successfully booted!\n");
        }
        
        ///Intro song functions
        protected override void Run()
        {
            String input = Console.ReadLine();
            string response = this.commandManager.processInput(input);
            Console.WriteLine(response);
        }
       
    }

}///<--- SFARSIT SCOPE KERNEL