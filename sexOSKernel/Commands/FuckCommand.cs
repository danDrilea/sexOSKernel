using System;

namespace sexOSKernel.Commands
{
    public class FuckCommand : Command
    {
        public FuckCommand(String name, String description) : base(name, description) { }
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
        private void PlaySong() //N-am stat sa gasesc duratele intre note ca n-aveam chef, dar notele sunt bune
        {
            //NEVER GONNA GIVE YOU UP
            PlayNote(440, 100); // A4
            PlayNote(494, 100); // B4
            PlayNote(587, 100); // D5
            PlayNote(494, 100); // B4
            PlayNote(740, 150); // F#5
            PlayNote(740, 150); // F#5
            PlayNote(659, 150); // E5

            //NEVER GONNA LET YOU DOWN
            PlayNote(440, 100); // A4
            PlayNote(494, 100); // B4
            PlayNote(587, 100); // D5
            PlayNote(494, 100); // B4
            PlayNote(659, 150); // E5
            PlayNote(659, 150); // E5
            PlayNote(587, 80); // D5
            PlayNote(554, 80); // C#5
            PlayNote(494, 80); // B4


            //NEVER GONNA RUN AROUND AND DESERT YOU
            PlayNote(440, 100); // A4
            PlayNote(494, 100); // B4
            PlayNote(587, 100); // D5
            PlayNote(494, 100); // B4
            PlayNote(587, 150); // D5
            PlayNote(659, 150); // E5
            PlayNote(554, 100); // C#5
            PlayNote(440, 100); // A4
            PlayNote(440, 100); // A4
            PlayNote(659, 100); // E5
            PlayNote(587, 100); // D5
        }

        private void PlayNote(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }
    }
}
