using System;
using Windows.ApplicationModel.ExtendedExecution;

namespace Metronome
{
    class ExtendedTask
    {
        //
        // Extended Execution
        //
        ExtendedExecutionSession session = null;

        System.Threading.Timer periodicTimer = null;

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

            if (periodicTimer != null)
            {
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
            Helper.Stop();

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

        public void EndExtendedExecution()
        {
            ClearExtendedExecution();
        }

        private void SessionRevoked(object sender, ExtendedExecutionRevokedEventArgs args)
        {
            //
            // Notify the user that the session was revoked
            //
            //EndExtendedExecution();
            BeginExtendExecution();
        }
    }
}
