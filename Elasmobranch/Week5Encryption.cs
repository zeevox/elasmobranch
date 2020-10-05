using System;
using System.Linq;
using System.Text;

namespace Elasmobranch
{
    public static class Week5Encryption
    {
        private const string AsciiLowercase = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string AsciiUppercase = AsciiLowercase.ToUpper();

        public static string CaesarCipher(string plaintext, int rot, bool squash = false, bool decrypt = false)
        {
            if (decrypt) rot = AsciiLowercase.Length - rot;

            var lowerCaseShifted = AsciiLowercase.Substring(rot) + AsciiLowercase.Substring(0, rot);
            var upperCaseShifted = AsciiUppercase.Substring(rot) + AsciiUppercase.Substring(0, rot);

            var encoded = new StringBuilder();
            foreach (var character in plaintext)
            {
                if (!squash && !char.IsLetter(character))
                {
                    encoded.Append(character);
                }
                else
                {
                    if (char.IsUpper(character))
                    {
                        encoded.Append(upperCaseShifted[character - 65]);
                    }
                    else if (char.IsLower(character))
                    {
                        encoded.Append(lowerCaseShifted[character - 97]);
                    }
                }
            }

            return encoded.ToString();
        }
    }
}