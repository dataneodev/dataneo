using dataneo.Extensions;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace dataneo.Extension
{
    public class IEnumerableExtensionToArrayTests
    {
        [Fact]
        public void NegativePredictedLength()
        {
            Action action = () => Enumerable.Empty<int>().ToArray(-1);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(10)]
        public void LengthIEnumerable(int inputCollectionLength)
        {
            var arr = Enumerable.Repeat<int>(0, inputCollectionLength)
                                .ToArray(10);
            arr.Should().HaveCount(inputCollectionLength);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(10)]
        public void StartEndContaininsIEnumerable(int inputCollectionLength)
        {
            var arr = Enumerable.Repeat<int>(0, inputCollectionLength)
                                .Select((s, index) => index + 1)
                                .ToArray(10);

            arr.Should().HaveCount(inputCollectionLength);
            arr.Should().StartWith(1);
            arr.Should().EndWith(inputCollectionLength);
        }
    }
}
