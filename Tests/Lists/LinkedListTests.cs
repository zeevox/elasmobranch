using System;
using Elasmobranch.Lists;
using NUnit.Framework;

namespace Tests.Lists
{
    [TestFixture]
    public class LinkedListTests
    {
        [SetUp]
        public void SetUp()
        {
            _linkedList = new LinkedList<int>();
            foreach (var item in SampleData)
                _linkedList.Add(item);
        }

        private LinkedList<int> _linkedList;
        private static readonly int[] SampleData = {6, 23, 5, 5, 2, 5, 325, 53, 2, 35};

        [Test]
        public void ToStringTest()
        {
            TestContext.WriteLine(_linkedList.ToString());
        }

        [Test]
        public void Add()
        {
            for (var i = 0; i < SampleData.Length; i++) Assert.AreEqual(SampleData[i], _linkedList[i]);

            _linkedList.Add(5);
            Assert.AreEqual(5, _linkedList[SampleData.Length]);
        }

        [Test]
        public void Clear()
        {
            _linkedList.Clear();
            Assert.AreEqual(0, _linkedList.Count);
        }

        [Test]
        public void Contains()
        {
            foreach (var item in SampleData)
                Assert.IsTrue(_linkedList.Contains(item));
            Assert.IsFalse(_linkedList.Contains(1000));
            Assert.IsFalse(_linkedList.Contains(-1));
        }

        [Test]
        public void CopyTo()
        {
            var testArray = new int[20];
            _linkedList.CopyTo(testArray, 10);
            Assert.AreEqual(new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 23, 5, 5, 2, 5, 325, 53, 2, 35}, testArray);
        }

        [Test]
        public void Remove()
        {
            Assert.IsTrue(_linkedList.Remove(5));
            Assert.IsTrue(_linkedList.Contains(5));
            Assert.IsFalse(_linkedList.Remove(1000));
            Assert.IsTrue(_linkedList.Remove(6));
            Assert.IsFalse(_linkedList.Contains(6));
            Assert.AreEqual(8, _linkedList.Count);
        }

        [Test]
        public void Count()
        {
            Assert.AreEqual(10, _linkedList.Count);
        }

        [Test]
        public void IsReadOnly()
        {
            Assert.DoesNotThrow(delegate { _linkedList.Add(5); });
            Assert.DoesNotThrow(delegate { _linkedList.Insert(5, 5); });
            Assert.DoesNotThrow(delegate { _linkedList.Remove(5); });
            Assert.DoesNotThrow(delegate { _linkedList.RemoveAt(5); });
            Assert.DoesNotThrow(delegate { _linkedList.Clear(); });
            Assert.IsFalse(_linkedList.IsReadOnly);

            _linkedList.IsReadOnly = true;
            Assert.IsTrue(_linkedList.IsReadOnly);

            Assert.Throws<NotSupportedException>(delegate { _linkedList.Add(5); });
            Assert.Throws<NotSupportedException>(delegate { _linkedList.Clear(); });
            Assert.Throws<NotSupportedException>(delegate { _linkedList.Insert(5, 5); });
            Assert.Throws<NotSupportedException>(delegate { _linkedList.Remove(5); });
            Assert.Throws<NotSupportedException>(delegate { _linkedList.RemoveAt(5); });
        }

        [Test]
        public void IndexOf()
        {
            Assert.AreEqual(0, _linkedList.IndexOf(6));
            Assert.AreEqual(2, _linkedList.IndexOf(5));
        }

        [Test]
        public void Insert()
        {
            Assert.AreEqual(5, _linkedList[5]);
            _linkedList.Insert(5, -5);
            Assert.AreEqual(-5, _linkedList[5]);
            Assert.AreEqual(11, _linkedList.Count);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { _linkedList.Insert(-5, 5); });
        }

        [Test]
        public void RemoveAt()
        {
            _linkedList.RemoveAt(5);
            Assert.AreEqual(325, _linkedList[5]);
            Assert.Throws<ArgumentOutOfRangeException>(delegate { _linkedList.RemoveAt(-1); });
            Assert.Throws<ArgumentOutOfRangeException>(delegate { _linkedList.RemoveAt(1000); });
        }

        [Test]
        public void UpdateAt()
        {
            Assert.AreEqual(5, _linkedList[5]);
            _linkedList[5] = -5;
            Assert.AreEqual(-5, _linkedList[5]);
        }
    }
}