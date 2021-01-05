using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Elasmobranch.Queues
{
    public class QueueExercises
    {
        /// <summary>
        ///     We are given the following sequence:
        ///     S1 = N;
        ///     S2 = S1 + 1;
        ///     S3 = 2*S1 + 1;
        ///     S4 = S1 + 2;
        ///     S5 = S2 + 1;
        ///     S6 = 2*S2 + 1;
        ///     S7 = S2 + 2;
        ///     …
        ///     Using the C# inbuilt Queue class, write a program which by given N prints on the
        ///     console the first 50 elements of the sequence.
        ///     Example: N=2 -> 2, 3, 5, 4, 4, 7, 5, 6, 11, 7, 5, 9, 6, …
        /// </summary>
        /// <param name="u1">initial term</param>
        /// <param name="n">number of terms to print</param>
        /// <returns></returns>
        public static int[] SequenceCalculator(int u1, int n)
        {
            var result = new int[n];
            var queue = new Queue<int>();

            // initialisation term
            queue.Enqueue(u1);
            result[0] = u1;

            var value = -1;
            for (var i = 2; i <= n; i++)
            {
                value = (i % 3) switch
                {
                    2 => queue.Peek() + 1,
                    0 => queue.Peek() * 2 + 1,
                    1 => queue.Dequeue() + 2,
                    _ => value
                };

                queue.Enqueue(value);
                result[i - 1] = value;
            }

            return result;
        }

        /// <summary>
        ///     We are given N and M and the following operations:
        ///     N = N+1
        ///     N = N+2
        ///     N = N*2
        ///     Write a program, which finds the shortest subsequence from the
        ///     operations, which starts with N and ends with M. Use queue.
        /// </summary>
        /// <example>
        ///     Example: N = 5, M = 16
        ///     Route: 5 (+2) -> 7 (+1) -> 8 (^2) -> 16
        /// </example>
        /// <param name="start">Start value</param>
        /// <param name="end">Value we should end up at</param>
        /// <returns>The route taken to get from <paramref name="start" /> to <paramref name="end" /></returns>
        /// <exception cref="ArgumentException">the end value must be larger than the start value</exception>
        public static int[] CalculatePath(int start, int end)
        {
            if (end <= start) throw new ArgumentException("Final value cannot be smaller than or equal to start value");

            var queue = new Queue<PathElement>();
            queue.Enqueue(new PathElement(null, start));

            var route = new List<int>();
            PathElement finalElement;

            // Ideally there would be code that would break out of this loop if a route
            // cannot be found between the start and end numbers. However, this is not
            // necessary here because one of the given operations is +1, so as long as 
            // start < end, there will be a route.
            while (true)
            {
                var previous = queue.Dequeue();
                var values = new[] {previous.Value + 1, previous.Value + 2, previous.Value * 2};
                foreach (var value in values)
                    if (value < end)
                    {
                        queue.Enqueue(previous.Next(value));
                    }
                    else if (value == end)
                    {
                        finalElement = previous.Next(value);
                        route.Add(value);
                        goto RouteFound;
                    }
            }

            RouteFound:
            do
            {
                finalElement = finalElement.Previous;
                route.Insert(0, finalElement.Value);
            } while (finalElement.Previous != null);

            return route.ToArray();
        }

        /// <summary>
        ///     An integer that links back to another <see cref="PathElement" />
        /// </summary>
        private class PathElement
        {
            public readonly PathElement Previous;
            public readonly int Value;

            public PathElement([AllowNull] PathElement previous, int value)
            {
                Previous = previous;
                Value = value;
            }

            /// <summary>
            ///     Generate a new <see cref="PathElement" /> that links back to this one
            /// </summary>
            /// <param name="value">The integer value of the new element</param>
            /// <returns>A PathElement that links back to this one</returns>
            public PathElement Next(int value)
            {
                return new PathElement(this, value);
            }
        }
    }
}