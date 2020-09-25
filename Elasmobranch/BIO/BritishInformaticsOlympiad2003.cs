using System.Collections.Generic;
using System.Text;

namespace Elasmobranch.BIO
{
    /// <summary>
    ///     2003 British Informatics Olympiad paper implementation in C#
    ///     Original paper available for free viewing at:
    ///     https://www.olympiad.org.uk/papers/2003/bio/bio03ex.pdf
    /// </summary>
    public static class BritishInformaticsOlympiad2003
    {
        /// <summary>
        ///     Question 1 is all about the International Standard Book Number check digit system
        /// </summary>
        /// <param name="isbn">A string representing an 11-digit ISBN number with a ? for a missing digit</param>
        /// <returns>The missing digit</returns>
        public static string FindMissingNumber(string isbn)
        {
            var counter = 0;
            var isbnSum = 0;
            var missingNumberPosition = 0;
            foreach (var i in isbn)
            {
                if (i.Equals("?".ToCharArray()[0]))
                    missingNumberPosition = counter;
                else if (i.Equals("X".ToCharArray()[0]))
                    isbnSum += (10 - counter) * 10;
                else
                    isbnSum += (10 - counter) * (int) char.GetNumericValue(i);

                counter += 1;
            }

            for (var j = 0; j < 11; j++)
                if ((isbnSum + (10 - missingNumberPosition) * j) % 11 == 0)
                    return j == 10 ? "X" : j.ToString();

            // default return value, theoretically we should never get here
            return "?";
        }

        /// <summary>
        ///     Validate a given International Standard Book Number
        /// </summary>
        /// <param name="isbn">ISBN to check</param>
        /// <returns>true or false, depending on whether the given ISBN is valid</returns>
        public static bool IsValidBookNumber(string isbn)
        {
            var counter = 0;
            var isbnSum = 0;
            foreach (var i in isbn)
            {
                if (i.Equals("X".ToCharArray()[0]))
                    isbnSum += (10 - counter) * 10;
                else
                    isbnSum += (10 - counter) * (int) char.GetNumericValue(i);

                counter += 1;
            }

            return isbnSum % 11 == 0;
        }

        /// <summary>
        ///     Swap two characters in a string given their positions in the string
        /// </summary>
        /// <param name="str">The string in which we are swapping characters</param>
        /// <param name="i">The position of one of the characters to be swapped</param>
        /// <param name="j">The position of the other character to be swapped</param>
        /// <returns>The same string as the input but with str[i] and str[j] swapped</returns>
        public static string SwapPositionsInString(string str, int i, int j)
        {
            var stringBuilder = new StringBuilder(str);

            var temp = stringBuilder[i];
            stringBuilder[i] = stringBuilder[j];
            stringBuilder[j] = temp;

            return stringBuilder.ToString();
        }

        /// <summary>
        /// From the BIO 2003, question 1(c)
        /// A valid ISBN has two of its digits swapped and the resulting code is 3201014525. What are the possible values of the valid ISBN?
        /// This is small function that, given an ISBN, returns valid ISBNs that have two of the digits in the original one swapped
        /// </summary>
        /// <param name="isbn">The given ISBN number that we are analysing</param>
        /// <returns>A list of valid ISBN numbers</returns>
        public static List<string> ValidNumbersWithSwappedDigits(string isbn)
        {
            var validNumbers = new List<string>();
            for (var i = 0; i < isbn.Length; i++)
            for (var j = 0; j < isbn.Length; j++)
            {
                // use a small helping function to swap the characters at positions i and j in the string
                var swappedString = SwapPositionsInString(isbn, i, j);

                // if the ISBN that we tried with swapped digits is actually a valid ISBN, add it to the list of such ISBNs
                if (IsValidBookNumber(swappedString) && !validNumbers.Contains(swappedString))
                    validNumbers.Add(swappedString);
            }

            return validNumbers;
        }
    }
}