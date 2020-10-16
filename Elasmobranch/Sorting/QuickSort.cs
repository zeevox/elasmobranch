using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasmobranch.Sorting
{
    public static class QuickSort
    {
        /// <summary>
        ///     Return the minimum value in a List of comparable values
        /// </summary>
        /// <param name="li">List to search through</param>
        /// <returns>Minimum value in list</returns>
        private static IComparable MyMin(List<IComparable> li)
        {
            var minimum = li[0];
            foreach (var i in li)
                if (i.CompareTo(minimum) < 0)
                    minimum = i;

            return minimum;
        }

        /// <summary>
        ///     Basic sorting algorithm, works great on small datasets
        ///     Iterate over the list n^2 times in total
        /// </summary>
        /// <param name="l">List to sort</param>
        /// <returns>Sorted list</returns>
        private static List<IComparable> CustomSort(List<IComparable> l)
        {
            // Shallow copy the list
            var li = new List<IComparable>(l);
            // `li_out` will become the sorted list
            var liOut = new List<IComparable>();
            // Iterate n times over the list
            foreach (var item in l)
            {
                // The my_min function also iterates n times over the list
                var minimum = MyMin(li);
                liOut.Add(minimum);
                li.Remove(minimum);
            }

            return liOut;
        }

        /// <summary>
        ///     Splits the list into positive and negative numbers. If there
        ///     are a roughly equal number of both we halve the running time
        ///     The more list entries the more pronounced the difference
        ///     between `CustomSort` and `CustomSortPositiveNegative` is.
        /// </summary>
        /// <param name="l">The list to sort</param>
        /// <returns>Sorted list</returns>
        private static List<IComparable> CustomSortPositiveNegative(List<IComparable> l)
        {
            // Shallow copy the list
            var li = new List<IComparable>(l);

            var liNegative = new List<IComparable>();
            var liPositive = new List<IComparable>();

            // First we go through the list once and separate it into a positive and negative half
            foreach (var item in li)
                // CompareTo returns -1 if item < obj, 0 if item == obj, 1 if item > obj
                if (item.CompareTo(0) < 0) // we assume zero is "positive"
                    liNegative.Add(item);
                else
                    liPositive.Add(item);

            // sort the two lists individually
            var liNegativeSorted = CustomSort(liNegative);
            var liPositiveSorted = CustomSort(liPositive);

            // append the positive list to the negative one
            liNegativeSorted.AddRange(liPositiveSorted);

            // return the combined two lists
            return liNegativeSorted;
        }

        /// <summary>
        ///     Sort a list of numbers using the quicksort algorithm
        ///     Source: https://www.khanacademy.org/computing/computer-science/algorithms/quick-sort/a/overview-of-quicksort
        /// </summary>
        /// <param name="l">The list to sort</param>
        public static List<IComparable> Sort(List<IComparable> l)
        {
            // # If it is a very short list we discontinue quicksort
            // (The magic number 4 comes from https://code.woboq.org/userspace/glibc/stdlib/qsort.c.html#_M/MAX_THRESH)
            if (l.Count < 4) return CustomSortPositiveNegative(l);

            // Clone the list
            var li = new List<IComparable>(l);

            var pivot = li[^1];

            var left = new List<IComparable>();
            var right = new List<IComparable>();

            foreach (var value in li.Take(li.Count - 1))
                // If the value is less than or equal to the pivot, insert it on the left of the list
                if (value.CompareTo(pivot) <= 0)
                    left.Add(value);
                else if (value.CompareTo(pivot) > 0) right.Add(value);

            // combine [left] + [pivot] + [right]
            var sortedList = Sort(left);
            sortedList.Add(pivot);
            sortedList.AddRange(Sort(right));

            return sortedList;
        }
    }
}