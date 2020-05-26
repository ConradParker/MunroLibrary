using MunroLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MunroLibrary.Data
{
    public interface IMunroRepository
    {
        IEnumerable<Munro> GetData();
        PagedResult<Munro> GetPaged(
            int page,
            int pageSize,
            string[] sortFields,
            bool sortDescending,
            Expression<Func<Munro, bool>> filterBy);
    }
}
