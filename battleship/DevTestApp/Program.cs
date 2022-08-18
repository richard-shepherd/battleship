using GameEngine;
using Utility;

namespace DevTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.onMessageLogged += (sender, logArgs) =>
            {
                Console.WriteLine($"LOG: {logArgs.Message}");
            };

            var aiManager = new AIManager(@"D:\code\battleship\AIs");
            var ai = aiManager.createAIProcess("AI_Sample_RandomPlayer");

            var startGame = new API.StartGame.Message();
            startGame.BoardSize.X = 100;
            startGame.BoardSize.Y = 100;
            startGame.ShipSquares = 20;

            ai.sendMessage(startGame);
            Utils.wait(() => ai.HasOutput, 5000);
            Console.WriteLine(ai.Output);

            ai.Dispose();
        }
    }
}