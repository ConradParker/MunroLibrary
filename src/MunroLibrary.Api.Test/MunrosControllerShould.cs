using MunroLibrary.Api.Controllers;
using MunroLibrary.Api.Dtos;
using MunroLibrary.Data;
using MunroLibrary.Domain;
using System;
using System.Linq;
using Xunit;

namespace MunroLibrary.Api.Test
{
    public class MunrosControllerShould
    {
        private readonly IMunroRepository repository;
        private readonly MunroQueryDto query;
        private readonly MunrosController controller;

        public MunrosControllerShould()
        {
            // Would normally mock here but no need in this instance.
            repository = new MunroRepository();

            // Set up controller
            controller = new MunrosController(repository);

            // Set up basic query dto
            query = new MunroQueryDto();
        }

        [Theory]
        [InlineData(MunroType.MUN)]
        [InlineData(MunroType.TOP)]
        public void Return_Correct_MunroType(MunroType munroType)
        {
            // Arrange
            query.PageSize = 100;
            query.MunroType = munroType;

            // Act
            var result = controller.Get(query);

            // Assert
            Assert.Equal(query.PageSize, result.Results.Where(x => x.MunroType.Equals(munroType)).Count());
        }

        [Fact]
        public void Return_Mixed_MunroTypes_If_Not_Filtered()
        {
            // Act
            var result = controller.Get(query);

            // Assert
            Assert.True(result.Results.Where(x => x.MunroType.Equals(MunroType.MUN)).Count() > 0);
            Assert.True(result.Results.Where(x => x.MunroType.Equals(MunroType.TOP)).Count() > 0);
        }

        [Fact]
        public void Return_Height_Sorted_Asc_Results()
        {
            // Arrange
            query.SortFields = new string[] { "HeightMeters" };

            // Act
            var result = controller.Get(query);

            // Use the returned results and sort for comparison
            var expectedList = result.Results.OrderBy(x => x.HeightMeters).ToList();

            // Assert
            Assert.Equal(expectedList, result.Results);
        }


        [Fact]
        public void Return_Height_Sorted_Desc_Results()
        {
            // Arrange
            query.SortFields = new string[] { "HeightMeters" };
            query.SortDescending = true;

            // Act
            var result = controller.Get(query);

            // Use the returned results and sort for comparison
            var expectedList = result.Results.OrderByDescending(x => x.HeightMeters).ToList();

            // Assert
            Assert.Equal(expectedList, result.Results);
        }


        [Fact]
        public void Return_Name_Sorted_Asc_Results()
        {
            // Arrange
            query.SortFields = new string[] { "Name" };

            // Act
            var result = controller.Get(query);

            // Use the returned results and sort for comparison
            var expectedList = result.Results.OrderBy(x => x.Name).ToList();

            // Assert
            Assert.Equal(expectedList, result.Results);
        }

        [Fact]
        public void Return_Name_Sorted_Desc_Results()
        {
            // Arrange
            query.SortFields = new string[] { "Name" };
            query.SortDescending = true;

            // Act
            var result = controller.Get(query);

            // Use the returned results and sort for comparison
            var expectedList = result.Results.OrderByDescending(x => x.Name).ToList();

            // Assert
            Assert.Equal(expectedList, result.Results);
        }

        [Fact]
        public void Return_Limited_Results()
        {
            // Arrange
            query.PageSize = 10;

            // Act
            var result = controller.Get(query);

            // Assert
            Assert.Equal(query.PageSize, result.Results.Count());
        }

        [Fact]
        public void Only_Return_Results_Over_MinHeight()
        {
            // Arrange
            query.MinHeight = 1000m;

            // Act
            var result = controller.Get(query);

            // Assert
            Assert.Empty(result.Results.Where(x => x.HeightMeters < query.MinHeight));
        }


        [Fact]
        public void Only_Return_Results_Under_MaxHeight()
        {
            // Arrange
            query.MaxHeight = 1000m;

            // Act
            var result = controller.Get(query);

            // Assert
            Assert.Empty(result.Results.Where(x => x.HeightMeters > query.MinHeight));
        }

        [Fact]
        public void Only_Return_Results_Between_MinHeight_And_Maximum_Height()
        {
            // Arrange
            query.MinHeight = 1000m;
            query.MaxHeight = 2000m;

            // Act
            var result = controller.Get(query);

            // Assert
            Assert.Empty(result.Results.Where(x => x.HeightMeters < query.MinHeight));
            Assert.Empty(result.Results.Where(x => x.HeightMeters > query.MaxHeight));
            Assert.NotEmpty(result.Results);
        }

        [Fact]
        public void Throw_Error_If_MinHeight_More_Than_MaxHeight()
        {
            // Arrange
            query.MinHeight = 10m;
            query.MaxHeight = 5m;

            // Act
            var exception = Assert.Throws<ArgumentException>(() => controller.Get(query));

            // Assert
            Assert.Equal("MinHeight must not be greater or equal to MaxHeight", exception.Message);
        }


        [Fact]
        public void Return_Name_Then_Height_Sorted_Results()
        {
            // Arrange
            query.SortFields = new string[] { "Name", "HeightMeters" };

            // Act
            var result = controller.Get(query);

            // Use the returned results and sort for comparison
            var expectedList = result.Results.OrderBy(x => x.Name).ThenBy(x => x.HeightMeters).ToList();

            // Assert
            Assert.Equal(expectedList, result.Results);
        }
    }
}
