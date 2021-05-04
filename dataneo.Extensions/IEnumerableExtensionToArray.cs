using System;
using System.Collections.Generic;

namespace dataneo.Extensions
{
    public static class IEnumerableExtensionToArray
    {
        public static T[] ToArray<T>(this IEnumerable<T> enumerable, int predictedCapacity)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (predictedCapacity < 0)
                throw new ArgumentOutOfRangeException("predictedCapacity < 0");

            var returnArray = new T[Math.Max(predictedCapacity, 4)];

            int itemIndex = 0;
            foreach (var item in enumerable)
            {
                if (itemIndex + 1 > returnArray.Length)
                {
                    Array.Resize(ref returnArray, checked(returnArray.Length * 2));
                    // returnArray = CreateNewArray(returnArray, );
                }
                returnArray[itemIndex] = item;
                itemIndex++;
            }

            if (returnArray.Length > 0 && returnArray.Length != itemIndex)
            {
                Array.Resize(ref returnArray, itemIndex);
                // return CreateNewArray(returnArray, itemIndex);
            }

            return returnArray;
        }

        //private static T[] CreateNewArray<T>(T[] oldArray, int newLength)
        //{
        //    var newReturnArray = new T[newLength];
        //    Array.Copy(oldArray, newReturnArray, Math.Min(oldArray.Length, newReturnArray.Length));
        //    return newReturnArray;
        //}
    }
}
