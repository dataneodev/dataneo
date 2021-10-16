using System.Collections.Generic;

namespace dataneo.Helpers
{
    public static class ArrayHelper
    {
        public static IReadOnlyList<T> SingleElementToIReadOnlyList<T>(T element)
            => new T[] { element };

        public static T[] SingleElementToArray<T>(T element)
            => new T[] { element };
    }
}
