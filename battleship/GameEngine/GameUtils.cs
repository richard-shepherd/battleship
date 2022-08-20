using Utility;

namespace GameEngine
{
    /// <summary>
    /// Utilities for games.
    /// </summary>
    internal class GameUtils
    {
        #region Public methods

        /// <summary>
        /// Waits for responses from both players.
        /// Logs messages and throws an exception if one or both responses were not received.
        /// </summary>
        public static void waitForAIReponses(Player player1, Player player2, int timeoutMS, string messageType)
        {
            waitForAIReponses(player1.AI, player2.AI, timeoutMS, messageType);
        }

        /// <summary>
        /// Waits for responses from both AIs.
        /// Logs messages and throws an exception if one or both responses were not received.
        /// </summary>
        public static void waitForAIReponses(AIProcess ai1, AIProcess ai2, int timeoutMS, string messageType)
        {
            var gotBothResponses = Utils.wait(() => ai1.HasOutput && ai2.HasOutput, timeoutMS);
            var messages = new List<string>();
            if (!ai1.HasOutput)
            {
                messages.Add($"{ai1.Name} did not respond to the {messageType} message");
            }
            if (!ai2.HasOutput)
            {
                messages.Add($"{ai2.Name} did not respond to the {messageType} message");
            }
            if (!gotBothResponses)
            {
                var message = String.Join("; ", messages);
                Logger.log(message);
                throw new Exception(message);
            }
        }

        #endregion
    }
}
