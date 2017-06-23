using CashFlow.BusinessObjects.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CashFlow.BusinessLogic.Utilities
{
    public static class SearchUtilities
    {
        public static IQueryable<TSource> Filter<TSource, TResult>(this IQueryable<TSource> queryable, DataFilter dataFilter)
        {
            return queryable
                .Filter<TSource, TResult>(dataFilter.FilterProperties)
                .Sort<TSource, TResult>(dataFilter.SortPropertyName, dataFilter.IsDescending)
                .Page(dataFilter.Skip, dataFilter.Take);
        }
        
        //TODO - add range ('_from','_to') handling
        public static IQueryable<TSource> Filter<TSource, TResult>(this IQueryable<TSource> queryable, IEnumerable<KeyValuePair<string, string>> filter)
        {
            foreach (var filterField in filter)
            {
                string filterFieldTrimmed = filterField.Key;
                bool isFrom = false;
                bool isTo = false;
                if (filterFieldTrimmed.Contains("_from"))
                {
                    isFrom = true;
                    filterFieldTrimmed = filterFieldTrimmed.Substring(0, filterFieldTrimmed.IndexOf("_from"));
                }
                else if (filterFieldTrimmed.Contains("_to"))
                {
                    isTo = true;
                    filterFieldTrimmed = filterFieldTrimmed.Substring(0, filterFieldTrimmed.IndexOf("_to"));
                }
                string propertyName = GetPropertyInfosWithMapping<TSource, TResult>(filterFieldTrimmed, (isFrom | isTo));
                if (propertyName != null)
                {
                    PropertyInfo efProperty = GetPropertyInfo(typeof(TSource), propertyName);
                    if (efProperty != null)
                    {
                        object convertedFilterValue = Convert.ChangeType(filterField.Value, efProperty.PropertyType);
                        if (efProperty.PropertyType == typeof(string))
                        {
                            queryable = queryable.Where(p => ((string)efProperty.GetValue(p)).Contains((string)convertedFilterValue));
                        }
                        else if (efProperty.PropertyType == typeof(int))
                        {
                            if (isFrom)
                            {
                                queryable = queryable.Where(p => (int)efProperty.GetValue(p) >= (int)convertedFilterValue);
                            }
                            else if (isTo)
                            {
                                queryable = queryable.Where(p => (int)efProperty.GetValue(p) <= (int)convertedFilterValue);
                            }
                            else
                            {
                                queryable = queryable.Where(p => (int)efProperty.GetValue(p) == (int)convertedFilterValue);
                            }
                        }
                        else if (efProperty.PropertyType == typeof(long))
                        {
                            if (isFrom)
                            {
                                queryable = queryable.Where(p => (long)efProperty.GetValue(p) >= (long)convertedFilterValue);
                            }
                            else if (isTo)
                            {
                                queryable = queryable.Where(p => (long)efProperty.GetValue(p) <= (long)convertedFilterValue);
                            }
                            else
                            {
                                queryable = queryable.Where(p => (long)efProperty.GetValue(p) == (long)convertedFilterValue);
                            }
                        }
                        else if (efProperty.PropertyType == typeof(decimal))
                        {
                            if (isFrom)
                            {
                                queryable = queryable.Where(p => (decimal)efProperty.GetValue(p) >= (decimal)convertedFilterValue);
                            }
                            else if (isTo)
                            {
                                queryable = queryable.Where(p => (decimal)efProperty.GetValue(p) <= (decimal)convertedFilterValue);
                            }
                            else
                            {
                                queryable = queryable.Where(p => (decimal)efProperty.GetValue(p) == (decimal)convertedFilterValue);
                            }
                        }
                        else if (efProperty.PropertyType == typeof(DateTime))
                        {
                            if (isFrom)
                            {
                                queryable = queryable.Where(p => (DateTime)efProperty.GetValue(p) >= (DateTime)convertedFilterValue);
                            }
                            else if (isTo)
                            {
                                queryable = queryable.Where(p => (DateTime)efProperty.GetValue(p) <= (DateTime)convertedFilterValue);
                            }
                            else
                            {
                                queryable = queryable.Where(p => (DateTime)efProperty.GetValue(p) == (DateTime)convertedFilterValue);
                            }
                        }
                    }
                }
            }
            return queryable;
        }

        private static IQueryable<TSource> Sort<TSource, TResult>(this IQueryable<TSource> queryable, string sort="", bool isDescending=false)
        {
            string sortProperties = GetPropertyInfosWithMapping<TSource, TResult>(sort);
            if (sortProperties.Count() == 0)
            {
                return queryable;
            }
            string methodName = isDescending ? "OrderByDescending" : "OrderBy";
            return queryable.Provider.CreateQuery<TSource>(GenerateMethodCall<TSource>(queryable, methodName, sortProperties));
        }

        private static IQueryable<T> Page<T>(this IQueryable<T> queryable, int? skip = null, int? take = null)
        {
            if (skip.HasValue)
            {
                queryable = queryable.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                queryable = queryable.Take(take.Value);
            }
            return queryable;
        }

        private static string GetPropertyInfosWithMapping<TSource, TResult>(string propertyName, bool isRange = false)
        {
            PropertyInfo targetProperty = GetPropertyInfo(typeof(TResult), propertyName);
            if(targetProperty == null)
            {
                return null;
            }
            MappingPropertyAttribute mappingPropertyAttribute = targetProperty.GetCustomAttribute<MappingPropertyAttribute>();
            string efProperty = string.Empty;
            if (mappingPropertyAttribute != null)
            {
                if(isRange && !mappingPropertyAttribute.IsRange)
                {
                    return null;
                }
                else if (mappingPropertyAttribute.MappedPropertyPath != null)
                {
                    for (int i = 0; i < mappingPropertyAttribute.MappedPropertyPath.Count(); i++)
                    {
                        if (i == 0)
                        {
                            efProperty += mappingPropertyAttribute.MappedPropertyPath[i];
                        }
                        else
                        {
                            efProperty += "." + mappingPropertyAttribute.MappedPropertyPath[i];
                        }
                    }
                }
                else
                {
                    efProperty = propertyName;
                }
                return efProperty;
            }
            else if(!isRange)
            {
                return propertyName;
            }
            else
            {
                return null;
            }
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetProperties().Where(p => p.Name.ToLowerInvariant() == propertyName.ToLowerInvariant()).FirstOrDefault();
        }

        private static LambdaExpression GenerateSelector<TEntity>(String propertyName, out Type resultType)
        {
            // Create a parameter to pass into the Lambda expression (Entity => Entity.OrderByField).
            var parameter = Expression.Parameter(typeof(TEntity), "Entity");
            //  create the selector part, but support child properties
            PropertyInfo property;
            Expression propertyAccess;
            if (propertyName.Contains('.'))
            {
                // support to be sorted on child fields.
                String[] childProperties = propertyName.Split('.');
                property = GetPropertyInfo(typeof(TEntity), childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = GetPropertyInfo(property.PropertyType, childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = GetPropertyInfo(typeof(TEntity), propertyName);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }
            resultType = property.PropertyType;
            // Create the order by expression.
            return Expression.Lambda(propertyAccess, parameter);
        }

        private static MethodCallExpression GenerateMethodCall<TEntity>(IQueryable<TEntity> source, string methodName, String fieldName)
        {
            Type type = typeof(TEntity);
            Type selectorResultType;
            LambdaExpression selector = GenerateSelector<TEntity>(fieldName, out selectorResultType);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                                            new Type[] { type, selectorResultType },
                                            source.Expression, Expression.Quote(selector));
            return resultExp;
        }
    }
}