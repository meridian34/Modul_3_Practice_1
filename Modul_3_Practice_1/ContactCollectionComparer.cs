using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_3_Practice_1
{

    class ContactCollectionComparer : IComparer<KeyValuePair<string, List<MyContact>>>
    {
        public int Compare(KeyValuePair<string, List<MyContact>> x, KeyValuePair<string, List<MyContact>> y)
        {
            return x.Key.CompareTo(y.Key);
        }

    }
}
