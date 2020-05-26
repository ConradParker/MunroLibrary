using System.Linq;
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

            // Act
            var sut = new MunroRepository();
            var result = sut.GetData().ToList();

            // Assert
            Assert.Equal(expectedCount, result.Count());
        }
    }
}
