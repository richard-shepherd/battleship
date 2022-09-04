using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI
{
    /// <summary>
    /// Manages the results for a tournament.
    /// </summary>
    internal class TournamentInfo
    {
        #region Public types

        /// <summary>
        /// Info for one AI's results in a tournament.
        /// </summary><remarks>
        /// Implements INotifyPropertyChanged so that the data can be updated in a bound data-grid.
        /// </remarks>
        public class AIInfo : INotifyPropertyChanged
        {
            /// <summary>
            /// Raised when a property changes.
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Gets or sets the AI name.
            /// </summary>
            public string AIName
            {
                get { return m_aiName; }
                set { m_aiName = value; notifyPropertyChanged(); }
            }

            /// <summary>
            /// Gets or sets the number of wins for the AI.
            /// </summary>
            public int Wins
            {
                get { return m_wins; }
                set { m_wins = value; notifyPropertyChanged(); }
            }

            /// <summary>
            /// Gets or sets the number of points scored by the AI.
            /// </summary>
            public int Points
            {
                get { return m_points; }
                set { m_points = value; notifyPropertyChanged(); }
            }

            /// <summary>
            /// Raises the PropertyChanged event for the property specified.
            /// </summary>
            private void notifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            // Backing data...
            private string m_aiName = "";
            private int m_wins = 0;
            private int m_points = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the AI infos in a form which can be bound to a data grid.
        /// </summary>
        public BindingList<AIInfo> AIInfos => m_aiInfos;

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public TournamentInfo(List<string> aiNames)
        {
            foreach(var aiName in aiNames)
            {
                m_aiInfos.Add(new AIInfo { AIName = aiName });
            }
        }

        /// <summary>
        /// Updates the info for the AI specified.
        /// </summary><remarks>
        /// Set win=1 if the AI won a game, or 0 if not.
        /// </remarks>
        public void updateAIInfo(string aiName, int win, int points)
        {
            var aiInfo = m_aiInfos.FirstOrDefault(x => x.AIName == aiName);
            if(aiInfo != null)
            {
                aiInfo.Wins += win;
                aiInfo.Points += points;
            }
        }

        #endregion

        #region Private data

        // List of AI tournament results, which can be bound to a data-grid...
        private readonly BindingList<AIInfo> m_aiInfos = new BindingList<AIInfo>();

        #endregion
    }
}
