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


            var wins = new Dictionary<int, int> { { 1, 0 }, { 2, 0 } };

            for(var i=0; i< 1000; ++i)
            {
                var game = new Game(aiManager, "AI_Sample_RandomPlayer", "AI_Sample_RandomPlayer", 50, 20);
                try
                {
                    game.startGame();
                    while (game.GameStatus == Game.GameStatusEnum.PLAYING)
                    {
                        game.playTurn();
                    }
                    wins[game.WinningPlayerNumber] = wins[game.WinningPlayerNumber] + 1;
                    Console.WriteLine($"1: {wins[1]}, 2: {wins[2]}");
                }
                catch(Exception)
                {
                }
                finally
                {
                    game.Dispose();
                }
            }

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }
}