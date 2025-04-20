using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.DatabaseModels
{
    public class FieldDataModel
    {
        [Key]
        public int Id { get; set; }

        public string Field { get; set; }
    }
}
