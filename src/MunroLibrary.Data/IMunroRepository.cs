using MunroLibrary.Domain;
using System.Collections.Generic;

namespace MunroLibrary.Data
{
    public interface IMunroRepository
    {
        IEnumerable<Munro> GetData();
    }
}
