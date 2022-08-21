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
            var game = new Game(aiManager, "AI_Sample_RandomPlayer", "AI_Sample_RandomPlayer", 10, 20);
            while(game.GameStatus == Game.GameStatusEnum.PLAYING)
            {
                game.playTurn();
                Console.Write(".");
            }
            Console.WriteLine("");
            Console.WriteLine($"{game.GameStatus}: {game.WinningAIName}");

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();

            
            game.Dispose();
        }
    }
}