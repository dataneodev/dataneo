using System;
using System.Collections.Generic;

namespace dataneo.Extensions
{
    public static class IEnumerableExtension
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
            returnList.AddRange(enumerable);

            if (trimExcess && returnList.Count != returnList.Capacity)
            {
                returnList.TrimExcess();
            }

            return returnList;
        }
    }
}
