using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects.Utilities
{
    public class DataFilter
    {
        public string SortPropertyName { get; set; }
        public bool IsDescending { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public List<KeyValuePair<string,string>> FilterProperties { get; set; }

        public DataFilter()
        {
            FilterProperties = new List<KeyValuePair<string, string>>();
        }
    }
}
