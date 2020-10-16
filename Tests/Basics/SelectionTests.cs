using NUnit.Framework;
using static Elasmobranch.Basics.Selection;

namespace Tests.Basics
{
    public class SelectionTests
    {
        [Test]
        public void EqualityCheckTest()
        {
            Assert.AreEqual(EqualityCheck(1, 1), true);
            Assert.AreEqual(EqualityCheck(2020, 2020), true);
            Assert.AreNotEqual(EqualityCheck(1, 2), true);
        }

        [Test]
        public void IsEvenTest()
        {
            Assert.AreEqual(IsEven(2), true);
            Assert.AreEqual(IsEven(1), false);
            Assert.AreEqual(IsEven(0), true);
        }

        [Test]
        public void IsLeapYearTest()
        {
            Assert.AreEqual(IsLeapYear(2020), true);
            Assert.AreEqual(IsLeapYear(2000), true);
            Assert.AreEqual(IsLeapYear(1900), false);
        }

        [Test]
        public void GreatestOfThreeTest()
        {
            Assert.AreEqual(3, GreatestOfThree(1, 2, 3));
            Assert.AreEqual(2, GreatestOfThree(1, 2, 2));
            Assert.AreEqual(1, GreatestOfThree(0, -1, 1));
        }

        [Test]
        public void DetermineQuadrantTest()
        {
            Assert.AreEqual("I", DetermineQuadrant(3, 5));
            Assert.AreEqual("II", DetermineQuadrant(-5, 3));
            Assert.AreEqual("III", DetermineQuadrant(-5, -3));
            Assert.AreEqual("IV", DetermineQuadrant(5, -3));
            Assert.AreEqual("origin", DetermineQuadrant(0, 0));
            Assert.AreEqual("one of the coordinates is zero or my logic is flawed",
                DetermineQuadrant(5, 0));
            Assert.AreEqual("one of the coordinates is zero or my logic is flawed",
                DetermineQuadrant(0, 5));
        }

        [Test]
        public void ScoreToGradeTest()
        {
            Assert.AreEqual("A*", ScoreToGrade(100, 100, 100));
            Assert.AreEqual("A", ScoreToGrade(80, 90, 85));
            Assert.AreEqual("B", ScoreToGrade(70, 77, 73));
            Assert.AreEqual("F", ScoreToGrade(10, 10, 100));
            Assert.AreNotEqual("F", ScoreToGrade(45, 50, 55));
        }

        [Test]
        public void TriangulatorTest()
        {
            Assert.AreEqual("equilateral", Triangulator(5, 5, 5));
            Assert.AreEqual("isosceles", Triangulator(5, 5, 3));
            Assert.AreNotEqual("scalene", Triangulator(5, 5, 3));
            Assert.AreEqual("impossible", Triangulator(5, 12, 2));
        }
    }
}