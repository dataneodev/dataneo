using System;

namespace dataneo.Extensions
{
    public static class NumericExtension
    {
        public static bool EqualsPrecision(this double thisValue, double value, double precision = 0.001)
            => Math.Abs(thisValue - value) <= precision;

        public static bool EqualsPrecision(this float thisValue, double value, double precision = 0.001)
            => Math.Abs(thisValue - value) <= precision;
    }
}
