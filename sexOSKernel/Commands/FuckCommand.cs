using System;

namespace sexOSKernel.Commands
{
    public class FuckCommand : Command
    {
        public FuckCommand(String name) : base(name) { }
        public override string Execute(string[] args)
        {
            PlaySong();
            return @"
   ___
 /     \
| () () |
 \  ^  /
  |||||
";
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
}
