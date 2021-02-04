using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Elasmobranch.TimedCodingTasks.Week7BlockTest;

namespace Tests.TimedCodingTasks
{
    [TestFixture]
    public class Week7BlockTestTests
    {
        [Test]
        public void RepeatsTest()
        {
            Assert.AreEqual(15, Repeats(new List<int> {4, 5, 7, 5, 4, 8}));
            Assert.AreEqual(19, Repeats(new List<int> {9, 10, 19, 13, 19, 13}));
            Assert.AreEqual(12, Repeats(new List<int> {16, 0, 11, 4, 8, 16, 0, 11}));
        }

        [Test]
        public void FindShortsTest()
        {
            Assert.AreEqual(3, FindShort("bitcoin take over the world maybe who knows perhaps"));
            Assert.AreEqual(2, FindShort("consectetur adipiscing elit sed do eiusmod tempor incididiunt"));
        }

        [Test]
        public void PyramidTest()
        {
            Assert.AreEqual(new int[0], Pyramid(0));
            Assert.AreEqual(new[]
            {
                new[] {1}
            }, Pyramid(1));
            Assert.AreEqual(new[]
            {
                new[] {1},
                new[] {1, 1}
            }, Pyramid(2));
            Assert.AreEqual(new[]
            {
                new[] {1},
                new[] {1, 1},
                new[] {1, 1, 1}
            }, Pyramid(3));
    }
    }
}