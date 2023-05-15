using System;

namespace Envisia.Library.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ApplyDiscount(this decimal price, decimal discount)
        {
            var result = (price * (1 - discount)).RoundToCents(0.95M);

            return result;
        }

        public static decimal RoundToCents(this decimal value, decimal cents)
        {
            var result = Math.Truncate(value) + cents;

            return result;
        }

        public static decimal RoundTo(this decimal value, int places)
        {
            var result = Math.Round(value, places);

            return result;
        }
    }
}
