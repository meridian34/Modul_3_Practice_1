using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_3_Practice_1
{
    public class CharacterFilter
    {
        private readonly string _alphabet;
        private readonly char _blockingCharacter;

        public CharacterFilter(CultureConfig config, CultureInfo cultureInfo)
        {
            _blockingCharacter = '#';
            if (!config.Alphabets.ContainsKey(cultureInfo))
            {
                _alphabet = config.Alphabets[new CultureInfo("en-EN")];
            }

            _alphabet = config.Alphabets[cultureInfo];
        }

        public string Filter(string input)
        {
            if (input != null)
            {
                StringBuilder builder = new StringBuilder(input);
                for (var i = 0; i < builder.Length; i++)
                {
                    if (!_alphabet.Contains(builder[i], StringComparison.OrdinalIgnoreCase) && builder[i] != _blockingCharacter)
                    {
                        builder.Replace(builder[i], _blockingCharacter);
                    }
                }

                return builder.ToString();
            }
            else
            {
                return null;
            }
        }

        public string FilterNumbersPhone(string input)
        {
            if (input != null)
            {
                StringBuilder builder = new StringBuilder(input);
                for (var i = 0; i < builder.Length; i++)
                {
                    if (!char.IsNumber(builder[i]) && builder[i] != _blockingCharacter)
                    {
                        builder.Replace(builder[i], _blockingCharacter);
                    }
                }

                return builder.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
