using System.Text;

namespace Elasmobranch.TimedCodingTasks
{
    /// <summary>
    ///     20 minutes to code an answer to Section B of AQA 7517 Specimen Paper
    ///     Available for viewing at https://store.aqa.org.uk/resources/computing/AQA-75171-SQP.PDF
    /// </summary>
    public static class Week6TimedCodingTask
    {
        public static string ConvertDecimalToBinaryReversed(int dec)
        {
            var remainders = new StringBuilder();
            do
            {
                remainders.Append((dec % 2).ToString());
                dec = (dec - dec % 2) / 2;
            } while (dec > 0);

            return remainders.ToString();
        }

        public static string ConvertDecimalToBinary(int dec)
        {
            var reversed = ConvertDecimalToBinaryReversed(dec);
            var result = new StringBuilder();
            for (var i = reversed.Length - 1; i >= 0; i--) result.Append(reversed[i]);

            return result.ToString();
        }
    }
}