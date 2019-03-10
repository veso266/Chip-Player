using System;

namespace ChipTune_Player
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo keypress = new ConsoleKeyInfo();
            //PlayXM XM = new PlayXM("i_loved_you_by_heart.xm"); //Reads a file
            PlayXM XM = new PlayXM(Music.Song2); //Reads from memory
            XM.Play();
            keypress = Console.ReadKey(true);
            switch(keypress.Key)
            {
                case ConsoleKey.Escape: //If Escape key is pressed it stops the stream and frees resources (won't crash if you close it after doing that :))
                    XM.Stop();
                    break;
            }
        }
    }
}
