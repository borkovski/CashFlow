using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects.Utilities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MappingPropertyAttribute : Attribute
    {
        public bool IsRange { get; set; }
        public string[] MappedPropertyPath { get; set; }
        public MappingPropertyAttribute(string[] mappedPropertyPath)
        {
            MappedPropertyPath = mappedPropertyPath;
        }
        public MappingPropertyAttribute(string mappedProperty)
        {
            MappedPropertyPath = new string[] { mappedProperty };
        }
        public MappingPropertyAttribute(bool isRange)
        {
            IsRange = isRange;
        }
    }
}
