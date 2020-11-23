using System;

namespace Elasmobranch.Queues
{
    /// <summary>
    ///     <a href="https://en.wikipedia.org/wiki/Circular_buffer">Circular Queue</a> implementation that allows for
    ///     dynamically growing when full.
    /// </summary>
    /// <typeparam name="T">Anything</typeparam>
    public class CircularQueue<T>
    {
        /// <summary>
        ///     Determine whether, when the queue is full, to grow the queue or to loop back around the the start
        /// </summary>
        private readonly bool _growDynamically;

        // start at 2² so that subsequent doubling keeps to powers of two
        private T[] _data = new T[4];

        // track the front and rear of the queue
        private int _rear;
        private int _front;

        /// <summary>
        ///     Initialise a circular queue that grows dynamically as elements are added
        /// </summary>
        public CircularQueue()
        {
            _growDynamically = true;
        }

        /// <summary>
        ///     Initialise a circular queue of fixed size
        /// </summary>
        /// <param name="length">the size of the queue</param>
        public CircularQueue(int length)
        {
            _data = new T[length];
            _growDynamically = false;
        }

        /// <summary>
        ///     Add a new element at the front of the queue
        /// </summary>
        /// <param name="value">A value of type <see cref="T" /> to be written</param>
        public void Write(T value)
        {
            if (IsFull())
            {
                if (_growDynamically)
                {
                    // create a new array of twice the size
                    var newData = new T[_data.Length * 2];
                    
                    // copy data from the current array into the new one
                    var splitPoint = _front % _data.Length;
                    Array.Copy(_data, newData, splitPoint);
                    Array.Copy(_data, splitPoint, newData, splitPoint + _data.Length, _data.Length - splitPoint);
                    
                    // if the rear of the queue has now been shifted due to the growing appropriately updated the pointer
                    if (_rear % _data.Length > splitPoint) _rear += _data.Length;
                    
                    // replace the old list with the new one
                    _data = newData;
                }
                else
                {
                    // If it is a fixed size list and it is full, increment start
                    // pointer since are now about to overwrite the oldest element
                    _rear++;
                }
            }
            
            // Save the value at the front of the queue
            _data[_front++ % _data.Length] = value;
        }

        /// <summary>
        ///     Read the oldest element and remove it from the queue
        /// </summary>
        /// <returns>The oldest element in the queue</returns>
        public T Read()
        {
            if (_rear == _front) throw new Exception("Queue is empty");
            return _data[_rear++ % _data.Length];
        }

        /// <summary>
        ///     Read the oldest element from the queue but do not remove it
        /// </summary>
        /// <returns>THe oldest element in the queue</returns>
        public T Peek()
        {
            if (_rear == _front) throw new Exception("Queue is empty");
            return _data[_rear % _data.Length];
        }

        /// <summary>
        /// Get the size of the queue
        /// </summary>
        /// <returns>The size of the queue</returns>
        public int Size() => _data.Length;

        /// <summary>
        /// Find out whether this circular queue has been maximally filled
        /// </summary>
        /// <returns>Whether the queue is full</returns>
        private bool IsFull() => _front != _rear && _front % _data.Length == _rear % _data.Length;
    }
}