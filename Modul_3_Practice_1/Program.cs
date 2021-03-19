using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Modul_3_Practice_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ContactCollection<MyContact> myContacts = new ContactCollection<MyContact>(new MocConfigGenerator().GetConfig(), CultureInfo.CurrentCulture);
            myContacts.Add(new MyContact() { Lastname = " ", Name = "Rio", Phone = "0959896876", Surname = "Risotto" });
            myContacts.Add(new MyContact() { Lastname = null, Name = "Иван", Phone = "0959896876", Surname = "Додик" });
            myContacts.Add(new { Lastname = "213fadf", Name = "Ірина", Phone = "+095-98-96-876", Surname = "Ірис" });
            myContacts.Add(new { Lastname = "Володимирович", Name = "Сашко", Phone = "+095-98-96-876", Surname = "Курлик" });
            myContacts.Add(new { Lastname = "Володимирович", Name = "Генадий", Phone = "+095-98-96-876", Surname = "Алиев" });

            foreach (var i in myContacts)
            {
                Console.WriteLine($"{i.Surname} {i.Name} {i.Lastname} {i.Phone}");
            }

            myContacts.Sort();

            Console.WriteLine();

            foreach (var i in myContacts)
            {
                Console.WriteLine($"{i.Surname} {i.Name} {i.Lastname} {i.Phone}");
            }

            try
            {
                myContacts.Add(new { Lastname = "Володимирович", Name = "Генадий", Phone = "+095-98", Surname = "Алиев" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}
