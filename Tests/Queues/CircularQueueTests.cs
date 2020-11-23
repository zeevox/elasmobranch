using System;
using Elasmobranch.Queues;
using NUnit.Framework;

namespace Tests.Queues
{
    public class CircularQueueTests
    {
        [Test]
        public void FixedSizeCircularQueueTest()
        {
            var circularQueue = new CircularQueue<int>(5);

            circularQueue.Write(3);
            circularQueue.Write(2);

            Assert.AreEqual(3, circularQueue.Peek());
            Assert.AreEqual(3, circularQueue.Read());

            circularQueue.Write(6);
            circularQueue.Write(7);
            circularQueue.Write(33);
            circularQueue.Write(22);

            Assert.AreEqual(2, circularQueue.Peek());

            circularQueue.Write(57);

            // oldest element overwritten so we expect 6
            Assert.AreEqual(6, circularQueue.Read());

            circularQueue.Write(17);

            Assert.AreEqual(7, circularQueue.Read());
            Assert.AreEqual(33, circularQueue.Read());
            Assert.AreEqual(22, circularQueue.Read());
            Assert.AreEqual(57, circularQueue.Read());
            Assert.AreEqual(17, circularQueue.Read());

            var errorThrown = false;
            try
            {
                circularQueue.Read();
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.Message);
                errorThrown = true;
            }

            Assert.AreEqual(true, errorThrown);
        }

        [Test]
        public void DynamicSizeCircularQueueTest()
        {
            var circularQueue = new CircularQueue<int>();

            // default initial queue size for a dynamic size circular queue = 4
            Assert.AreEqual(4, circularQueue.Size());

            circularQueue.Write(2);
            circularQueue.Write(3);
            circularQueue.Write(4);
            
            Assert.AreEqual(4, circularQueue.Size());
            Assert.AreEqual(2, circularQueue.Peek());
            
            circularQueue.Write(5);
            circularQueue.Write(6);
            
            Assert.AreEqual(8, circularQueue.Size());
            Assert.AreEqual(2, circularQueue.Read());
            Assert.AreEqual(3, circularQueue.Read());
            
            circularQueue.Write(7);
            circularQueue.Write(8);
            circularQueue.Write(9);
            circularQueue.Write(10);
            
            Assert.AreEqual(8, circularQueue.Size());
            Assert.AreEqual(4, circularQueue.Peek());
        }
    }
}