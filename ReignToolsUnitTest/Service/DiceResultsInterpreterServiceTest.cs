using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using ReignTools.Service;
using ReignTools.Entities.Business;

namespace ReignToolsUnitTest.Service
{
    public class DiceResultsInterpreterServiceTest
    {
        private readonly DiceResultsInterpreterService sut;

        public DiceResultsInterpreterServiceTest()
        {
            sut = new DiceResultsInterpreterService(); 
        }

        [Fact]
        public void When_two_dices_are_differant_then_no_sets()
        {
            var results = sut.GetSetsFromDiceRolls(new List<short> { 1, 2 });

            results.Should().BeEmpty();
        }

        [Fact]
        public void When_ten_dices_are_differant_then_no_sets()
        {
            var results = sut.GetSetsFromDiceRolls(new List<short> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            results.Should().BeEmpty();
        }

        [Fact]
        public void When_5_dices_with_one_set_of_2x7_then_one_sets()
        {
            var expected = new List<Sets>
            {
                new Sets
                {
                    Width = 2,
                    Height = 7
                }
            };

            var results = sut.GetSetsFromDiceRolls(new List<short> { 1, 2, 7, 4, 7});

            results.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_7_dices_with_tow_set_of_2x7__and_3x8_then_two_sets()
        {
            var expected = new List<Sets>
            {
                new Sets
                {
                    Width = 2,
                    Height = 7
                },

                new Sets
                {
                    Width = 3,
                    Height = 8
                },
            };

            var results = sut.GetSetsFromDiceRolls(new List<short> { 8, 2, 7, 8, 7, 8, 1 });

            results.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_10_dices_with_5_set_of_2_then_five_sets()
        {
            var expected = new List<Sets>
            {
                new Sets
                {
                    Width = 2,
                    Height = 1
                },

                new Sets
                {
                    Width = 2,
                    Height = 2
                },


                new Sets
                {
                    Width = 2,
                    Height = 3
                },

                new Sets
                {
                    Width = 2,
                    Height = 4
                },

                new Sets
                {
                    Width = 2,
                    Height = 5
                },
            };

            var results = sut.GetSetsFromDiceRolls(new List<short> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 });

            results.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_10_dices_with_one_set_of_10x10_then_one_sets()
        {
            var expected = new List<Sets>
            {
                new Sets
                {
                    Width = 10,
                    Height = 10
                }
            };

            var results = sut.GetSetsFromDiceRolls(new List<short> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 });

            results.Should().BeEquivalentTo(expected);
        }
    }
}
