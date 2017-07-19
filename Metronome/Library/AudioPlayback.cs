using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Midi;

namespace Metronome
{
    public class AudioPlayback
    {
        static IMidiMessage beep1 = new MidiNoteOnMessage(9, 76, 90);


        public static void Beep1()
        {
            try
            {
                MidiDevice.Device.SendMessage(beep1);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
