using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.DatabaseModels
{
    public class SchedulerModel
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool? IsAllDay { get; set; }
        public bool SpecifiedDay { get; set; }
        public int IntervalNumber { get; set; } = 1;
        public string RecurrenceRule { get; set; } = "";
        public string StartTimezone { get; set; } = "";
        public string EndTimezone { get; set; } = "";      
    }
}
