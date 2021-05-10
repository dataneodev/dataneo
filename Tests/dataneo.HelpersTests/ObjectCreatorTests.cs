using dataneo.Helpers;
using FluentAssertions;
using FluentAssertions.CSharpFunctionalExtensions;
using System.Text;
using Xunit;

namespace dataneo.HelpersTests
{
    public class ObjectCreatorTests
    {
        [Fact]
        public void CreateStringBuilder()
        {
            var sbName = "System.Text.StringBuilder";
            var sbResult = ObjectCreator.CreateInstance<StringBuilder>(sbName);
            sbResult.Should().BeSuccess();

            if (sbResult.IsSuccess)
            {
                sbResult.Value.Should().BeOfType(typeof(StringBuilder));
            }
        }
    }
}
