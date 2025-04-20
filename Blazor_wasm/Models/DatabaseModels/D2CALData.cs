using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.DatabaseModels
{
    public class D2CalData
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ushort Health { get; set; }
        public double Zero { get; set; }
        public double Slope { get; set; }
        public double LifeSpan { get; set; }
    }
}
