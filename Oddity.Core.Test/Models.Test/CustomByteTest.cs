using System;
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
            var base3 = new CustomLengthByte(4, 8);
            // Assert
            base3.Should().NotBe(null);
        }

        [Theory]
        [InlineData(13, new bool[] { true, false, true, true })]
        [InlineData(4, new bool[] { false, false, true, false })]
        [InlineData(15, new bool[] { true, true, true, true })]
        public void should_be_able_to_create_a_correct_bit_array(int value, bool[] bitArray)
        {
            // Arrange
            var base4 = new CustomLengthByte(4, value);
            // Assert
            base4.BitArray.Should().BeEquivalentTo(bitArray);
        }

        [Fact]
        public void should_throw_ArgumentException_with_if_value_is_to_high_for_base()
        {
            // Arrange
            Action expected = () => new CustomLengthByte(3, 8);
            // Assert
            expected.Should().Throw<ArgumentException>().WithMessage("Byte base 3 can only present numbers from 0 - 8");
        }
    }
}

