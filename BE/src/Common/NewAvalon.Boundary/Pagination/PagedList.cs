using System;
using System.Collections.Generic;
using System.Linq;

namespace NewAvalon.Boundary.Pagination
{
    public class PagedList<T>
    {
        private readonly HashSet<T> _items = new();

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddItems(items);
        }

        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public List<T> Items => _items.ToList();

        private void AddItems(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                _items.Add(item);
            }
        }
    }
}
