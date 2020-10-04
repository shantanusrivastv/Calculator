using System;

namespace UL.Calculator.Common
{
    public static class Constants
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