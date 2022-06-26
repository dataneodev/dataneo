using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataneo.Extensions
{
    public static class IEnumerableToDistinctList
    {
        public static List<TSource> ToDistinctList<TSource>(this IEnumerable<TSource> input)
        {
            var collectionCount = GetCollectionCount(input);
            var collectionWithoutDuplicates = collectionCount.HasValue ? new List<TSource>(collectionCount.Value) : new List<TSource>();

            foreach (var item in input)
            {
                if (collectionWithoutDuplicates.Contains(item))
                    continue;
                collectionWithoutDuplicates.Add(item);
            }

            if (collectionCount.HasValue)
                collectionWithoutDuplicates.TrimExcess();

            return collectionWithoutDuplicates;
        }

        public static List<TSource> ToDistinctList<TSource, TEquals>(this IEnumerable<TSource> input, Func<TSource, TEquals> distincBy)
        {
            if (distincBy is null)
                return ToDistinctList(input);

            var collectionCount = GetCollectionCount(input);
            var collectionWithoutDuplicates = collectionCount.HasValue ? new List<TSource>(collectionCount.Value) : new List<TSource>();

            foreach (var item in input)
            {
                var itemDistinc = distincBy.Invoke(item);

                if (collectionWithoutDuplicates.Any(itemDup => distincBy.Invoke(itemDup)?.Equals(itemDistinc) ?? false))
                    continue;
                collectionWithoutDuplicates.Add(item);
            }

            if (collectionCount.HasValue)
                collectionWithoutDuplicates.TrimExcess();

            return collectionWithoutDuplicates;
        }

        private static Maybe<int> GetCollectionCount<TSource>(IEnumerable<TSource> input)
        {
            if (input is ICollection<TSource> collection)
                return collection.Count;

            return Maybe.None;
        }
    }
}
