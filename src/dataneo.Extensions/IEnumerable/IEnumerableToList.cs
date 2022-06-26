using System;
using System.Collections.Generic;

namespace dataneo.Extensions
{
    public static class IEnumerableToList
    {
        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> enumerable, int predictedCapacity, bool trimExcess = false)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (predictedCapacity < 0)
                throw new ArgumentOutOfRangeException("predictedCapacity < 0");

            if (enumerable is ICollection<TSource> collection)
            {
                predictedCapacity = collection.Count;
            }

            var returnList = new List<TSource>(predictedCapacity);

            foreach (TSource item in enumerable)
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
