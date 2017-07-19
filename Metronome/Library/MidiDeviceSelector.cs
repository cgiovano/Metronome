using System;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;

namespace BackgroundMetronome
{
    public class MidiDeviceSelector
    {
        public MidiDeviceSelector()
        {
            GetOutputMidiDevice();
        }

        public async void GetOutputMidiDevice()
        {
            IMidiOutPort currentMidiOutputDevice;

            DeviceInformation devInfo;
            DeviceInformationCollection devInfoCollection;

            string devInfoId;

            devInfoCollection = await DeviceInformation.FindAllAsync(MidiOutPort.GetDeviceSelector());

            if (devInfoCollection == null)
            {
                //notify the user that any device was found.
                System.Diagnostics.Debug.WriteLine("Any device was found.");
            }

            devInfo = devInfoCollection[0];

            if (devInfo == null)
            {
                //Notify the User that the device not found
                System.Diagnostics.Debug.WriteLine("Device not found.");
            }


            devInfoId = devInfo.Id.ToString();
            currentMidiOutputDevice = await MidiOutPort.FromIdAsync(devInfoId);

            if (currentMidiOutputDevice == null)
            {
                //Notify the User that wasn't possible to create MidiOutputPort for the device.
                System.Diagnostics.Debug.WriteLine("It was not possible to create the OutPort for the device.");
            }

            MidiDevice.Device = currentMidiOutputDevice;
        }
    }
}
