using System;

namespace Elasmobranch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Console.WriteLine(BitwiseCountdownSolver.Solve(new[] {25,9,5,7,100,7}, 191));
            Console.WriteLine(BitwiseCountdownSolver.Solve(new[] {50,1,9,7,5,6}, 753));
        }
    }
}