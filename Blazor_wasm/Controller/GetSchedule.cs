using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_wasm.Models.DatabaseModels;
using Blazor_wasm.Services;

namespace Blazor_wasm.Controller
{
    public class GetSchedule
    {
        protected IDataService _dataService;

        public List<SchedulerModel>? dataSource { get; set; }

        public GetSchedule(IDataService dataService)
        {
            _dataService = dataService;
            UpdateData();
        }    
        
        public async void UpdateData()
        {
            var result = await _dataService.GetScheduler();
            dataSource = result.Content;
        }
    }
}
