using System.Collections.Generic;

namespace dataneo.Extensions
{
    public static class ArrayHelper
    {
        public static IReadOnlyList<TSource> SingleElementToIReadOnlyList<TSource>(TSource element)
            => new TSource[] { element };

        public static TSource[] SingleElementToArray<TSource>(TSource element)
            => new TSource[] { element };
    }
}
