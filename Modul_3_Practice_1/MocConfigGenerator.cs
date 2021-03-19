using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Modul_3_Practice_1
{
    public class MocConfigGenerator
    {
        public CultureConfig GetConfig()
        {
            var config = new CultureConfig();
            config.Alphabets.Add(
                new CultureInfo("en-US"),
                "abcdefghijklmnopqrstuvwxyz");
            config.Alphabets.Add(
               new CultureInfo("en-EN"),
               "abcdefghijklmnopqrstuvwxyz");
            config.Alphabets.Add(
                new CultureInfo("ru-RU"),
                "абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
            config.Alphabets.Add(
                new CultureInfo("ru-UA"),
                "абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
            config.Alphabets.Add(
                new CultureInfo("uk-UA"),
                "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя");
            return config;
        }
    }
}
