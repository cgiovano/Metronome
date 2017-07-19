using Windows.ApplicationModel.ExtendedExecution;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.ApplicationModel;
using Windows.UI.Core;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace Metronome
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MidiDeviceSelector deviceSelector = new MidiDeviceSelector();
        System.Threading.Timer periodicTimer = null;
        int interval;
        bool isActive = false;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BpmSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {   
            ShowBpm.Text = BpmSlider.Value.ToString() + " bpm";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (isActive == false)
            {
                interval = 60000 / ((int)BpmSlider.Value);
                BeginExtendExecution();
                StartButton.Content = "Stop";
            }
            else
            {
                EndExtendedExecution();
                StartButton.Content = "Start";
            }
        }

        //
        // Extended Execution
        //
        ExtendedExecutionSession session = null;

        //
        // Clear the Extended Excution process
        //
        void ClearExtendedExecution()
        {
            if (session != null)
            {
                session.Revoked -= SessionRevokedAsync;
                session.Dispose();
                session = null;
            }

            if (periodicTimer != null)
            {
                isActive = false;
                periodicTimer.Dispose();
                periodicTimer = null;
            }
        }

        //
        // Begin the Extended Execution
        //
        public async void BeginExtendExecution()
        {
            ClearExtendedExecution();

            using (var session = new ExtendedExecutionSession())
            {
                session.Reason = ExtendedExecutionReason.Unspecified;
                session.Description = "Run the metronome in background";
                session.Revoked += SessionRevokedAsync;
                ExtendedExecutionResult result = await session.RequestExtensionAsync();

                if (result == ExtendedExecutionResult.Denied)
                {
                    return;
                }

                periodicTimer = new System.Threading.Timer(PlaySound, null, 0, interval);
                isActive = true;
            }
        }

        void PlaySound(object state)
        {
            AudioPlayback.Beep1();
        }

        public void EndExtendedExecution()
        {
            ClearExtendedExecution();
        }

        private async void SessionRevokedAsync(object sender, ExtendedExecutionRevokedEventArgs args)
        {
            //
            // Notify the user that the session was revoked. Operation do redo/clode the resource?
            //
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { EndExtendedExecution(); });
        }
    }
}
