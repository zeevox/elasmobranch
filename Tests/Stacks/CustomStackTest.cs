using System;
using Elasmobranch.Stacks;
using NUnit.Framework;

namespace Tests.Stacks
{
    public class CustomStackTest
    {
        [Test]
        public void StackPushPopTest()
        {
            var stack = new CustomStack<int>(20);
            
            stack.Push(5);
            stack.Push(20);
            Assert.AreEqual(20, stack.Pop());
            stack.Push(500);
            stack.Push(20);
            Assert.AreEqual(20, stack.Pop());
            Assert.AreEqual(500, stack.Pop());
            Assert.AreEqual(5, stack.Pop());
        }

        [Test]
        public void StackUnderflowTest()
        {
            var stack = new CustomStack<int>(5);
            
            var errorThrown = false;
            try
            {
                stack.Pop();
            }
            catch (Exception)
            {
                errorThrown = true;
            }
            
            Assert.AreEqual(true, errorThrown);
        }

        [Test]
        public void StackOverflowTest()
        {
            var stack = new CustomStack<int>(5);

            var errorThrown = false;
            try
            {
                for (var i = 0; i <= stack.MaxHeight; i++)
                {
                    stack.Push(5);
                }
            }
            catch (Exception)
            {
                errorThrown = true;
            }
            
            Assert.AreEqual(true, errorThrown);
        }
    }
}