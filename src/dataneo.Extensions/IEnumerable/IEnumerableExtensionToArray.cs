using System;
using System.Collections.Generic;

namespace dataneo.Extensions
{
    public static class IEnumerableExtensionToArray
    {
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source, int predictedCapacity)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (predictedCapacity < 0)
                throw new ArgumentOutOfRangeException("predictedCapacity < 0");

            if (source is ICollection<TSource> collection)
            {
                int count = collection.Count;
                if (count == 0)
                {
                    return Array.Empty<TSource>();
                }

                var result = new TSource[count];
                collection.CopyTo(result, arrayIndex: 0);
                return result;
            }

            var returnArray = new TSource[Math.Max(predictedCapacity, 4)];

            int itemIndex = 0;
            foreach (var item in source)
            {
                if (itemIndex + 1 > returnArray.Length)
                {
                    Array.Resize(ref returnArray, checked(returnArray.Length * 2));
                }
                returnArray[itemIndex] = item;
                itemIndex++;
            }

            if (returnArray.Length > 0 && returnArray.Length != itemIndex)
            {
                Array.Resize(ref returnArray, itemIndex);
            }

            return returnArray;
        }
    }
}
