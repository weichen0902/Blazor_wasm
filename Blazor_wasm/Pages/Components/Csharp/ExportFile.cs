using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_wasm.Pages;

namespace Blazor_wasm.Pages.Components.Csharp
{
    public class ExportFile
    {
        public async Task DownloadFile<T>(List<T>? calData)
        {
            if (calData == null || !calData.Any())
                return;

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\caldata\\";
            var fileName = Path.Combine(desktopPath, $"D2CalData_{DateTime.Now:yyyyMMdd}.csv");

            using var writer = new StreamWriter(fileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(calData);
        }
    }
}
