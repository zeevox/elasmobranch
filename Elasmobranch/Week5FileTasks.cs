using System;
using System.Collections.Generic;
using System.IO;

namespace Elasmobranch
{
    public static class Week5FileTasks
    {
        /// <summary>
        /// Write a line to a text file on the disk, if the file already exists, append the sentence to the end
        /// </summary>
        /// <param name="line">The line to write</param>
        /// <param name="filename">Optional filename (default: sentences.txt)</param>
        public static void WriteLine(string line, string filename = "sentences.txt") {
            if (!File.Exists(filename)) File.CreateText(filename).Close();
            // use the `using` statement which will automatically dispose and close file when necessary
            using var streamWriter = File.AppendText(filename);
            streamWriter.WriteLine(line);
        }

        /// <summary>
        /// Method to generate a given number of random integers (within a given range), writing them to a file, one per line
        /// </summary>
        /// <param name="length">Optional The number of lines / integers to generate</param>
        /// <param name="rangeStart">Optional minimum of range</param>
        /// <param name="rangeEnd">Optional maximum of range</param>
        /// <param name="filename">Optional filename to output to</param>
        public static void GenerateTextFileOfRandomNumbers(int length = 1000, int rangeStart = 1000, int rangeEnd = 9999,
            string filename = "random_numbers_unsorted.txt")
        {
            var random = new Random();
            using var streamWriter = File.CreateText(filename);
            for (var i = 0; i < length; i++)
            {
                streamWriter.WriteLine(random.Next(rangeStart, rangeEnd).ToString());
            }
        }

        /// <summary>
        /// Read in a text file where each line is an integer and output a text file with the same integers, but sorted
        /// </summary>
        /// <param name="filenameInput"></param>
        /// <param name="filenameOutput"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void SortTextFile(string filenameInput = "random_numbers_unsorted.txt",
            string filenameOutput = "random_numbers_sorted.txt")
        {
            if (!File.Exists(filenameInput)) throw new FileNotFoundException();
            using var streamReader = File.OpenText(filenameInput);
            // read all the lines of the text file as integers into the list
            var listOfNumbers = new List<IComparable>();
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                listOfNumbers.Add(int.Parse(line));
            }

            using var streamWriter = File.CreateText(filenameOutput);
            foreach (var integer in Week3Sorting.QuickSort(listOfNumbers))
            {
                streamWriter.WriteLine(integer.ToString());
            }
        }
    }
}