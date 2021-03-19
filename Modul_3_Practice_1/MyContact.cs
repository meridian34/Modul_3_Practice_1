using System;
using System.Collections.Generic;
using System.Text;

namespace Modul_3_Practice_1
{
    public class MyContact : IComparable, IEquatable<MyContact>
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Phone { get; set; }

        public int CompareTo(object obj)
        {
            var contact = obj as MyContact;
            if (contact != null && contact.Surname != null)
            {
                var s = Name.CompareTo(contact.Surname);
                if (s == 0)
                {
                    var n = Name.CompareTo(contact.Name);
                    if (n == 0)
                    {
                        var l = Lastname.CompareTo(contact.Lastname);
                        if (l == 0)
                        {
                            return Phone.CompareTo(contact.Phone);
                        }
                        else
                        {
                            return l;
                        }
                    }
                    else
                    {
                        return n;
                    }
                }
                else
                {
                    return s;
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is MyContact contact &&
                   Surname == contact.Surname &&
                   Name == contact.Name &&
                   Lastname == contact.Lastname &&
                   Phone == contact.Phone;
        }

        public bool Equals(MyContact other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Surname, Name, Lastname, Phone);
        }
    }
}
