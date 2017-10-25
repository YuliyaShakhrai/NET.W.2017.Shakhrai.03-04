using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    /// <summary>
    ///Class Contains method for converting double-precision floating point value to string representation by binary format.
    /// </summary>
    public static class ExtensionMethod
    {
        #region Constants
        private const int ExponentBits = 11;
        private const int MantissaBits = 52;
        #endregion

        #region Public method
        public static string DoubleToBinaryString(this double doubleNumber)
        {
            string result = string.Empty;
            string fractionPart = string.Empty;

            if (doubleNumber >= 0) result += "0";
            else result += "1";

            string[] splitted = doubleNumber.ToString().Split('.');
            string integerPart = IntegerPartToBinary(Math.Abs(long.Parse(splitted[0])));
            if (splitted.Length == 1)
            {
                fractionPart = FractionPartToBinary(0, MantissaBits - integerPart.Length + 1);
            }
            else
            {
                fractionPart = FractionPartToBinary(double.Parse("0." + splitted[1]), MantissaBits - integerPart.Length + 1);
            }
            string exponentPart = IntegerPartToBinary(integerPart.Length - 1 + 1023);

            for (int i = 0; i < exponentPart.Length - ExponentBits; i++)
            {
                exponentPart += "0";
            }

            result += exponentPart + GetMantissa(integerPart, fractionPart);
            return result;

        }
        #endregion

        #region Private methods
        private static string IntegerPartToBinary(long integerPart)
        {
            var integerPartString = new StringBuilder();

            for (var i = sizeof(long) * 8 - 1; i >= 0; i--)
            {
                integerPartString.Append((integerPart & ((long)1 << i)) != 0 ? '1' : '0');
            }

            return integerPartString.ToString().TrimStart('0');
        }

        public static string FractionPartToBinary(double fraction, int maxLength)
        {

            var fractionPartString = new StringBuilder();

            if (fraction == 0)
            {
                for (int i = 0; i < maxLength - 1; i++)
                {
                    fractionPartString.Append("0");
                }
                return fractionPartString.ToString();
            }
            for (int i = 0; i < maxLength; i++)
            {
                fraction *= 2;
                if (fraction == 1)
                {
                    fractionPartString.Append("1");
                    return fractionPartString.ToString();
                }
                if (fraction > 1)
                {
                    fractionPartString.Append("1");
                    fraction--;
                }
                else
                {
                    fractionPartString.Append("0");
                }
                fraction = Math.Round(fraction, 10);
            }

            return fractionPartString.ToString();
        }

        private static string GetMantissa(string integerPart, string fractionPart)
        {
            var strBuild = new StringBuilder();
            char[] ch = integerPart.ToCharArray();
            for (int i = 1; i < ch.Length; i++)
            {
                strBuild.Append(ch[i]);
            }
            strBuild.Append(fractionPart);

            for (var i = 0; i <= MantissaBits - integerPart.Length - fractionPart.Length; i++)
            {
                strBuild.Append("0");
            }
            strBuild.ToString();
            int d = strBuild.Length;
            return strBuild.ToString();
        }
        #endregion



    }
}
