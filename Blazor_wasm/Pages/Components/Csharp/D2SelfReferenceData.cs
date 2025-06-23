using Blazor_wasm.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Blazor_wasm.Pages.Components.Csharp
{
    public class D2SelfReferenceData
    {
        public static List<D2SelfReferenceData> tree = new List<D2SelfReferenceData>();
        [Key]
        public int? Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Health { get; set; }
        public double Zero { get; set; }
        public double Slope { get; set; }
        public int? ParentID { get; set; }

        public D2SelfReferenceData() { }

        public static List<D2SelfReferenceData> GetTree(List<CalDataModel> D2CalData)
        {
            tree.Clear();

            foreach (var data in D2CalData)
            {
                tree.Add(new D2SelfReferenceData() { Id = data.Id, DateTime = data.DateTime, Health = data.Health, Zero = data.Zero, Slope = data.Slope, ParentID = null });
            }
            return tree;
        }
    }
}
