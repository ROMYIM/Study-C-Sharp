using System;
using System.Collections;
using Xunit;

namespace SystemLibTest
{
    public class BitArrayTest
    {

        private const int Number = 374827343;

        [Fact]
        public void NumberToBitArrayTest()
        {
            var bitArray = new BitArray(Number);
        }
    }
}
