using Utility;

namespace UI
{
    /// <summary>
    /// Shows logged messages.
    /// </summary>
    public partial class Control_Log : UserControl
    {
        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Control_Log()
        {
            InitializeComponent();
            Logger.onMessageLogged += onMessageLogged;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Called when a message is logged.
        /// </summary>
        private void onMessageLogged(object sender, Logger.Args e)
        {
            try
            {
                ctrlLogText.Text = e.Message + Environment.NewLine + ctrlLogText.Text;
            }
            catch(Exception)
            {
                // We do not do anything here, as it may not be a good idea to log when we have
                // a failure with the logging itself.
            }
        }

        #endregion
    }
}
