using System.Linq;
using System.Text;

namespace Elasmobranch.Encryption
{
    /// <summary>
    ///     Functions relating to the Vigenère Cipher
    /// </summary>
    public static class VigenereCipher
    {
        /// <summary>
        ///     Encipher a string of plaintext using the Vigenère cipher
        /// </summary>
        /// <param name="plaintext">The text to be ciphered</param>
        /// <param name="keyword">The key to use for the Vigenère cipher</param>
        /// <param name="squash">Whether to strip non-alphabet characters from the output</param>
        /// <returns>The input enciphered with the Vigenère cipher</returns>
        public static string Encipher(string plaintext, string keyword, bool squash = true)
        {
            // convert the key into a char array, chars can also be used as numbers
            // uppercase so that they are homogeneously same case so we do not have to consider cases (lower/upper)
            var keywordShifts = keyword.ToUpper().ToCharArray();

            // Strip punctuation at the very start (by default)
            if (squash) plaintext = new string(plaintext.Where(char.IsLetter).ToArray());

            var encoded = new StringBuilder();

            // count the number of characters that we did not cipher due to them being non-alphabet chars
            // this is necessary to prevent the key going off-place if we are leaving punctuation in the output
            var squashCount = 0;

            for (var i = 0; i < plaintext.Length; i++)
                if (!char.IsLetter(plaintext[i]))
                {
                    squashCount += 1;
                    encoded.Append(plaintext[i]);
                }
                else
                {
                    encoded.Append(CaesarCipher.EncipherSingleChar(plaintext[i],
                        keywordShifts[(i - squashCount) % keywordShifts.Length] - 65));
                }

            return encoded.ToString();
        }
    }
}