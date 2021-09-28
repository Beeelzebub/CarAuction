﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Entity.RequestFeatures
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)(count / (double)pageSize) + 1;
            Items = items;
        }

        public PagedList()
        {

        }
    }
}
