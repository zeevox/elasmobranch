using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasmobranch.Sorting;
using NUnit.Framework;

namespace Tests.Sorting
{
    public class SortingTests
    {
        /// <summary>
        ///     Generates a list of random integers in order to test our sorting functions
        /// </summary>
        /// <param name="length">The length/size of the list</param>
        /// <param name="maxItemValue">
        ///     The maximum integer value of a single element in the list (the minimum is -1 *
        ///     max_item_value)
        /// </param>
        /// <returns></returns>
        private static IEnumerable<int> GenerateListOfInts(int length, int maxItemValue = 1000)
        {
            // for generating the pseudo-random numbers we use the Random class
            var random = new Random();

            // list comprehension, c# style? 
            return from number in Enumerable.Range(0, length) select random.Next(-1 * maxItemValue, maxItemValue);
        }

        /// <summary>
        ///     Test the performance and validity of my sorting functions.
        ///     We make the assumption that the inbuilt sort function is always correct.
        /// </summary>
        /// <param name="repeats">The number of lists to sort -- because sorting speed vary depending on the input</param>
        /// <param name="listSize">The number of elements in each list</param>
        [Test]
        // The more times we repeat the more unbiased the result -- because sorting speeds will vary depending on the input
        [TestCase(100, 1000)]
        [TestCase(100, 10000)]
        public void SortingTest(int repeats, int listSize)
        {
            var timeTakenInbuilt = new long[repeats];
            var timeTakenQuickSort = new long[repeats];

            // to record how long it took to sort each list
            var watch = Stopwatch.StartNew();

            for (var i = 0; i < repeats; i++)
            {
                var li = GenerateListOfInts(listSize).Cast<IComparable>().ToList();
                var liSortedByInbuiltFunction = new List<IComparable>(li);

                watch.Restart();
                liSortedByInbuiltFunction.Sort();
                watch.Stop();
                timeTakenInbuilt[i] = watch.ElapsedMilliseconds;

                watch.Restart();
                var liSortedByQuickSort = QuickSort.Sort(li);
                watch.Stop();
                timeTakenQuickSort[i] = watch.ElapsedMilliseconds;

                Assert.AreEqual(liSortedByInbuiltFunction, liSortedByQuickSort);
            }

            // Since each separate value is so small it's easier to compare if we take the sum of values
            // This way it also is less biased as it is equivalent to taking an average
            TestContext.WriteLine("Total time taken by my QuickSort: {0}ms", timeTakenQuickSort.Sum());
            TestContext.WriteLine("Total time taken by inbuilt sort: {0}ms", timeTakenInbuilt.Sum());
        }
    }
}