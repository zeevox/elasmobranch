using Elasmobranch.Dictionaries;
using NUnit.Framework;

namespace Tests.Dictionaries
{
    [TestFixture]
    public class HashTableTest
    {
        [Test]
        [Ignore("HashTable implementation incomplete")]
        public void HashDistributionTest()
        {
            var frequencies = new int[100];
            for (var i = 0; i < 10000; i++)
            {
                frequencies[HashTable<int,int>.ComputeHash(i) % 100] += 1;
            }
            
        }
    }
}