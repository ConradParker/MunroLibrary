using MunroLibrary.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace MunroLibrary.Data.Test
{
    public class MunroRepositoryShould
    {
        /// <summary>
        /// Just test the expected CSV lines until we change the approach for retrieving data
        /// </summary>
        [Fact]
        public void Return_Some_Data()
        {
            // Arrange
            var expectedCount = 499;
            Expression<Func<Munro, bool>> filter = null;

            // Act
            var sut = new MunroRepository();
            var result = sut.GetPaged(1, expectedCount, null, false, filter);

            // Assert
            Assert.Equal(expectedCount, result.Results.Count());
            Assert.Equal(expectedCount, result.TotalCount);
        }
    }
}
