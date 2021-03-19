using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Modul_3_Practice_1
{
    public class CultureConfig
    {
        public CultureConfig()
        {
            Alphabets = new Dictionary<CultureInfo, string>();
        }
                
        public Dictionary<CultureInfo,string> Alphabets { get; set; }
    }
}
