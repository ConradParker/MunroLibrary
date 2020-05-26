using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using MunroLibrary.Api.Dtos;
using MunroLibrary.Api.Extensions;
using MunroLibrary.Data;
using MunroLibrary.Domain;

namespace MunroLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunrosController : ControllerBase
    {
        private readonly IMunroRepository repository;
 
        public MunrosController(IMunroRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Munros
        [HttpGet]
        public PagedResult<Munro> Get([FromQuery] MunroQueryDto query)
        {
            return repository.GetPaged(
                query.PageNumber,
                query.PageSize,
                query.SortFields,
                query.SortDescending,
                GetFilterExpression(query));
        }

        private Expression<Func<Munro, bool>> GetFilterExpression(MunroQueryDto query)
        {
            Expression<Func<Munro, bool>> filterExpression = null;

            if (query.MunroType != null)
            {
                filterExpression = filterExpression.AndWithNullCheck(x => x.MunroType.Equals(query.MunroType));
            }
            if (query.MinHeight != null)
            {
                filterExpression = filterExpression.AndWithNullCheck(x => x.HeightMeters > query.MinHeight);
            }
            if (query.MaxHeight != null)
            {
                filterExpression = filterExpression.AndWithNullCheck(x => x.HeightMeters < query.MaxHeight);
            }

            return filterExpression;
        }
    }
}
