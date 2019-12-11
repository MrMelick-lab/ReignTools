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

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void When_only_one_special_dice_is_present_then_is_valid(short expertDice, bool masterDice)
        {
            var sut = new RollOptions
            {
                NumberOfDice = 2,
                ExpertDice = expertDice,
                MasterDice = masterDice
            };

            var result = sut.IsValid();

            result.Should().BeTrue();
        }

        [Fact]
        public void When_both_special_dice_are_presents_then_is_not_valid()
        {
            var sut = new RollOptions
            {
                NumberOfDice = 2,
                ExpertDice = 1,
                MasterDice = true
            };

            var result = sut.IsValid();

            result.Should().BeFalse();
        }
    }
}
