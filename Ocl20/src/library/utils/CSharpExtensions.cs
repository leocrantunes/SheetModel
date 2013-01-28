using System;
using System.Collections.Generic;
using System.Linq;

namespace Ocl20.library.utils
{
    public class CSharpExtensions
    {}

    static class Extensions
    {
        public static void AddRange<T>(this ICollection<T> collection, List<T> list)
        {
            foreach (var item in list)
            {
                collection.Add(item);
            }
        }

        public static ICollection<T> Clone<T>(this ICollection<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static string MySubstring(this string s, int start, int end)
        {
            return s.Substring(start, end - start);
        }
    }
}
