using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elasmobranch.Bitwise;
using NUnit.Framework;

namespace Tests.Bitwise
{
    public class BitwiseCountdownSolverTests
    {
        [Test]
        [TestCase(new[] {25,9,5,7,100,7}, 191)]
        [TestCase(new[] {50,1,9,7,5,6}, 753)]
        [TestCase(new[] {1,8,6,4,7,100}, 658)]
        [TestCase(new[] {1,8,6,4,7,100}, 581)] // 581, 585, 651, 653, 693, 699, 701, 958, 973, 978, 981
        public void TestSolve(int[] cards, int target)
        {
            TestContext.WriteLine(BitwiseCountdownSolver.Solve(cards, target));
        }

        [Test]
        [Ignore("Very time consuming and unnecessary")]
        [TestCase(new[] {1,8,6,4,7,100})]
        public void FindImpossibleNumbers(int[] cards)
        {
            var impossible = new List<int>();
            var watch = new Stopwatch();
            for (var i = 10; i < 1000; i++)
            {
                watch.Start();
                var output = BitwiseCountdownSolver.Solve(cards, i);
                watch.Stop();
                TestContext.Progress.WriteLine($"[target={i}] {output} in {watch.ElapsedMilliseconds:N0}ms");
                if (output == "No solution found") impossible.Add(i);
                watch.Reset();
            }
            TestContext.Out.WriteLine(string.Join(", ", impossible));
        }
    }
}