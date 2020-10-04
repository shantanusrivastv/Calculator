using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL.Calculator.Common.Extensions
{
    public static class CharExtensions
    {
        public static int ToInt(this char c)
        {
            return c - '0';
        }
        
        public static double ToDouble(this char c)
        {
            return char.GetNumericValue(c);
        }
    }
}