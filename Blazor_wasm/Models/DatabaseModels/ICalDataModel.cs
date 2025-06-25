using System.ComponentModel.DataAnnotations;

namespace Blazor_wasm.Models.DatabaseModels
{
    public interface ICalDataModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public ushort Health { get; set; }

        public double Zero { get; set; }

        public double Slope { get; set; }
        public ushort ActualRemainingDays { get; set; }
        public double EndurancePercentage { get; set; }
    }
}
