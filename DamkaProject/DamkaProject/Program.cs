using System;

namespace DamkaProject
{
    class Program
    {
        public static void Main()
        {
            Game game = new Game(); // hi
            game.Run();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
