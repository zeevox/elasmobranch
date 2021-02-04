using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// ReSharper disable LoopCanBeConvertedToQuery

namespace Elasmobranch.Lists
{
    /// <summary>
    ///     Represents a linear collection of data elements whose order is not given by their physical placement in memory.
    ///     Instead, each element points to the next.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class LinkedList<T> : IList<T>
    {
        private Node _head;
        private Node _tail;

        /// <summary>
        ///     Gets the number of items in the <see cref="LinkedList{T}" />.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Adds an item to the <see cref="LinkedList{T}" />
        /// </summary>
        /// <param name="item">The object to add to the <see cref="LinkedList{T}" /></param>
        /// <exception cref="NotSupportedException">The <see cref="LinkedList{T}" /> is read-only.</exception>
        /// <seealso cref="IsReadOnly" />
        public void Add(T item)
        {
            CheckReadOnly();

            if (_tail == null)
            {
                _tail = new Node(item, null);
                _head = _tail;
            }
            else
            {
                var node = new Node(item, null);
                _head.Pointer = node;
                _head = node;
            }

            Count += 1;
        }

        /// <summary>
        ///     Removes all items from the <see cref="LinkedList{T}" />
        /// </summary>
        /// <exception cref="NotSupportedException">The <see cref="LinkedList{T}" /> is read-only.</exception>
        /// <seealso cref="IsReadOnly" />
        public void Clear()
        {
            CheckReadOnly();

            _tail = null;
            _head = null;

            Count = 0;
        }

        /// <summary>
        ///     Determines whether the <see cref="LinkedList{T}" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in <see cref="LinkedList{T}" />.</param>
        /// <returns>
        ///     <code>true</code> if <code>item</code> is found in the <see cref="LinkedList{T}" />; otherwise,
        ///     <code>false</code>.
        /// </returns>
        public bool Contains(T item)
        {
            foreach (var element in this)
                if (element.Equals(item))
                    return true;

            return false;
        }

        /// <summary>
        ///     Copies the elements of the <see cref="LinkedList{T}" /> to an <see cref="Array" />, starting at a particular
        ///     <see cref="Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="Array" /> that is the destination of the elements copied from
        ///     <see cref="LinkedList{T}" />. The <see cref="Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <code>array</code> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex += 1;
            }
        }

        /// <summary>
        ///     Remove the first occurrence of the specific object from the <see cref="LinkedList{T}" />
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="LinkedList{T}" /></param>
        /// <exception cref="NotSupportedException">This <see cref="LinkedList{T}" /> is read-only.</exception>
        /// <returns>
        ///     <code>true</code> if <code>item</code> was successfully removed from <see cref="LinkedList{T}" />; otherwise
        ///     <code>false</code>. This method also returns <code>false</code> if <code>item</code> is not found in the original
        ///     <see cref="LinkedList{T}" />.
        /// </returns>
        /// <seealso cref="IsReadOnly" />
        public bool Remove(T item)
        {
            CheckReadOnly();

            if (_tail.Value.Equals(item))
            {
                _tail = _tail.Pointer;
                Count -= 1;
                return true;
            }

            var current = _tail;
            while (current.Pointer != null)
            {
                if (current.Pointer.Value.Equals(item))
                {
                    current.Pointer = current.Pointer.Pointer;
                    Count -= 1;
                    return true;
                }

                current = current.Pointer;
            }

            return false;
        }

        public bool IsReadOnly { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _tail;
            while (current != null)
            {
                yield return current.Value;
                current = current.Pointer;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Determines the index of a specific item in the <see cref="LinkedList{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="LinkedList{T}" />.</param>
        /// <returns>The index of <code>item</code> if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            var i = 0;
            var current = _tail;
            foreach (var element in this)
            {
                if (element.Equals(item))
                    return i;
                current = current.Pointer;
                i += 1;
            }

            return -1;
        }

        /// <summary>
        ///     Inserts an item to the <see cref="LinkedList{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <code>item</code> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="LinkedList{T}" />.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <code>index</code> is not valid in the <see cref="LinkedList{T}" />
        /// </exception>
        /// <exception cref="NotSupportedException">This <see cref="LinkedList{T}" /> is read-only.</exception>
        /// <remarks>
        ///     If <code>index</code> equals the number of items in the <see cref="LinkedList{T}" />, then <code>item</code>
        ///     is appended to the list.
        /// </remarks>
        public void Insert(int index, T item)
        {
            CheckReadOnly();
            ValidateIndex(index, true);
            if (index == Count)
            {
                Add(item);
            }
            else if (index == 0)
            {
                _tail = new Node(item, _tail);
            }
            else
            {
                var current = GetNodeAt(index - 1);
                var toInsert = new Node(item, current.Pointer);
                current.Pointer = toInsert;
            }


            Count += 1;
        }

        public void RemoveAt(int index)
        {
            CheckReadOnly();
            ValidateIndex(index);
            if (index == 0) _tail = _tail.Pointer;
            var previous = GetNodeAt(index - 1);
            previous.Pointer = previous.Pointer.Pointer;

            Count -= 1;
        }

        /// <summary>
        ///     Gets or sets the item at the specified index
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        public T this[int index]
        {
            get => GetNodeAt(index).Value;
            set => GetNodeAt(index).Value = value;
        }

        private void CheckReadOnly()
        {
            if (IsReadOnly) throw new NotSupportedException($"This LinkedList<{typeof(T)}> is read-only.");
        }

        private void ValidateIndex(int index, bool allowHead = false)
        {
            if (index < 0 || (allowHead ? index > Count : index >= Count))
                throw new ArgumentOutOfRangeException(
                    $"Provided index is outside the range of acceptable values in this LinkedList<{typeof(T)}>.");
        }

        /// <summary>
        ///     Get the <code>n-1</code>th node in the <see cref="LinkedList{T}" />
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns></returns>
        private Node GetNodeAt(int index)
        {
            ValidateIndex(index);
            if (index == Count - 1) return _head;
            if (index == 0) return _tail;

            var current = _tail;
            var i = 0;
            while (i < index)
            {
                i += 1;
                current = current.Pointer;
            }

            return current;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0}<{1}>: {{", nameof(LinkedList<T>), typeof(T));

            var current = _tail;
            while (current.Pointer != null)
            {
                stringBuilder.AppendFormat("{0} -> ", current.Value);
                current = current.Pointer;
            }

            stringBuilder.Append(current.Value);
            stringBuilder.Append('}');

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Represents an element of the list
        /// </summary>
        private class Node
        {
            public Node Pointer;
            public T Value;

            public Node(T value, Node pointer)
            {
                Value = value;
                Pointer = pointer;
            }
        }
    }
}