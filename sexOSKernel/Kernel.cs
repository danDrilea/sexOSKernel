using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Sys = Cosmos.System;
using sexOSKernel.Commands;
using Cosmos.System.FileSystem;
using sexOSKernel.Graphics;

namespace sexOSKernel//<------ INCEPUT SCOPE KERNEL
{
    ///nu IESITI DIN NAMESPACE CA FACE ASTA URAT
    ///practic ce e in scope-ul lui sexOSKernel il foloseste, daca iesi din scope nu mai recunoaste nimic
    public class Kernel : Sys.Kernel
    {
        public CosmosVFS vfs;//file system class
        private CommandManager commandManager;

        public static GUI gui;

        //imi bag pula in ea de viata
        protected override void BeforeRun()
        {
            this.vfs = new CosmosVFS();//register vfs
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.vfs);
            this.commandManager = new CommandManager();//adauga comenzile ca sa fie recunoscute in scope

            Console.Write("                ___  ____  \r\n ___  _____  __/ _ \\/ ___| \r\n/ __|/ _ \\ \\/ / | | \\___ \\ \r\n\\__ \\  __/>  <| |_| |___) |\r\n|___/\\___/_/\\_\\\\___/|____/ \n");
            Console.Write("sexOS successfully booted!\n");
        }

        ///Intro song functions
        protected override void Run()
        {
            if (gui != null && !gui.ShouldExitGUI)
            {
                gui.handleGUIInputs();
            }
            else if (gui != null && gui.ShouldExitGUI)
            {
                // Exit GUI mode
                ExitGUIMode(); // You need to define this method
                gui = null; // Reset GUI to null if you're done with it
            }
            else
            {
                // Handle console input/output as before
                HandleConsoleInput();
            }
        }
        private void ExitGUIMode()
        {
            // Clear the GUI canvas or disable graphics mode as necessary
            GUI.canvas.Disable(); // This is conceptual; actual method to disable canvas or graphics mode may vary

            // Potentially clear the console and reset any needed console settings
            Console.Clear();
            Console.WriteLine("Back to console mode.");

            // Re-enable console input if it was disabled during GUI mode
        }
        private void HandleConsoleInput()
        {
            String input = Console.ReadLine();
            string response = this.commandManager.processInput(input);
            Console.WriteLine(response);
        }


    }

}///<--- SFARSIT SCOPE KERNEL