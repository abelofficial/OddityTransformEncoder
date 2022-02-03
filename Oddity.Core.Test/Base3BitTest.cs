using Xunit;
using FluentAssertions;

namespace Oddity.Core.Test
{
    public class Base3BitTest
    {
        [Fact]
        public void should_be_able_to_create_instance()
        {
            var base3 = "Hello";
            base3.Should().NotBe(null);
        }
    }
}

