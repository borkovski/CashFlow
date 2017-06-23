using CashFlow.BusinessObjects.Utilities;
using System;

namespace CashFlow.BusinessObjects
{
    public class TransferDto
    {
        public long? Id { get; set; }
        public long AccountFromId { get; set; }
        [MappingProperty(new string[] {"AccountFrom", "Name"})]
        public string AccountFrom { get; set; }
        public long AccountToId { get; set; }
        [MappingProperty(new string[] { "AccountTo", "Name" })]
        public string AccountTo { get; set; }
        [MappingProperty("Name")]
        public string Title { get; set; }
        [MappingProperty(true)]
        public decimal Amount { get; set; }
        [MappingProperty(true)]
        public DateTime TransferDate { get; set; }
    }
}
