using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Sys = Cosmos.System;

namespace sexOSKernel//<------ SFARSIT SCOPE KERNEL
{
    ///nu IESITI DIN NAMESPACE CA FACE ASTA URAT
    ///practic ce e in scope-ul lui sexOSKernel il foloseste, daca iesi din scope nu mai recunoaste nimic
    public class Kernel : Sys.Kernel
    {
        //imi bag pula in ea de viata
        protected override void BeforeRun()
        {
            // Beginning of "Never Gonna Give You Up" Chorus
            PlaySong();
            Console.Write("                ___  ____  \r\n ___  _____  __/ _ \\/ ___| \r\n/ __|/ _ \\ \\/ / | | \\___ \\ \r\n\\__ \\  __/>  <| |_| |___) |\r\n|___/\\___/_/\\_\\\\___/|____/ \n");
            Console.WriteLine("sexOS booted successfully. Type a line of text to get it echoed back.");
        }
        
        ///Intro song functions
        protected override void Run()
        {
            ///sugi pula comentariu
            ///
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
        
        private void PlaySong()
        {
            PlayNote(392, 400); // G
            PlayNote(349, 200); // F
            PlayNote(294, 400); // D
            PlayNote(349, 200); // F
            PlayNote(392, 400); // G
        }

        private void PlayNote(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }
    }



    

}///<--- SFARSIT SCOPE KERNEL
