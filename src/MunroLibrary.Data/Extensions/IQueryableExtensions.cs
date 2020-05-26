using System;
using System.Linq;
using System.Linq.Expressions;

namespace MunroLibrary.Data.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Paging and sorting extension.
        /// </summary>
        /// <typeparam name="T">Entity.</typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedResult<T> GetPaged<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
            where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = query.Count(),
            };

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string memberPath)
        {
            return source.OrderByHelper(memberPath, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string memberPath)
        {
            return source.OrderByHelper(memberPath, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IQueryable<T> source, string memberPath)
        {
            return source.OrderByHelper(memberPath, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IQueryable<T> source, string memberPath)
        {
            return source.OrderByHelper(memberPath, "ThenByDescending");
        }

        private static IOrderedQueryable<T> OrderByHelper<T>(this IQueryable<T> source, string propertyName, string method)
        {
            var parameter = Expression.Parameter(typeof(T));

            try
            {
                var member = propertyName.Split('.')
                    .Aggregate(
                        (Expression)parameter,
                        Expression.PropertyOrField);
                var keySelector = Expression.Lambda(member, parameter);
                var methodCall = Expression.Call(
                    typeof(Queryable),
                    method,
                    new[] { parameter.Type, member.Type },
                    source.Expression,
                    Expression.Quote(keySelector));
                return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Sort field error. Property '{propertyName}' is not known!");
            }
        }
    }
}