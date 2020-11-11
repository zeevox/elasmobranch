using NUnit.Framework;
using static Elasmobranch.Queues.QueueExercises;

namespace Tests.Queues
{
    public class QueueExercisesTests
    {
        [Test]
        public void SequenceCalculatorTest()
        {
            Assert.AreEqual(new[] {2, 3, 5, 4, 4, 7, 5, 6, 11, 7, 5, 9, 6}, SequenceCalculator(2, 13));
        }

        [Test]
        public void PathCalculatorTest()
        {
            Assert.AreEqual(new[] {5, 6, 8, 16}, CalculatePath(5, 16));
        }
    }
}