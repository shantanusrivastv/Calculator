using System;

namespace UL.Calculator.Common
{
    public static class Constant
    {
        public static char[] GetAllowedOperators()
        {
            return new char[]
            {
                '+',
                '-',
                '/',
                '*'
            };
        }
    }
}