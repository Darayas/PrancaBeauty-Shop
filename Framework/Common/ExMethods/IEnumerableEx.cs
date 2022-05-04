using System;
using System.Collections.Generic;

namespace Framework.Common.ExMethods
{
    public static class IEnumerableEx
    {
        public static IEnumerable<T> OrderByCondition<T>(
            this IEnumerable<T> elements,
            Func<bool> Condition,
            Func<IEnumerable<T>, IEnumerable<T>> ThenPath,
            Func<IEnumerable<T>, IEnumerable<T>> ElsePath)
        {
            return Condition()
                        ? ThenPath(elements)
                        : ElsePath(elements);
        }
    }
}
