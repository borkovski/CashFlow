using CashFlow.BusinessObjects.Utilities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessLogic.Utilities
{
    public class DataFilterUtilities
    {
        public DataFilter GetDataFilter(string queryString, string defaultSortProperty = "id", int defaultPageSize = 10, bool defaultIsDescending = true)
        {
            DataFilter dataFilter = new DataFilter();
            dataFilter.SortPropertyName = defaultSortProperty;
            dataFilter.Take = defaultPageSize;
            dataFilter.IsDescending = defaultIsDescending;
            Dictionary<string, StringValues> queryStringDictionary = QueryHelpers.ParseQuery(queryString);
            foreach (var queryStringKey in queryStringDictionary)
            {
                foreach (var item in queryStringKey.Value)
                {
                    switch (queryStringKey.Key.ToLowerInvariant())
                    {
                        case "sortpropertyname":
                            dataFilter.SortPropertyName = item;
                            break;
                        case "isdescending":
                            bool isDescending;
                            if (bool.TryParse(item, out isDescending))
                            {
                                dataFilter.IsDescending = isDescending;
                            }
                            break;
                        case "skip":
                            int skip;
                            if (int.TryParse(item, out skip))
                            {
                                dataFilter.Skip = skip;
                            }
                            break;
                        case "take":
                            int take;
                            if (int.TryParse(item, out take))
                            {
                                dataFilter.Take = take;
                            }
                            break;
                        default:
                            dataFilter.FilterProperties.Add(new KeyValuePair<string, string>(queryStringKey.Key, item));
                            break;
                    }
                }
            }
            return dataFilter;
        }
    }
}
