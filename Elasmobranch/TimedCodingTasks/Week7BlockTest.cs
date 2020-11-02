using System.Collections.Generic;
using System.Linq;

namespace Elasmobranch.TimedCodingTasks
{
    public class Week7BlockTest
    {
        /// <summary>
        ///     Count the number of occurrences of an integer in a given list of integers
        /// </summary>
        /// <param name="source">List of integers</param>
        /// <param name="entry">Integer to search for</param>
        /// <returns>Number of occurrences of entry in source</returns>
        private static int CountOccurrences(List<int> source, int entry)
        {
            var count = 0;
            foreach (var t in source)
                if (t == entry)
                    count++;

            return count;
        }

        /// <summary>
        ///     Given a list of numbers in which two numbers occur once and the rest occur only twice,
        ///     return the sum of the numbers that occur only once.
        /// </summary>
        /// <param name="source">List of numbers</param>
        /// <returns>Sum of the numbers that occur only once in the list</returns>
        public static int Repeats(List<int> source)
        {
            var singularEntries = source.Where(entry => CountOccurrences(source, entry) == 1).ToList();

            return singularEntries[0] + singularEntries[1];
        }

        /// <summary>
        ///     Given a string of words return the length of the shortest words
        /// </summary>
        /// <param name="s">string of space-separated words</param>
        /// <returns>the length of the shortest word</returns>
        public static int FindShort(string s)
        {
            var listOfWords = s.Split(" ").ToList();
            var counts = listOfWords.Select(word => word.Length).ToList();
            return listOfWords[counts.IndexOf(counts.Min(i => i))].Length;
        }

        /// <summary>
        ///     Given an integer n return a jagged array of length n filled with sub-arrays of
        ///     increasing length. The sub-arrays should be filled with ones.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[][] Pyramid(int n)
        {
            var topArray = new int[n][];
            for (var i = 0; i < n; i++)
            {
                var subArray = new int[i+1];
                for (var j = 0; j < subArray.Length; j++) subArray[j] = 1;
                
                topArray[i] = subArray;
            }

            return topArray;
        }
    }
}