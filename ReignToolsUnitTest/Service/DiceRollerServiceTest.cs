using Xunit;
using Moq;
using AutoFixture.AutoMoq;
using AutoFixture;
using ReignTools.Service;
using ReignTools.Entities.Options;
using FluentAssertions;
using System.Collections.Generic;
using ReignTools.Entities.Business;
using System.Linq;

namespace ReignToolsUnitTest.Service
{
    public class DiceRollerServiceTest
    {
        private readonly IFixture _fixture;
        private readonly DiceRollerService sut;
        private readonly Mock<IDiceResultsInterpreterService> mockDiceResultsInterpreterService;
        private readonly Mock<IDiceResultUIService> mockDiceResultUIService;
        private readonly Mock<IConsoleReaderService> mockConsoleReaderService;

        public DiceRollerServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
            mockDiceResultsInterpreterService = _fixture.Freeze<Mock<IDiceResultsInterpreterService>>();
            mockDiceResultUIService = _fixture.Freeze<Mock<IDiceResultUIService>>();
            mockConsoleReaderService = _fixture.Freeze<Mock<IConsoleReaderService>>();
            sut = _fixture.Create<DiceRollerService>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public void When_rolling_dice_and_number_of_dice_is_valid_then_roll_the_exacter_number_of_dice_in_options(short numberOfDice)
        {
            var rollOptions = CreateRollOptions(numberOfDice);

            var listOfSets = _fixture.CreateMany<Sets>(numberOfDice / 2).ToList();
            mockDiceResultsInterpreterService.Setup(x => x.GetSetsFromDiceRolls(It.IsAny<List<short>>())).Returns(listOfSets);

            var result = sut.Roll(rollOptions);

            result.Should().Be(0);
            mockDiceResultsInterpreterService.Verify(x => x.GetSetsFromDiceRolls(It.Is<List<short>>(l => l.Count == numberOfDice)), Times.Once);
            mockDiceResultUIService.Verify(x => x.ShowResults(It.Is<List<Sets>>(l => l.Equals(listOfSets))), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(11)]
        public void When_rolling_dice_and_number_of_dice_is_not_valid_then_return_before_doing_anything(short numberOfDice)
        {
            var rollOptions = CreateRollOptions(numberOfDice);

            var result = sut.Roll(rollOptions);

            result.Should().Be(-1);
            mockDiceResultsInterpreterService.Verify(x => x.GetSetsFromDiceRolls(It.IsAny<List<short>>()), Times.Never);
            mockDiceResultUIService.Verify(x => x.ShowResults(It.IsAny<List<Sets>>()), Times.Never);
        }

        [Theory]
        [InlineData(2,1)]
        [InlineData(10,1)]
        public void When_rolling_dice_with_expert_dice_and_number_of_dice_is_valid_then_return_must_include_expert_dice(short numberOfDice, short expertDice)
        {
            var rollOptions = CreateRollOptions(numberOfDice, expertDice);

            var result = sut.Roll(rollOptions);

            result.Should().Be(0);
            mockDiceResultsInterpreterService.Verify(x => x.GetSetsFromDiceRolls(It.Is<List<short>>(x => x.Contains(expertDice))), Times.Once);
            mockDiceResultUIService.Verify(x => x.ShowResults(It.IsAny<List<Sets>>()), Times.Once);
        }

        [Fact]
        public void When_rolling_dice_with_master_and_number_of_dice_is_valid_then_return_must_include_expert_dice()
        {
            var rollOptions = new RollOptions { NumberOfDice = 2, MasterDice = true };
            mockConsoleReaderService.Setup(x => x.ReadLine()).Returns(1);

            var result = sut.Roll(rollOptions);

            result.Should().Be(0);
            mockDiceResultsInterpreterService.Verify(x => x.GetSetsFromDiceRolls(It.Is<List<short>>(x => x.Contains(1))), Times.Once);
            mockDiceResultUIService.Verify(x => x.ShowResults(It.IsAny<List<Sets>>()), Times.Once);
            mockDiceResultUIService.Verify(x => x.ShowResults(It.IsAny<List<short>>()), Times.Once);
        }

        [Fact]
        public void When_rolling_dice_with_invalider_master_dice_then_return_error()
        {
            var rollOptions = new RollOptions { NumberOfDice = 10, MasterDice = true };
            mockConsoleReaderService.Setup(x => x.ReadLine()).Returns(-1);

            var result = sut.Roll(rollOptions);

            result.Should().Be(-1);
            mockDiceResultsInterpreterService.Verify(x => x.GetSetsFromDiceRolls(It.Is<List<short>>(x => x.Contains(1))), Times.Never);
            mockDiceResultUIService.Verify(x => x.ShowResults(It.IsAny<List<Sets>>()), Times.Never);
            mockDiceResultUIService.Verify(x => x.ShowResults(It.IsAny<List<short>>()), Times.Once);
        }

        private RollOptions CreateRollOptions(short numberOfDice, short expertDice = 0)
        {
            return _fixture
                .Build<RollOptions>()
                .With(o => o.NumberOfDice, numberOfDice)
                .With(o => o.ExpertDice, expertDice)
                .Without(o => o.MasterDice)
                .Create();
        }
    }
}
