using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp_Core.Helpers
{
    public class PagedList <T> : List<T>
    {
        public int CurrentPage { get; set; }    
        public int TotalPages { get; set; } 
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public PagedList(List<T> items,int Count, int CurrentPage, int PageSize)
        {
            this.TotalCount = Count;
            this.CurrentPage = CurrentPage;
            this.PageSize = PageSize;
            this.TotalPages = (int) Math.Ceiling((double)Count / (double)PageSize);

            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int CurrentPage, int PageSize)
        {
            var count = await source.CountAsync();
            var list = await source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
            return new PagedList<T>(list, count, CurrentPage, PageSize);
        }
    }
}