using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects.Utilities
{
    public class PagedList<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }

        public PagedList()
        {
            Items = new List<T>();
        }

        public PagedList(List<T> items)
        {
            Items = items;
        }
    }
}
