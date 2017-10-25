using System;
using NUnit.Framework;
using GCDAlgorithms;

namespace GCDAlgorithms.Tests
{
    [TestFixture]
    public class GetGCDTests
    {
        [TestCase(24, 27, 3)]
        [TestCase(-24, -27, 3)]
        [TestCase(99, 0, 99)]
        [TestCase(0, 11, 11)]
        [TestCase(-12, 18, 6)]
        [TestCase(33, 33, 33)]
        public void GetGCDEuclidean_PositiveResults(int first, int second, int expected)
        {
            Assert.AreEqual(expected, GetGCD.GetGCDEuclidean(first, second));
        }

        [TestCase(1, 1, 2, 3)]
        [TestCase(33, 33, 66, 99)]
        [TestCase(1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0)]
        [TestCase(2, 0, 0, 0, 2, 12, 0, 0, 6, 0, 0)]
        [TestCase(2, 0, 0, 0, -2, 12, 0, 0, 6, 0, 0)]
        [TestCase(1, 0, 1)]
        public void GetGCDEuclideanArray_PositiveResults(int expected, params  int[] intArray)
        {
            Assert.AreEqual(expected, GetGCD.GetGCDEuclidean(intArray));
        }

        [TestCase(24, 27, 3)]
        [TestCase(-24, -27, 3)]
        [TestCase(99, 0, 99)]
        [TestCase(0, 11, 11)]
        [TestCase(-12, 18, 6)]
        [TestCase(33, 33, 33)]
        public void GetGCDStein_PositiveResults(int first, int second, int expected)
        {
            Assert.AreEqual(expected, GetGCD.GetGCDStein(first, second));
        }

        [TestCase(1, 1, 2, 3)]
        [TestCase(33, 33, 66, 99)]
        [TestCase(1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0)]
        [TestCase(2, 0, 0, 0, 2, 12, 0, 0, 6, 0, 0)]
        [TestCase(2, 0, 0, 0, -2, 12, 0, 0, 6, 0, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 1)]
        public void GetGCDSteinArray_PositiveResults(int expected, params int[] intArray)
        {
            Assert.AreEqual(expected, GetGCD.GetGCDStein(intArray));
        }

        [TestCase(0, 0)]
        public void EuclideanGCD_ThrowArgumentNullException(int first, int second)
        {
            Assert.Throws<ArgumentNullException>(() => GetGCD.GetGCDEuclidean(first, second));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(0,0,0,0,0)]
        public void EuclideanGCDArray_ThrowArgumentException(params int[] intArray)
        {
            Assert.Throws<ArgumentException>(() => GetGCD.GetGCDEuclidean(intArray));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(0, 0, 0, 0, 0)]
        public void GetGCDSteinArray_ThrowArgumentException(params int[] intArray)
        {
            Assert.Throws<ArgumentException>(() => GetGCD.GetGCDStein(intArray));
        }

        //GetGCDStein





    }



}
