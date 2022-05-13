using dataneo.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace dataneo.ExtensionsTest
{
    public class FromSingleIEnumerableTests
    {
        [Fact]
        public void Should_return_single_value()
        {
            var enumerator = FromSingleIEnumerable.Get(6);
            enumerator.Should().HaveCount(1);
            enumerator.Should().Contain(6);
        }

        [Fact]
        public void Default_FromSingleIEnumerable_should_have_0_count()
        {
            var enumerator = default(FromSingleIEnumerable<int>);
            enumerator.Should().HaveCount(0);
        }
    }
}
