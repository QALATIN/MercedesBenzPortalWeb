using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class PasswordOptions
    {
        public int RequiredLength { get; set; } = 6;
        public int RequiredUniqueChars { get; set; } = 1;
        public bool RequiredNonAlphaNumeric { get; set; } = true;
        public bool RequiredLowerCase { get; set; } = true;
        public bool RequiredUpperCase { get; set; } = true;
        public bool RequiredDigit { get; set; } = true;
    }
}
