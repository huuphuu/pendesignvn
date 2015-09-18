using System;
using System.Collections.Generic;

namespace PenDesign.Common.Paging
{
    public class Page<T> : IPage<T> where T : class
    {

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<T> Entities { get; set; }

        public Page() { }

        public Page(int currentPage, int pageSize, int totalCount)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            PageRecalculator();
        }

        public Page(int currentPage, int pageSize, int totalCount, IEnumerable<T> entities)
            :this (currentPage, pageSize, totalCount)
        {
            this.Entities = entities;
        }

        public void PageRecalculator()
        {
            TotalPages = TotalCount / PageSize;
            if (TotalPages * PageSize < TotalCount) ++TotalPages;
            if (TotalPages < 1) TotalPages = 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages) CurrentPage = TotalPages;
        }
    }
}
