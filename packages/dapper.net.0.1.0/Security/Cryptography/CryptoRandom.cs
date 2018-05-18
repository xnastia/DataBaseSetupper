using System;
using System.Security.Cryptography;

namespace Dapper.Net.Security.Cryptography {

    ///<summary>
    /// Represents a pseudo-random number generator, a device that produces random data.
    ///</summary>
    public class CryptoRandom : RandomNumberGenerator {
        private static RandomNumberGenerator _r;

        ///<summary>
        /// Creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.
        ///</summary>
        public CryptoRandom() {
            _r = Create();
        }

        ///<summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        ///</summary>
        ///<param name="buffer">An array of bytes to contain random numbers.</param>
        public override void GetBytes(byte[] buffer) => _r.GetBytes(buffer);

        ///<summary>
        /// Returns a random number between 0.0 and 1.0.
        ///</summary>
        public double NextDouble() {
            var b = new byte[4];
            _r.GetBytes(b);
            return (double) BitConverter.ToUInt32(b, 0)/uint.MaxValue;
        }

        /// <summary>
        ///  Returns a random number within the specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than or equal to minValue.</param>
        public int Next(int minValue, int maxValue) => (int) Math.Round(NextDouble()*(maxValue - minValue - 1)) + minValue;

        ///<summary>
        /// Returns a nonnegative random number.
        ///</summary>
        public int Next() => Next(0, int.MaxValue);

        /// <summary>
        ///  Returns a nonnegative random number less than the specified maximum
        /// </summary>
        /// <param name="maxValue">The inclusive upper bound of the random number returned. Must be greater than or equal to 0.</param>
        public int Next(int maxValue) => Next(0, maxValue);
    }

}