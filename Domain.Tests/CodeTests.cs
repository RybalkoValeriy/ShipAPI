using Domain.Entities.Ships;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.Tests;

public class CodeTests
{
    [Test]
    public void ShouldCreateCorrectCode()
    {
        // Arrange
        var correctCode = "AAAA-1234-A1";

        // Act
        var code = Code.Create(correctCode);

        // Assert
        code.Should().Be(correctCode);
    }

    [Test]
    public void ShouldThrowArgumentExceptionWhenIncorrectCode()
    {
        // Arrange
        var incorrectCode = "someIncorrectFormatCode";

        // Act + Assert
        FluentActions
            .Invoking(() => Code.Create(incorrectCode))
            .Should()
            .Throw<ArgumentException>();
    }
}
