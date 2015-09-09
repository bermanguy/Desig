using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public static class ExtensionMethods
    {
        public const string k_StringExistanceSymbol = "v";
        
        public static string StringExistance(this string i_Url)
        {           
            return (i_Url == null) ? string.Empty : k_StringExistanceSymbol;
        }
    }
}
