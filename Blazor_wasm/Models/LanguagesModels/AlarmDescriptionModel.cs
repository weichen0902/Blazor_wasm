using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.LanguagesModels
{
    public class AlarmDescriptionModel
    {
        public static Dictionary<int, string> CollectionOfSystemAlarm1 = new Dictionary<int, string>()
        {
            { 1, "alarm_system0" },{ 2, "alarm_system1" },{ 4, "alarm_system2"},{ 8, "alarm_system3"},{ 16, "alarm_system4"},
            { 32, "alarm_system5"},{ 64, "alarm_system6"},{ 128, "alarm_system7"},{ 256, "alarm_system8"},{ 512, "alarm_system9"},{ 1024, "alarm_system10"},
            { 2048, "alarm_system11"},{ 4096, "alarm_system12"},{ 8192, "alarm_system13"},{ 16384, "alarm_system14"},{ 32768, "alarm_system15"}
        };

        public static Dictionary<int, string> CollectionOfDriverAlarm = new Dictionary<int, string>()
        {
            { 1, "alarm_driver1" },{ 2, "alarm_driver2" },{ 3, "alarm_driver3"},{ 4, "alarm_driver4"},{ 5, "alarm_driver5"},
            { 6, "alarm_driver6"},{ 7, "alarm_driver7"},{ 8, "alarm_driver8"},{ 9, "alarm_driver9"},{ 16, "alarm_driver16"},{ 17, "alarm_driver17"},
            { 19, "alarm_driver19"},{ 20, "alarm_driver20"},{ 21, "alarm_driver21"},{ 22, "alarm_driver22"},{ 23, "alarm_driver23"},

            { 24, "alarm_driver24" },{ 32, "alarm_driver32" },{ 34, "alarm_driver34"},{ 35, "alarm_driver35"},{ 36, "alarm_driver36"},
            { 37, "alarm_driver37"},{ 38, "alarm_driver38"},{ 39, "alarm_driver39"},{ 40, "alarm_driver40"},{ 41, "alarm_driver41"},{ 42, "alarm_driver42"},
            { 43, "alarm_driver43"},{ 44, "alarm_driver44"},{ 45, "alarm_driver45"},{ 48, "alarm_driver48"},{ 49, "alarm_driver49"},

            { 50, "alarm_driver50" },{ 51, "alarm_driver51" },{ 52, "alarm_driver52"},{ 53, "alarm_driver53"},{ 54, "alarm_driver54"},
            { 66, "alarm_driver66"},{ 68, "alarm_driver68"},{ 69, "alarm_driver69"},{ 83, "alarm_driver83"},{ 86, "alarm_driver86"},{ 92, "alarm_driver92"},
            { 96, "alarm_driver96"},{ 97, "alarm_driver97"},{ 98, "alarm_driver98"},{ 100, "alarm_driver100"},{ 102, "alarm_driver102"},

            { 103, "alarm_driver103" },{ 104, "alarm_driver104" },{ 105, "alarm_driver105"},{ 107, "alarm_driver107"},{ 108, "alarm_driver108"},
            { 109, "alarm_driver109"},{ 112, "alarm_driver112"},{ 113, "alarm_driver113"},{ 114, "alarm_driver114"},{ 115, "alarm_driver115"},{ 116, "alarm_driver116"},
            { 117, "alarm_driver117"},{ 121, "alarm_driver121"},{ 122, "alarm_driver122"},{ 123, "alarm_driver123"},{ 124, "alarm_driver124"},

            { 125, "alarm_driver125"},{ 126, "alarm_driver126"},{ 127, "alarm_driver127"},{ 131, "alarm_driver131"},{ 133, "alarm_driver133"},
            { 134, "alarm_driver134" },{ 136, "alarm_driver136" },{ 137, "alarm_driver137"},{ 138, "alarm_driver138"},{ 139, "alarm_driver139"},
            { 140, "alarm_driver140"},{ 153, "alarm_driver153"},{ 156, "alarm_driver156"},{ 157, "alarm_driver157"},{ 166, "alarm_driver166"},{ 863, "alarm_driver863"},

            { 1058, "alarm_driver1058"},{ 1313, "alarm_driver1313"},{ 3121, "alarm_driver3121"},{ 3291, "alarm_driver3291"},
        };

        public static Dictionary<int, string> CollectionOfCommonAlarm = new Dictionary<int, string>()
        {
            { 1, "alarm_common0" },{ 2, "alarm_common1" },{ 4, "alarm_common2"},{ 8, "alarm_common3"},
        };
    }
}
