using System;
using System.Collections.Generic;

namespace Framework.Common.ExMethods
{
    public static class IEnumerableEx
    {
        public static IEnumerable<T> IfThenElse<T>(
            this IEnumerable<T> elements,
            bool Condition,
            Func<IEnumerable<T>, IEnumerable<T>> ThenPath,
            Func<IEnumerable<T>, IEnumerable<T>> ElsePath = null)
        {
            return Condition
                        ? ThenPath(elements)
                        : (ElsePath!=null ? ElsePath(elements) : elements);
        }
    }
}
