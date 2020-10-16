using Elasmobranch.Encryption;
using NUnit.Framework;

namespace Tests.Encryption
{
    public class CaesarCipherTests
    {
        [Test]
        // Encryption
        [TestCase("bcde", "abcd", 1, false)]
        [TestCase("cdef-ghij", "abcd-efgh", 2, false)]
        [TestCase("cdefghij", "abcd-efgh", 2, true)]
        [TestCase("krzgb", "howdy", 3)]
        [TestCase("abcdabcdddeebcadd", "abcdabcdddeebcadd", 26)]
        [TestCase("Wkhuh'v-d-vwdupdq-zdlwlqj-lq-wkh-vnb", "There's-a-starman-waiting-in-the-sky", 3)]
        [TestCase("Wkhuhvdvwdupdqzdlwlqjlqwkhvnb", "There's-a-starman-waiting-in-the-sky", 3, true)]
        [TestCase("okffng-Qwvb", "middle-Outz", 2)]
        [TestCase("okffngQwvb", "middle-Outz", 2, true)]
        public void CaesarCipherTest(string expected, string plaintext, int rot, bool squash = false)
        {
            Assert.AreEqual(expected, CaesarCipher.Encipher(plaintext, rot, squash));
        }

        [Test]
        // Decryption
        [TestCase("abcd", "bcde", 1, false)]
        [TestCase("abcd-efgh", "cdef-ghij", 2, false)]
        [TestCase("abcdefgh", "cdefghij", 2, true)]
        [TestCase("howdy", "krzgb", 3, false)]
        [TestCase("abcdabcdddeebcadd", "abcdabcdddeebcadd", 26, false)]
        [TestCase("There's-a-starman-waiting-in-the-sky", "Wkhuh'v-d-vwdupdq-zdlwlqj-lq-wkh-vnb", 3, false)]
        [TestCase("Theresastarmanwaitinginthesky", "Wkhuhvdvwdupdqzdlwlqjlqwkhvnb", 3, true)]
        [TestCase("middle-Outz", "okffng-Qwvb", 2, false)]
        [TestCase("middleOutz", "okffngQwvb", 2, true)]
        public void CaesarDecipherTest(string expected, string ciphered, int rot, bool squash = false)
        {
            Assert.AreEqual(expected, CaesarCipher.Decipher(ciphered, rot, squash));
        }
    }
}