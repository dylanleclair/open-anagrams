using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anagrams
{
    /// <summary>
    /// A class of helper functions to use when implementing the library.
    /// </summary>
    public class Helpers
    {
        public static string Shuffle(String str)
        {
            Random r = new Random();

            var list = new SortedList<int, char>();
            foreach (var c in str)
                list.Add(r.Next(), c);
            return new string(list.Values.ToArray());
        }
    }
}
