using System;
using NUnit.Framework;
using static Elasmobranch.BIO.BritishInformaticsOlympiad2004;

namespace Tests.BIO
{
    public class BritishInformaticsOlympiad2004
    {
        [Test]
        [TestCase(new[] {2001, 3, 22}, new[] {13, 20, 9, 2, 9})]
        [TestCase(new[] {2000, 1, 1}, new[] {13, 20, 7, 16, 3})]
        [TestCase(new[] {2000, 1, 10}, new[] {13, 20, 7, 16, 12})]
        [TestCase(new[] {2000, 2, 10}, new[] {13, 20, 7, 18, 3})]
        [TestCase(new[] {2000, 9, 22}, new[] {13, 20, 8, 11, 8})]
        [TestCase(new[] {2008, 2, 29}, new[] {13, 20, 16, 3, 4})]
        [TestCase(new[] {2006, 5, 15}, new[] {13, 20, 14, 6, 9})]
        [TestCase(new[] {2012, 12, 21}, new[] {14, 1, 1, 1, 1})]
        [TestCase(new[] {2026, 7, 14}, new[] {14, 1, 14, 14, 14})]
        public void MayanToGregorianTest(int[] expected, int[] mayanDate)
        {
            Assert.AreEqual(new DateTime(expected[0], expected[1], expected[2]),
                MayanToGregorian(mayanDate[0], mayanDate[1], mayanDate[2], mayanDate[3], mayanDate[4]));
        }
    }
}