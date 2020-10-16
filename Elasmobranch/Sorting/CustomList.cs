using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasmobranch.Sorting
{
    public class CustomList<T> : List<T>
    {
        private Type _typeParameterType = typeof(T);

        /// <summary>
        ///     Get all the duplicated elements in this list
        /// </summary>
        /// <returns>A list of the duplicated elements in this list</returns>
        public List<T> GetDuplicates()
        {
            return this.GroupBy(t => t).SelectMany(group => group.Skip(1)).ToList();
        }

        /// <summary>
        ///     Return whether the provided list is a subset of this one
        /// </summary>
        /// <param name="subset">A second list which is potentially a subset of this one</param>
        /// <returns>Whether the provided list is a subset of this one</returns>
        public bool IsSubset(IEnumerable<T> subset)
        {
            // Except removes all elements from the provided "subset" list that are present in this list
            // Any then returns true if there are any elements remaining
            // If there are elements remaining then they must be elements that are inside the "subset" but not in list
            // Then negate the boolean for the purposes of this function
            return !subset.Except(this).Any();
        }
    }
}