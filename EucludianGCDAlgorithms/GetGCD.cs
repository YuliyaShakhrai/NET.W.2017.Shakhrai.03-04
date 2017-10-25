using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GCDAlgorithms
{
    /// <summary>
    ///Class Contains methods for getting the greatest common divisor of integer numbers and receiving execution time.
    /// </summary>
    public static class GetGCD
    {
        #region Public methods
        /// <summary>
        /// The method for getting the greatest common divisor of two integer numbers by Euclidean algorithm.
        /// </summary>
        /// <param name="first">First number</param>
        /// <param name="second">Second number</param>
        /// <returns>The greatest common divisor of two integer numbers</returns>
        public static int GetGCDEuclidean (int first, int second)
        {
            #region Exceptions
            if (first == 0 && second == 0) throw new ArgumentNullException("Two numbers are zero.");
            #endregion
            if (first == 0) return second;
            if (second == 0) return first;

            first = Math.Abs(first);
            second = Math.Abs(second);

            while (first != second)
            {
                if (first > second) first -= second;
                else second -= first;
            }
            return first;
        }

        /// <summary>
        /// The method for getting the greatest common divisor of two integer numbers by Euclidean algorithm and receiving execution time.
        /// </summary>
        /// <param name="first">First number</param>
        /// <param name="second">Second number</param>
        /// <returns>The greatest common divisor of two integer numbers</returns>
        public static int GetGCDEuclidean(out long milliseconds, int first, int second)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int gcd = GetGCDEuclidean(first, second);
            stopwatch.Stop();
            milliseconds = stopwatch.ElapsedMilliseconds;
            return gcd;
        }

        /// <summary>
        /// The method for getting the greatest common divisor of array of integers by Euclidean algorithm.
        /// </summary>
        /// <param name="intArray">Array of integer numbers</param>
        /// <returns>The greatest common divisor of two integer numbers</returns>
        public static int GetGCDEuclidean(params int[] intArray)
        {
            #region Exceptions
            if (intArray.Length < 2)
            {
                throw new ArgumentException("Array contains less than 2 numbers.");
            }
            if (IsArrayOfZeros(intArray))
            {
                throw new ArgumentException("Array contains only zeros.");
            }
            #endregion

            int[] tempArray = AdaptArray(intArray);
            int result = GetGCDEuclidean(tempArray[0], tempArray[1]);
            for (int i = 2; i < tempArray.Length; i++)
            {
                result = GetGCDEuclidean(result, tempArray[i]);
            }
            return result;
        }

        /// <summary>
        /// The method for getting the greatest common divisor of array of integers by Euclidean algorithm and receiving execution time.
        /// </summary>
        /// <param name="intArray">Array of integer numbers</param>
        /// <returns>The greatest common divisor of two integer numbers</returns>
        public static int GetGCDEuclidean(out long milliseconds, params int[] intArray)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int gcd = GetGCDEuclidean(intArray);
            stopwatch.Stop();
            milliseconds = stopwatch.ElapsedMilliseconds;
            return gcd;
        }

        /// The method for getting the greatest common divisor of two integer numbers by Stein algorithm.
        /// </summary>
        /// <param name="first">First number</param>
        /// <param name="second">Second number</param>
        /// <returns>The greatest common divisor of two integer numbers</returns>
        public static int GetGCDStein(int first, int second)
        {
            #region Exceptions
            if (first == 0 && second == 0) throw new ArgumentNullException("Two numbers are zero.");
            #endregion
            if (first == 0) return second;
            if (second == 0) return first;
            if (first == 1 || second == 1) return 1;

            first = Math.Abs(first);
            second = Math.Abs(second);

            if ((first % 2 == 0) && (second % 2 == 0))
            {
                return 2 * GetGCDStein(first / 2, second / 2);
            }
            if ((first % 2 == 0) && (second % 2 != 0))
            {
                return GetGCDStein(first / 2, second);
            }
            if ((first % 2 != 0) && (second % 2 == 0))
            {
                return GetGCDStein(first, second / 2);
            }
            return GetGCDStein(second, Math.Abs(first - second));
        }

        /// <summary>
        /// The method for getting the greatest common divisor of array of integers by Stein algorithm.
        /// </summary>
        /// <param name="intArray">Array of integer numbers</param>
        /// <returns>The greatest common divisor of two integer numbers</returns>
        public static int GetGCDStein(params int[] intArray)
        {
            #region Exceptions
            if (intArray.Length < 2)
            {
                throw new ArgumentException("Array contains less than 2 numbers.");
            }
            if (IsArrayOfZeros(intArray))
            {
                throw new ArgumentException("Array contains only zeros.");
            }
            #endregion

            int[] tempArray = AdaptArray(intArray);
            int result = GetGCDStein(tempArray[0], tempArray[1]);
            for (int i = 2; i < tempArray.Length; i++)
            {
                result = GetGCDStein(result, tempArray[i]);
            }
            return result;
        }

        #endregion

        #region Private methods
        ///<summary>
        /// The method for checking is array of zeros.
        /// </summary>
        /// <param name="intArray">Array of integer numbers</param>
        /// <returns>Returns flag of checking</returns>
        private static bool IsArrayOfZeros(int[] intArray)
        {
            bool flag = false;
            for (int i = 0; i < intArray.Length; i++)
            {
                if (intArray[i] != 0)
                {
                    flag = false;
                    break;
                }
                else
                    flag = true;
            }
            return flag;
        }

        /// <summary>
        /// The method for adapting array for gcd algorithms.
        /// </summary>
        /// <param name="intArray">Array of integer numbers</param>
        /// <returns>The adapted array of integers</returns>
        private static int[] AdaptArray(int[] intArray)
        {
            #region Counting size for new int[] without zeros
            int size = 0;
            int[] sortedArray = intArray.OrderBy(x => x == 0).ToArray();
            for (int i = 0; i < sortedArray.Length; i++)
            {
                if (sortedArray[i] != 0)
                    size++;
            }
            #endregion

            #region Adapting array depending on the scenario
            if (size == intArray.Length)
            {
                return intArray;
            }
            if (size == 1)
            {
                int[] adaptedArray = new int[size + 1];
                adaptedArray[0] = sortedArray[0];
                adaptedArray[1] = 0;
                return adaptedArray;
            }
            else
            {
                int[] adaptedArray = new int[size];
                for (int i = 0; i < size; i++)
                {
                    adaptedArray[i] = sortedArray[i];
                }
                return adaptedArray;
            }
            #endregion
        }
        #endregion
    }
}
