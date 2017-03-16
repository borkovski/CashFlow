using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Transfer
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public int Direction { get; set; }
        public DateTime Date { get; set; }
        public bool IsRepeated { get; set; }
        public int? RepeatPeriod { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? IsContinuous { get; set; }

        public Transfer()
        {
        }
    }
}
