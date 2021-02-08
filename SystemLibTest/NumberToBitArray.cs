using System;
using System.Collections;
using Xunit;

namespace SystemLibTest
{
    public class BitArrayTest
    {

        private const int Number = 3748273482738429;

        [Fact]
        public void NumberToBitArrayTest()
        {
            var bitArray = new BitArray(Number);
        }
    }
}
