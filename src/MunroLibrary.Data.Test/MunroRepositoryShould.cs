using MunroLibrary.Data.Test.Mocks;
using MunroLibrary.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace MunroLibrary.Data.Test
{
    public class MunroRepositoryShould
    {
        [Fact]
        public void Get_Data()
        {
            // Arrange
            var expectedResult = new List<Munro>();

            // Act
            var sut = new MockMunroRepository();
            var result = sut.GetData();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
