using System.Runtime.InteropServices;

namespace ChipTune_Player
{
    class PlayXM
    {
        byte[] data; //ChipTune Data
        public PlayXM(string FileName) //Play File
        {
            //Get file into memory
            data = System.IO.File.ReadAllBytes(FileName);
        }
        public PlayXM(byte[] XMData) //Play from Memory (Base64 encoded)
        {
            //Get Memory to BASSMOD_MusicLoad
            data = XMData;
        }
        internal struct BASSMOD_CONSTANTS
        {
            public const int BASS_MUSIC_LOOP = 4; // loop music
            public const int BASS_MUSIC_RAMPS = 2; // sensitive ramping
            public const int BASS_MUSIC_SURROUND = 512; // surround sound
            public const int BASS_MUSIC_CALCLEN = 8192; // calculate playback length
            public const int BASS_SYNC_END = 2;
        }

        /// <summary>
        ///     Initializes an output device.
        /// </summary>
        /// <param name="device">The device to use; -1 = default, 0 = no sound, 1 = first real output</param>
        /// <param name="frequency">Output sample rate</param>
        /// <param name="flags">Combination of BASSInitFlags</param>
        /// <returns>0 if the device was successfully initialized, else 1</returns>
        [DllImport("BASSMOD.dll")]
        public static extern int BASSMOD_Init(int device, uint frequency, uint flags); //Initialize BASSMOD

        /// <summary>
        ///     Frees all resources used by the output device
        /// </summary>
        /// <returns>true if successful, else false is returned</returns>
        [DllImport("BASSMOD.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BASSMOD_Free();

        /// <summary>
        ///    Used to load Music
        /// </summary>
        /// <param name = "mem" > Needs to be set to TRUE</param>
        /// <param name = "XMfile" > An IntPtr to the memory block.</param>
        /// <param name = "offset" > File offset to load the MOD music from.</param>
        /// <param name = "length" > Needs to be set to the length of the buffer data which should be played.</param>
        /// <param name = "flags" > A combination of these flagsž</param>
        /// <returns>If successful, the loaded music's handle is returned, else 0 is returned.</returns>

        [DllImport("BASSMOD.dll")]
        public static extern int BASSMOD_MusicLoad([MarshalAs(UnmanagedType.Bool)] bool mem, [In][MarshalAs(UnmanagedType.LPArray)] byte[] XMfile, uint offset, uint length, uint flags); //Select File to play

        /// <summary>
        /// Starts the stream
        /// </summary>
        /// <returns></returns>
        [DllImport("BASSMOD.dll")]
        public static extern int BASSMOD_MusicPlay();

        public void Play()
        {
            //Play that file
            BASSMOD_Init(-1, 44100, 0); //initialize bassmod
            BASSMOD_MusicLoad(true, data, 0, (uint)data.Length, BASSMOD_CONSTANTS.BASS_MUSIC_LOOP | BASSMOD_CONSTANTS.BASS_MUSIC_RAMPS | BASSMOD_CONSTANTS.BASS_MUSIC_SURROUND | BASSMOD_CONSTANTS.BASS_MUSIC_CALCLEN); //Load file
            BASSMOD_MusicPlay(); //Starts playback
        }
        public void Stop()
        {
            BASSMOD_Free(); //Stops playback and frees resources
        }
    }
}
