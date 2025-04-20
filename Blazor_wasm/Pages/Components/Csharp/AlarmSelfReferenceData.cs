using Blazor_wasm.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blazor_wasm.Pages.Components.Csharp
{
    public class AlarmSelfReferenceData
    {
        public static List<AlarmSelfReferenceData> tree = new List<AlarmSelfReferenceData>();

        [Key]
        public int? Id { get; set; }
        public DateTime DateTime { get; set; }
        public string AlarmContent { get; set; }
        public string AlarmStatus { get; set; }
        public int? ParentID { get; set; }
        public ushort AlarmNumber { get; set; }

        public AlarmSelfReferenceData() { }

        public static List<AlarmSelfReferenceData> GetTree(List<AlarmData> alarmData)
        {
            tree.Clear();

            foreach (var data in alarmData)
            {
                tree.Add(new AlarmSelfReferenceData() { Id = data.Id, DateTime = data.DateTime, AlarmContent = data.AlarmContent, AlarmStatus = data.AlarmStatus, ParentID = null, AlarmNumber = data.AlarmNumber });
            }
            return tree;
        }
    }
}
