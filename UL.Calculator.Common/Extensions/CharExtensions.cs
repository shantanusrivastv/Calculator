using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL.Calculator.Common.Extensions
{
    public static class CharExtensions
    {
        private static int ToInt(this char c)
        {
            return c - '0';
        }
    }
}