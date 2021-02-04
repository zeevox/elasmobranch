using System.Collections.Generic;
using NUnit.Framework;
using static Elasmobranch.BIO.BritishInformaticsOlympiad2003;

namespace Tests.BIO
{
    [TestFixture]
    public class BritishInformaticsOlympiad2003
    {
        [Test]
        [TestCase("1", "15688?111X")]
        [TestCase("3", "812071988?")]
        [TestCase("X", "020161586?")]
        [TestCase("0", "?131103628")]
        [TestCase("1", "?86046324X")]
        [TestCase("5", "1?68811306")]
        [TestCase("4", "951?451570")]
        [TestCase("2", "0393020?31")]
        [TestCase("9", "01367440?5")]
        public void Question1ATest(string missingDigit, string input)
        {
            Assert.AreEqual(missingDigit, FindMissingNumber(input));
        }

        [Test]
        [TestCase(true, "3540678654")]
        [TestCase(true, "9514451570")]
        [TestCase(false, "0972311900")]
        [TestCase(false, "013674409X")]
        public void Question1BTest(bool valid, string isbn)
        {
            Assert.AreEqual(valid, IsValidBookNumber(isbn));
        }

        [Test]
        public void Question1CTest()
        {
            var validNumbersWithSwappedDigits = new List<string>
            {
                "2301014525",
                "0201314525",
                "3401012525",
                "3200114525"
            };

            Assert.AreEqual(validNumbersWithSwappedDigits, ValidNumbersWithSwappedDigits("3201014525"));
        }
    }
}