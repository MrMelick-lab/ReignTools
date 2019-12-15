using Xunit;
using FluentAssertions;
using ReignTools.Entities.Options;

namespace ReignToolsUnitTest.Entitites.Options
{
    public class UnworthyRollOptionsTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(16)]
        [InlineData(short.MaxValue)]
        [InlineData(short.MinValue)]
        public void When_number_of_dice_is_not_valid_then_return_error(short numberOfDice)
        {
            var sut = new UnworthyRollOptions
            {
                NumberOfDice = numberOfDice
            };

            var result = sut.IsValid();

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(15)]
        [InlineData(7)]
        public void When_number_of_dice_is_valid_then_retur_true(short numberOfDice)
        {
            var sut = new UnworthyRollOptions
            {
                NumberOfDice = numberOfDice
            };

            var result = sut.IsValid();

            result.Should().BeTrue();
        }
    }
}
