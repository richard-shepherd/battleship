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
            var game = new Game(aiManager, "AI_Sample_RandomPlayer", "AI_Sample_RandomPlayer", 100, 20);
            game.playTurn();
            game.playTurn();

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();

            
            game.Dispose();
        }
    }
}