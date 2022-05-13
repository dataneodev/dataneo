using dataneo.Helpers;
using FluentAssertions;
using FluentAssertions.CSharpFunctionalExtensions;
using System.Text;
using Xunit;

namespace dataneo.HelpersTests
{
    public class TestClassForObjectCreator { }

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

        [Fact]
        public void CreateTestClassForObjectCreator()
        {
            var testClass = "dataneo.HelpersTests.TestClassForObjectCreator";
            var sbResult = ObjectCreator.CreateInstance<TestClassForObjectCreator>(testClass);
            sbResult.Should().BeSuccess();

            if (sbResult.IsSuccess)
            {
                sbResult.Value.Should().BeOfType(typeof(TestClassForObjectCreator));
            }
        }

        [Fact]
        public void CreateNonExistingTypeName()
        {
            var testClass = "dataneo.HelpersTests.None";
            var sbResult = ObjectCreator.CreateInstance<TestClassForObjectCreator>(testClass);
            sbResult.Should().BeFailure();
            if (sbResult.IsFailure)
            {
                sbResult.Error.Should().Be("Class not found");
            }
        }

        [Fact]
        public void CreateInvalidType()
        {
            var testClass = "dataneo.HelpersTests.TestClassForObjectCreator";
            var sbResult = ObjectCreator.CreateInstance<string>(testClass);
            sbResult.Should().BeFailure();
            if (sbResult.IsFailure)
            {
                sbResult.Error.Should().Be("Unable to create instance of an object");
            }
        }
    }
}
