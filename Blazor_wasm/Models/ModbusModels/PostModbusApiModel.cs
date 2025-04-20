using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.ModbusModels
{
    public class PostModbusApiModel
    {
        public int startAddress { get; set; }
        public int value { get; set; }
    }
}
