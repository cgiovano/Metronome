namespace Metronome
{
    class Helper
    {
        static Metronome metronome = new Metronome();

        public static int bpm;
        public static bool metronomeIsActive;

        public static void Start()
        {
            metronome.Start(bpm);
            metronomeIsActive = metronome.IsActive;
        }

        public static void Stop()
        {
            metronome.Stop();
            metronomeIsActive = metronome.IsActive;
        }

        
    }
}
