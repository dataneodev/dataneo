using System;
using System.Collections.Generic;

namespace dataneo.Extensions
{
    public static class IEnumerableToList
    {
        public static List<T> ToList<T>(this IEnumerable<T> enumerable, int predictedCapacity, bool trimExcess = false)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (predictedCapacity < 0)
                throw new ArgumentOutOfRangeException("predictedCapacity < 0");

            if (enumerable is ICollection<T> collection)
            {
                predictedCapacity = collection.Count;
            }

            var returnList = new List<T>(predictedCapacity);

            foreach (T item in enumerable)
            {
                returnList.Add(item);
            }

            if (trimExcess && returnList.Count != returnList.Capacity)
            {
                returnList.TrimExcess();
            }

            return returnList;
        }
    }
}
