using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.DatabaseModels
{
    public class AlarmDataModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ushort AlarmNumber { get; set; }
        public string AlarmContent { get; set; }
        public string AlarmStatus { get; set; }

    }
}
