using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.DatabaseModels
{
    public class CalDataModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public ushort Health { get; set; }

        public double Zero { get; set; }

        public double Slope { get; set; }
        public int ActualRemainingDays { get; set; }
        public double EndurancePercentage { get; set; }

    }
}
