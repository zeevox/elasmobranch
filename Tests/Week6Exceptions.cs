using NUnit.Framework;
using static Elasmobranch.Week6Exceptions;

namespace Tests
{
    public class Week6Exceptions
    {
        [Test]
        [TestCase("https://zeevox.net/images/avatar_122px.webp")]
        [TestCase("http://zeevox.net/images/avatar_122px.webp")]
        [TestCase("hsts://zeevox.net/images/avatar_122px.webp")]
        [TestCase("http://example.org/images/avatar_122px.webp")]
        [TestCase("http://abysmalgreenparrot.com/images/parrot.jpg")]
        public void FileDownloadTest(string url)
        {
            DownloadFile(url);
        }
    }
}