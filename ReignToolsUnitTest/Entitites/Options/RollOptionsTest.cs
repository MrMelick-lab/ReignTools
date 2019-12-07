using Xunit;
using FluentAssertions;
using ReignTools.Entities.Options;

namespace ReignToolsUnitTest.Entitites.Options
{
    public class RollOptionsTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(11)]
        public void When_number_of_dice_is_not_between_2_and_10_then_is_not_valid(short numberOfDice)
        {
            var sut = new RollOptions
            {
                NumberOfDice = numberOfDice
            };

            var result = sut.IsValid();

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public void When_number_of_dice_is_between_2_and_10_then_is_valid(short numberOfDice)
        {
            var sut = new RollOptions
            {
                NumberOfDice = numberOfDice
            };

            var result = sut.IsValid();

            result.Should().BeTrue();
        }
    }
}
