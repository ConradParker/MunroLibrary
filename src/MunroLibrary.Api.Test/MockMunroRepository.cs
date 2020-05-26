using MunroLibrary.Data;
using MunroLibrary.Domain;
using System;
using System.Linq.Expressions;

namespace MunroLibrary.Api.Test
{
    public class MockMunroRepository : IMunroRepository
    {
        public PagedResult<Munro> GetPaged(int page, int pageSize, string[] sortFields, bool sortDescending, Expression<Func<Munro, bool>> filterBy)
        {
            throw new NotImplementedException();
        }
    }
}
