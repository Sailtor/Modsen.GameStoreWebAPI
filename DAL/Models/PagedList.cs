﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DAL.Models
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList()
        {
        }

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            IEnumerable<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public void WritePaginationData(IHeaderDictionary headers)
        {
            var metadata = new
            {
                TotalCount,
                PageSize,
                CurrentPage,
                TotalPages,
                HasNext,
                HasPrevious
            };
            headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        }
    }
}