using System;
using Windows.UI.Xaml;

namespace BackgroundMetronome
{
    public sealed class Metronome
    {
        //
        // The Task
        //
        DispatcherTimer timer = new DispatcherTimer();
        MidiDeviceSelector deviceSelector = new MidiDeviceSelector();

        bool isActive;
        public bool IsActive { get { return isActive; } }

        //
        // Timer counter function.
        //
        void TimerStart()
        {
            timer.Start();
            timer.Tick += timer_Tick;
        }

        //
        // Play the sound.
        //
        void timer_Tick(object sender, object e)
        {
            AudioPlayback.Beep1();
        }

        //
        // Start the Timer
        //
        public void Start(int bpm)
        {
            double interval = (double)60.000f / (bpm);
            timer.Interval = TimeSpan.FromSeconds(interval);

            TimerStart();
            isActive = true;
        }

        // 
        // Stop the Timer
        // 
        public void Stop()
        {
            timer.Stop();
            isActive = false;
        }
    }
}
