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
        public void CaesarCipherTest(string expected, string plaintext, int rot, bool squash = false)
        {
            Assert.AreEqual(expected, CaesarCipher.Encipher(plaintext, rot, squash));
        }

        [Test]
        // Decryption
        [TestCase("abcd","bcde",  1, false)]
        [TestCase("abcd-efgh","cdef-ghij",  2, false)]
        [TestCase("abcdefgh","cdefghij",  2, true)]
        [TestCase("howdy", "krzgb", 3, false)]
        [TestCase("abcdabcdddeebcadd","abcdabcdddeebcadd",  26, false)]
        [TestCase("There's-a-starman-waiting-in-the-sky","Wkhuh'v-d-vwdupdq-zdlwlqj-lq-wkh-vnb",  3, false)]
        [TestCase("Theresastarmanwaitinginthesky", "Wkhuhvdvwdupdqzdlwlqjlqwkhvnb", 3, true)]
        [TestCase("middle-Outz", "okffng-Qwvb", 2, false)]
        [TestCase("middleOutz","okffngQwvb",  2, true)]
        public void CaesarDecipherTest(string expected, string ciphered, int rot, bool squash = false)
        {
            Assert.AreEqual(expected, CaesarCipher.Decipher(ciphered, rot, squash));
        }

        [Test]
        [TestCase("alpvsuhypn", "helloworld", "thekey")]
        [TestCase("ashyspvftgocsw", "he's gone bananas", "topsecret")]
        [TestCase("Vyc fnqkm spdpv nqo hjfxa qmcg mpqtkctg tibp bdza.", "The quick brown fox jumps over thirteen lazy dogs.", "cryptii", false)]
        [TestCase("Vycfnqkmspdpvnqohjfxaqmcgmpqtkctgtibpbdza", "The quick brown fox jumps over thirteen lazy dogs.", "cryptii")]
        [TestCase("Bggafyqrh xs mzg srpar xora sg ezuooqso, xlqfz qwo'x ezm ric b fiq gcwymh fq owti us jxm.", "According to all known laws of aviation, there isn't any way a bee should be able to fly.", "beemovie", false)]
        [TestCase("Bggafyqrhxsmzgsrparxorasgezuooqsoxlqfzqwoxezmricbfiqgcwymhfqowtiusjxm", "According to all known laws of aviation, there isn't any way a bee should be able to fly.", "beemovie")]
        [TestCase("\"Vx's ezr mfrm\" – jr grzpb!", "\"It's not true\" – he cried!", "nEaRlYtOXiC", false)]
        [TestCase("Vxsezrmfrmjrgrzpb", "\"It's not true\" – he cried!", "nEaRlYtOXiC")]
        public void VigenereCipherTest(string expected, string plaintext, string keyword, bool squash = true)
        {
            Assert.AreEqual(expected, VigenereCipher.Encipher(plaintext, keyword, squash));
        }
    }
}