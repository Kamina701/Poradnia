using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Common
{
    public class PaginatedList<T> : List<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => (PageNumber > 1);
        public bool HasNextPage => (PageNumber < TotalPages);
        public PaginatedList(IEnumerable<T> source, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(source);
        }
        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize = 2)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        /// <summary>
        /// Create PaginatedList without skipping any item.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PaginatedList<TDestination> Create<TDestination>(List<TDestination> list, int count, int pageNumber, int pageSize)
        {
            return new PaginatedList<TDestination>(list, count, pageNumber, pageSize);
        }
    }
}
