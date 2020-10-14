using Anagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anagrams
{


    public class WordLists
    {

        // copy old methods




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
