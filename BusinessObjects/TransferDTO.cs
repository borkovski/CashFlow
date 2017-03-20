using BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class TransferDTO
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public Direction Direction { get; set; }
        public DateTime Date { get; set; }
        public bool IsRepeated { get; set; }
        public RepeatPeriod? RepeatPeriod { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? IsContinuous { get; set; }
        public long Account { get; set; }

        public TransferDTO()
        {
        }
    }
}
