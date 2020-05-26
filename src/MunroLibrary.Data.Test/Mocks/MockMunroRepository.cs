using MunroLibrary.Domain;
using System.Collections.Generic;

namespace MunroLibrary.Data.Test.Mocks
{
    class MockMunroRepository : IMunroRepository
    {
        public IEnumerable<Munro> GetData()
        {
            throw new System.NotImplementedException();
        }
    }
}
