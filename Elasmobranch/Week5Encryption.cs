using System;
using System.Linq;
using System.Text;

namespace Elasmobranch
{
    public static class Week5Encryption
    {
        /// <summary>
        /// Functions relating to the Caesar Cipher
        /// </summary>
        public static class CaesarCipher
        {
            private const string AsciiLowercase = "abcdefghijklmnopqrstuvwxyz";
            private static readonly string AsciiUppercase = AsciiLowercase.ToUpper();

            /// <summary>
            /// Apply the Caesar cipher to a string of text
            /// </summary>
            /// <param name="input">The input text</param>
            /// <param name="rot">The rotation of the Caesar Cipher</param>
            /// <param name="squash">Whether to leave punctuation included</param>
            /// <param name="decrypt">Whether to run it in reverse, i.e. decipher instead of cipher</param>
            /// <returns>The ciphered text</returns>
            private static string RunCipher(string input, int rot, bool squash = false, bool decrypt = false)
            {
                if (decrypt) rot = AsciiLowercase.Length - rot;

                var encoded = new StringBuilder();
                foreach (var character in input.Where(character => !squash || char.IsLetter(character)))
                {
                    encoded.Append(EncipherSingleChar(character, rot));
                }

                return encoded.ToString();
            }

            /// <summary>
            /// Apply the Caesar cipher to a string of text
            /// </summary>
            /// <param name="plaintext">The input text</param>
            /// <param name="rot">The rotation of the Caesar Cipher</param>
            /// <param name="squash">Whether to leave punctuation included</param>
            /// <returns>The ciphered text</returns>
            public static string Encipher(string plaintext, int rot, bool squash = false)
            {
                return RunCipher(plaintext, rot, squash);
            }

            /// <summary>
            /// Decipher a Caesar ciphered string of text
            /// </summary>
            /// <param name="ciphered">The input ciphered text</param>
            /// <param name="rot">The rotation of the Caesar Cipher</param>
            /// <param name="squash">Whether to leave punctuation included</param>
            /// <returns>The deciphered text</returns>
            public static string Decipher(string ciphered, int rot, bool squash = false)
            {
                return RunCipher(ciphered, rot, squash, true);
            }

            /// <summary>
            /// Caesar Cipher shift a single character
            /// </summary>
            /// <param name="character">Input character</param>
            /// <param name="rot">Number of places to shift by</param>
            /// <returns>Ciphered character</returns>
            public static char EncipherSingleChar(char character, int rot)
            {
                var lowerCaseShifted = AsciiLowercase.Substring(rot) + AsciiLowercase.Substring(0, rot);
                var upperCaseShifted = AsciiUppercase.Substring(rot) + AsciiUppercase.Substring(0, rot);

                if (char.IsLetter(character))
                {
                    return char.IsUpper(character)
                        ? upperCaseShifted[character - 65]
                        : lowerCaseShifted[character - 97];
                }

                return character;
            }
        }

        /// <summary>
        /// Functions relating to the Vigenère Cipher
        /// </summary>
        public static class VigenereCipher
        {
            /// <summary>
            /// Encipher a string of plaintext using the Vigenère cipher
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
                {
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
                }

                return encoded.ToString();
            }
        }
    }
}