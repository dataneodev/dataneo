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
                throw new ArgumentException("predictedCapacity < 0");

            var returnList = new List<T>(predictedCapacity);
            returnList.AddRange(enumerable);

            if (trimExcess && returnList.Count != returnList.Capacity)
            {
                returnList.TrimExcess();
            }

            return returnList;
        }

        public static T[] ToArray<T>(this IEnumerable<T> enumerable, int predictedCapacity)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (predictedCapacity < 0)
                throw new ArgumentException("predictedCapacity < 0");

            var returnArray = new T[Math.Max(predictedCapacity, 4)];

            int itemIndex = 0;
            foreach (var item in enumerable)
            {
                if (itemIndex + 1 > returnArray.Length)
                {
                    checked
                    {
                        var newReturnArray = new T[returnArray.Length * 2];
                        Array.Copy(returnArray, newReturnArray, returnArray.Length);
                        returnArray = newReturnArray;
                    }
                }
                returnArray[itemIndex] = item;
                itemIndex++;
            }

            if (returnArray.Length > 0 && returnArray.Length != itemIndex - 1)
            {



            }

            return returnArray;
        }
    }
}
