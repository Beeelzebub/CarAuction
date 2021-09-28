using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.RequestFeatures
{
    public static class EnumerableExtension
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, RequestParameters parameters)
        {
            var count = source.Count();
            var returnCars = source
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToList();

            return new PagedList<T>(returnCars, count, parameters.PageNumber, parameters.PageSize);
        }
    }
}
