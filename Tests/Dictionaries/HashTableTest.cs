using Elasmobranch.Dictionaries;
using NUnit.Framework;

namespace Tests.Dictionaries
{
    public class HashTableTest
    {
        [Test]
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