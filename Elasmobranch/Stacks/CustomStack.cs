namespace Elasmobranch.Stacks
{
    /// <summary>
    /// A bare-bones stack implementation in C#
    /// </summary>
    /// <typeparam name="T">Anything</typeparam>
    public class CustomStack<T>
    {
        private readonly T[] _stack;
        private int _stackPointer = -1;

        public CustomStack(int height) => _stack = new T[height];

        /// <remarks>
        /// When popping from the stack we decrement the pointer.
        /// There is no need to remove the element at the popped position.
        /// </remarks>
        /// <returns>The element at the top of the stack</returns>
        public T Pop() => _stack[_stackPointer--];

        public void Push(T input) => _stack[++_stackPointer] = input;

        public int MaxHeight => _stack.Length;
    }
}