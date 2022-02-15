using FluentAssertions;
using Xunit;

namespace Oddity.Core.Test
{
    public class Base3BitTest
    {
        [Fact]
        public void should_be_able_to_create_instance_by_providing_base_and_value()
        {
            // Arrange
            var base3 = new CustomBitGenerator(3, 8);

            base3.Should().NotBe(null);
        }

        [Theory]
        [InlineData(13, new bool[] { true, false, true, true })]
        [InlineData(4, new bool[] { false, false, true, false })]
        [InlineData(15, new bool[] { true, true, true, true })]
        public void should_be_able_to_create_a_correct_bit_array(int value, bool[] bitArray)
        {
            // Arrange
            var base4 = new CustomBitGenerator(4, value);


            base4.BitArray.Should().BeEquivalentTo(bitArray);
        }
    }
}

