using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_wasm.Services;
using Blazor_wasm.Models.DatabaseModels;
using Blazor_wasm.Shared;
using Blazor_wasm.Models.AuthModels;

namespace Blazor_wasm.Controller
{
    public class GetAllCalData
    {
        protected IDataService _dataService;

        public List<CalDataModel>? d1CalData = new List<CalDataModel>();
        public List<CalDataModel>? d2CalData = new List<CalDataModel>();

        public DateTime minimumDateTime{ get; set; }
        public DateTime maximumDateTime { get; set; }

        public GetAllCalData(IDataService dataService)
        {
            _dataService = dataService;
            UpdateData();          
        }

        public async Task<(MainResponse<object>, MainResponse<object>)> UpdateData()
        {
            var result1 = await _dataService.GetD1AllCAL();
            if (result1.IsSuccess && result1.Content != null)
            {
                d1CalData = result1?.Content as List<CalDataModel>;
                d1CalData?.Sort((a, b) => b.DateTime.CompareTo(a.DateTime));           
            }

            var result2 = await _dataService.GetD2AllCAL();
            if (result2.IsSuccess && result2.Content != null)
            {
                d2CalData = result2?.Content as List<CalDataModel>;
                d2CalData?.Sort((a, b) => b.DateTime.CompareTo(a.DateTime));
            }
                

            if(d1CalData?.Count > 0)
            {
                minimumDateTime = d1CalData.Min(d => d.DateTime).AddDays(-10);
                maximumDateTime = d1CalData.Max(d => d.DateTime).AddDays(10);
            }
            else
            {
                minimumDateTime = DateTime.Now.AddDays(-10);
                maximumDateTime = DateTime.Now.AddDays(10);
            }

            return (result1!, result2!);
        }       
    }
}
