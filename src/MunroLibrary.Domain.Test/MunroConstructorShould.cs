using System;
using Xunit;

namespace MunroLibrary.Domain.Test
{
    public class MunroConstructorShould
    {
        [Fact]
        public void Assign_Parameters_On_Construction()
        {
            // Arrange
            const int id = 5;
            const string name = "Test";
            const decimal heightMeters = 1.1m;
            const string gridRef = "1234567";
            const MunroType munroType = MunroType.Munro;

            // Act
            var sut = new Munro(id, name, heightMeters, gridRef, munroType);

            // Assert
            Assert.Equal(id, sut.Id);
            Assert.Equal(name, sut.Name);
            Assert.Equal(heightMeters, sut.HeightMeters);
            Assert.Equal(gridRef, sut.GridRef);
            Assert.Equal(munroType, sut.MunroType);
        }

        [Fact]
        public void Throw_Exception_If_Id_Is_Zero()
        {
            // Act
            var exception = Assert.Throws<ArgumentException>(() => new Munro(
                0,
                "Test"
                , 1.0m,
                "1234",
                MunroType.Munro));

            // Assert
            Assert.Equal("id must be greater than 0!", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Throw_Exception_If_Name_Is_Null_Empty_Or_Whitespace(string parameter)
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new Munro(
                1,
                parameter,
                1.0m,
                "1234",
                MunroType.Munro));

            // Assert
            Assert.Equal("name", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Throw_Exception_If_GridRef_Is_Null_Empty_Or_Whitespace(string parameter)
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new Munro(
                1,
                "TEST",
                1.0m,
                parameter,
                MunroType.Munro));

            // Assert
            Assert.Equal("gridRef", exception.ParamName);
        }
    }
}
