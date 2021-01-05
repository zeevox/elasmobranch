using Elasmobranch.Encryption;
using NUnit.Framework;

namespace Tests.Encryption
{
    public class VigenereCipherTests
    {
        [Test]
        [TestCase("alpvsuhypn", "helloworld", "thekey")]
        [TestCase("ashyspvftgocsw", "he's gone bananas", "topsecret")]
        [TestCase("Vyc fnqkm spdpv nqo hjfxa qmcg mpqtkctg tibp bdza.",
            "The quick brown fox jumps over thirteen lazy dogs.", "cryptii", false)]
        [TestCase("Vycfnqkmspdpvnqohjfxaqmcgmpqtkctgtibpbdza", "The quick brown fox jumps over thirteen lazy dogs.",
            "cryptii")]
        [TestCase("Bggafyqrh xs mzg srpar xora sg ezuooqso, xlqfz qwo'x ezm ric b fiq gcwymh fq owti us jxm.",
            "According to all known laws of aviation, there isn't any way a bee should be able to fly.", "beemovie",
            false)]
        [TestCase("Bggafyqrhxsmzgsrparxorasgezuooqsoxlqfzqwoxezmricbfiqgcwymhfqowtiusjxm",
            "According to all known laws of aviation, there isn't any way a bee should be able to fly.", "beemovie")]
        [TestCase("\"Vx's ezr mfrm\" – jr grzpb!", "\"It's not true\" – he cried!", "nEaRlYtOXiC", false)]
        [TestCase("Vxsezrmfrmjrgrzpb", "\"It's not true\" – he cried!", "nEaRlYtOXiC")]
        public void VigenereCipherTest(string expected, string plaintext, string keyword, bool squash = true)
        {
            Assert.AreEqual(expected, VigenereCipher.Encipher(plaintext, keyword, squash));
        }
    }
}