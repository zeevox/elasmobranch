using NUnit.Framework;
using static Elasmobranch.Week5Encryption;

namespace Tests
{
    public class Week5Encryption
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
        // Decryption
        [TestCase("abcd","bcde",  1, false, true)]
        [TestCase("abcd-efgh","cdef-ghij",  2, false, true)]
        [TestCase("abcdefgh","cdefghij",  2, true, true)]
        [TestCase("howdy", "krzgb", 3, false, true)]
        [TestCase("abcdabcdddeebcadd","abcdabcdddeebcadd",  26, false, true)]
        [TestCase("There's-a-starman-waiting-in-the-sky","Wkhuh'v-d-vwdupdq-zdlwlqj-lq-wkh-vnb",  3, false, true)]
        [TestCase("Theresastarmanwaitinginthesky", "Wkhuhvdvwdupdqzdlwlqjlqwkhvnb", 3, true, true)]
        [TestCase("middle-Outz", "okffng-Qwvb", 2, false, true)]
        [TestCase("middleOutz","okffngQwvb",  2, true, true)]
        public void CaesarCipherTest(string expected, string plaintext, int rot, bool squash = false, bool decrypt = false)
        {
            Assert.AreEqual(expected, CaesarCipher(plaintext, rot, squash, decrypt));
        }
    }
}