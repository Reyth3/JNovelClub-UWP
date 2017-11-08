using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCReaderUWP.Helpers
{
    public static class StringHelper
    {
        public static bool IsPronouncable(this string str)
        {
            for (int i = 0; i < str.Length; i++)
                if (char.IsLetterOrDigit(str[i]))
                    return true;
            return false;
        }
    }
}
