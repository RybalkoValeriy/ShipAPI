using Domain.Entities.Ships;
using FluentAssertions;

namespace Domain.UnitTests
{
    public class ShipCodeTests
    {
        [TestCase("AAAA-1234-A1")]
        [TestCase("ZZZZ-9999-Z9")]
        [TestCase("aaaa-1234-a1")]
        [TestCase("zzzz-9999-z9")]
        [TestCase("AkfS-1847-d9")]
        [Parallelizable(ParallelScope.All)]
        public void CreateCode_ShouldCorrectly(string correctCode)
        {
            // Arrange + Act
            var code = Code.Create(correctCode);

            // Assert
            code.Should().NotBeNull();
            code.Value.Should().Be(correctCode);
        }

        [TestCase("AAA-1234-A1")]
        [TestCase("dfjk-999-Z9")]
        [TestCase("Üfdf-1234-a1")]
        [TestCase("@fsf-1234-z9")]
        [TestCase("aaaa-1847-#9")]
        [TestCase("sdlkfjsdljÜ")]
        [TestCase("")]
        [Parallelizable(ParallelScope.All)]
        public void CreateCode_ShouldThrowArgumentException_WhenIncorrectCodeFormat(string incorrectCode)
        {
            // Arrange + Act + Assert
            FluentActions
                .Invoking(() => Code.Create(incorrectCode))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Incorrect Code");
        }
    }
}