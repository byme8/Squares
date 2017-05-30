using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class RandomExtention
    {
        static Random random = new Random();

        public static TValue Random<TValue>(this IEnumerable<TValue> list)
        {
            var count = list.Count();
            return list.Skip(random.Next(0, count)).First();
        }
    }
}
