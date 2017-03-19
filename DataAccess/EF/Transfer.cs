using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class Transfer
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int DirectionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsRepeated { get; set; }
        public int? RepeatPeriodId { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? IsContinuous { get; set; }

        public virtual DictDirection Direction { get; set; }
        public virtual DictRepeatPeriod RepeatPeriod { get; set; }
    }
}
