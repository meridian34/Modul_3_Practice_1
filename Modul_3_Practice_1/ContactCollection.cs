using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Modul_3_Practice_1
{
    public class ContactCollection<T>
        : ICollection<T>, IReadOnlyCollection<T>
        where T : class
    {
        private readonly CharacterFilter _characterFilter;
        private readonly List<KeyValuePair<string, List<MyContact>>> _container5;

        public ContactCollection(CultureConfig config)
            : this(config, CultureInfo.CurrentCulture)
        {
        }

        public ContactCollection(CultureConfig config, CultureInfo cultureInfo)
        {
            _characterFilter = new CharacterFilter(config, cultureInfo);
            _container5 = new List<KeyValuePair<string, List<MyContact>>>();
        }

        public int Count
        {
            get
            {
                var count = 0;
                for (var i = 0; i < _container5.Count; i++)
                {
                    for (var j = 0; j < _container5[i].Value.Count; j++)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var contact = ScanObject(item);
            Add(contact);
        }

        public void Add(object item)
        {
            var contact = ScanObject(item);
            Add(contact);
        }

        public void Add(MyContact contact)
        {
            ApplyCharacterFilter(contact);

            if (IsValid(contact))
            {
                var letter = GetFirstLeter(contact);
                var index = -1;

                for (var i = 0; i < _container5.Count; i++)
                {
                    if (_container5[i].Key == letter)
                    {
                        if (_container5[i].Value != null)
                        {
                            if (!Contains(contact))
                            {
                                _container5[i].Value.Add(contact);
                                index = i;
                            }
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                }

                if (index == -1)
                {
                    _container5.Add(new KeyValuePair<string, List<MyContact>>(letter, new List<MyContact>() { contact }));
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public void Clear()
        {
            _container5.Clear();
        }

        public bool Contains(T item)
        {
            var contact = ScanObject(item);
            return Contains(contact);
        }

        public bool Contains(MyContact item)
        {
            if (IsValid(item))
            {
                var letter = GetFirstLeter(item);
                foreach (var i in _container5)
                {
                    if (i.Key == letter)
                    {
                        foreach (var j in i.Value)
                        {
                            if (j.Equals(item))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void Sort()
        {
            foreach (var item in _container5)
            {
                item.Value.Sort();
            }

            _container5.Sort(new ContactCollectionComparer());
        }

        public void CopyTo(T[] array, int startIndex)
        {
            var countArr = array.Length - startIndex;
            if (countArr >= Count)
            {
                var index = startIndex;
                foreach (var item in this)
                {
                    array[index] = item;
                    index++;
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Type t = typeof(T);
            var item = Activator.CreateInstance(t);
            PropertyInfo[] properties = item.GetType().GetProperties();
            foreach (var block in _container5)
            {
                foreach (var contact in block.Value)
                {
                    foreach (var p in properties)
                    {
                        if (p.PropertyType == typeof(string))
                        {
                            if (string.Equals(p.Name, nameof(contact.Name), StringComparison.OrdinalIgnoreCase))
                            {
                                p.SetValue(item, contact.Name);
                            }
                            else if (string.Equals(p.Name, nameof(contact.Surname), StringComparison.OrdinalIgnoreCase))
                            {
                                p.SetValue(item, contact.Surname);
                            }
                            else if (string.Equals(p.Name, nameof(contact.Lastname), StringComparison.OrdinalIgnoreCase))
                            {
                                p.SetValue(item, contact.Lastname);
                            }
                            else if (string.Equals(p.Name, nameof(contact.Phone), StringComparison.OrdinalIgnoreCase))
                            {
                                p.SetValue(item, contact.Phone);
                            }
                        }
                        else if (p.PropertyType == typeof(int))
                        {
                            if (string.Equals(p.Name, nameof(contact.Phone), StringComparison.OrdinalIgnoreCase))
                            {
                                p.SetValue(item, contact.Phone);
                            }
                        }
                    }

                    yield return (T)item;
                }
            }
        }

        public bool Remove(T item)
        {
            var contact = ScanObject(item);
            ApplyCharacterFilter(contact);
            var letter = GetFirstLeter(contact);
            foreach (var block in _container5)
            {
                if (block.Key == letter)
                {
                    return block.Value.Remove(contact);
                }
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool IsValid(MyContact contact)
        {
            if (!string.IsNullOrEmpty(contact.Surname) &&
                !string.IsNullOrEmpty(contact.Phone) &&
                contact.Phone.Length >= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private MyContact ScanObject(object item)
        {
            var contact = new MyContact();

            PropertyInfo[] properties = item.GetType().GetProperties();
            foreach (var p in properties)
            {
                if (p.PropertyType == typeof(string))
                {
                    if (contact.Surname == null && string.Equals(p.Name, nameof(contact.Surname), StringComparison.OrdinalIgnoreCase))
                    {
                        contact.Surname = (string)p.GetValue(item);
                    }
                    else if (contact.Name == null && string.Equals(p.Name, nameof(contact.Name), StringComparison.OrdinalIgnoreCase))
                    {
                        contact.Name = (string)p.GetValue(item);
                    }
                    else if (contact.Lastname == null && string.Equals(p.Name, nameof(contact.Lastname), StringComparison.OrdinalIgnoreCase))
                    {
                        contact.Lastname = (string)p.GetValue(item);
                    }
                    else if (contact.Phone == null && string.Equals(p.Name, nameof(contact.Phone), StringComparison.OrdinalIgnoreCase))
                    {
                        contact.Phone = (string)p.GetValue(item);
                    }
                }
                else if (p.PropertyType == typeof(int))
                {
                    if (contact.Phone == null && string.Equals(p.Name, nameof(contact.Phone), StringComparison.OrdinalIgnoreCase))
                    {
                        contact.Phone = ((int)p.GetValue(item)).ToString();
                    }
                }
            }

            return contact;
        }

        private string GetFirstLeter(MyContact contact)
        {
            return contact.Surname.Substring(0, 1).ToUpper();
        }

        private void ApplyCharacterFilter(MyContact contact)
        {
            contact.Surname = contact.Surname == null ? null : _characterFilter.Filter(contact.Surname);
            contact.Name = contact.Name == null ? null : _characterFilter.Filter(contact.Name);
            contact.Lastname = contact.Lastname == null ? null : _characterFilter.Filter(contact.Lastname);
            contact.Phone = contact.Phone == null ? null : _characterFilter.FilterNumbersPhone(contact.Phone);
        }
    }
}
