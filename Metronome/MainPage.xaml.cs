using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace Metronome
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BpmSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                Helper.Stop();
                StartButton.Content = "Start";
            }
            catch { }

            Helper.bpm = (int)BpmSlider.Value;
            ShowBpm.Text = BpmSlider.Value.ToString() + " bpm";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.metronomeIsActive == false)
            {
                //Helper.Start();
                //BeginExtendExecution();
                Helper.Start();
                StartButton.Content = "Stop";
            }
            else
            {
                Helper.Stop();
                StartButton.Content = "Start";
            }
        }

        /*
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
                session.Revoked -= SessionRevoked;
                session.Dispose();
                session = null;
            }
        }

        //
        // Begin the Extended Execution
        //
        public async void BeginExtendExecution()
        {
            ClearExtendedExecution();

            ExtendedExecutionSession newSession = new ExtendedExecutionSession();
            newSession.Reason = ExtendedExecutionReason.Unspecified;
            newSession.Description = "Run the metronome in background";
            newSession.Revoked += SessionRevoked;
            ExtendedExecutionResult result = await newSession.RequestExtensionAsync();

            switch (result)
            {
                case (ExtendedExecutionResult.Allowed):
                    session = newSession;
                    Helper.Start();
                    break;

                default:
                case (ExtendedExecutionResult.Denied):
                    newSession.Dispose();
                    break;
            }
        }

        void EndExtendedExecution()
        {
            ClearExtendedExecution();
        }

        private async void SessionRevoked(object sender, ExtendedExecutionRevokedEventArgs args)
        {
            //
            // Notify the user that the session was revoked
            //
            //EndExtendedExecution();
            //BeginExtendExecution();
        }*/
    }
}
