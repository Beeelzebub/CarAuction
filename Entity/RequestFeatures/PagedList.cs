using System;
using System.Collections.Generic;
using System.Linq;

namespace Entity.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(IEnumerable<T> cars, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)(count / (double) pageSize)
            };
            AddRange(cars);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> cars, int pageNumber, int pageSize)
        {
            var count = cars.Count();
            var returnCars = cars
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
            return new PagedList<T>(returnCars, count, pageNumber, pageSize);
        }
    }
}
