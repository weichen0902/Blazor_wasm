using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.DatabaseModels
{
    public class RealTimepHModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double? D1pH { get; set; }
        public double? D2pH { get; set; }
    }
}

