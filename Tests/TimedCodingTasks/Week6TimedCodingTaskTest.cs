using Elasmobranch.TimedCodingTasks;
using NUnit.Framework;

namespace Tests.TimedCodingTasks
{
    public class Week6TimedCodingTaskTest
    {
        [TestCase(210, "01001011", "11010010")]
        public void DecimalToBinaryTest(int dec, string expectedReversed, string expected)
        {
            var actualReversed = Week6TimedCodingTask.ConvertDecimalToBinaryReversed(dec);
            Assert.AreEqual(expectedReversed, actualReversed);

            var actual = Week6TimedCodingTask.ConvertDecimalToBinary(dec);
            Assert.AreEqual(expected, actual);

            TestContext.WriteLine(
                $"Decimal input: {dec} -> Reversed binary: {actualReversed} -> Binary output: {actual}");
        }
    }
}