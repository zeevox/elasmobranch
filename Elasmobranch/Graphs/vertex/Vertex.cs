using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Elasmobranch.Graphs.vertex
{
    /// <summary>
    ///     Vertex – the fundamental unit from which graphs and trees are formed
    /// </summary>
    public class Vertex<T> : IEquatable<Vertex<T>>
    {
        private readonly Dictionary<Vertex<T>, int> _neighbours;

        /// <summary>
        ///     Initialise a new vertex
        /// </summary>
        /// <param name="value">The value stored at this vertex</param>
        public Vertex([NotNull] T value)
        {
            Value = value;
            _neighbours = new Dictionary<Vertex<T>, int>();
        }

        /// <summary>
        ///     An object that uniquely identifies this vertex
        /// </summary>
        public T Value { get; }

        /// <summary>
        ///     Get the number of neighbours of this vertex
        /// </summary>
        public int Degree => _neighbours.Count;

        public bool Equals(Vertex<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Vertex<T>) obj);
        }

        /// <summary>
        ///     Add a vertex that is directly accessible from this one
        /// </summary>
        /// <param name="neighbour">A reference to the adjacent vertex</param>
        /// <param name="weight">The length of the edge between this vertex and the adjacent one</param>
        public void AddNeighbour(Vertex<T> neighbour, int weight = 1)
        {
            _neighbours.Add(neighbour, weight);
        }

        /// <summary>
        ///     Remove a neighbouring vertex from this one
        /// </summary>
        /// <param name="neighbour">The adjacent vertex to remove</param>
        /// <returns>Returns false if the provided vertex is not a neighbour</returns>
        public bool RemoveNeighbour(Vertex<T> neighbour)
        {
            return _neighbours.Remove(neighbour);
        }

        /// <summary>
        ///     Get an iterable collection of vertices that are adjacent to this one
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vertex<T>> GetNeighbours()
        {
            return _neighbours.Keys;
        }

        /// <summary>
        ///     Get the weight of an edge that directly connects this vertex to the given one
        /// </summary>
        /// <param name="vertex">The adjacent vertex for which to get the edge length</param>
        /// <returns>
        ///     The distance to the vertex, or <code>int.MaxValue</code> if the provided vertex is not directly accessible
        ///     from this one
        /// </returns>
        public int DistanceTo(Vertex<T> vertex)
        {
            return vertex == null ? int.MaxValue : _neighbours.GetValueOrDefault(vertex, int.MaxValue);
        }

        public override string ToString()
        {
            return string.Format(
                $"{Value} neighbours: {string.Join(',', _neighbours.Select(kv => kv.Key.Value.ToString() + ':' + kv.Value).ToArray())}");
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(Vertex<T> left, Vertex<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vertex<T> left, Vertex<T> right)
        {
            return !Equals(left, right);
        }
    }
}