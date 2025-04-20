using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Pages.Components.Csharp
{
    public class AlarmCollection
    {
        public static Dictionary<int, string> CollectionOfSystemAlarm1 = new Dictionary<int, string>()
        {
            { 0, "System_伺服通訊異常" },{ 1, "System_HBM通訊異常" },{ 2, "System_清水壓力異常"},{ 3, "System_酸液壓力異常"},{ 4, "System_Buf.A壓力異常"},
            { 5, "System_Buf.B壓力異常"},{ 6, "System_氣壓缸開合異常"},{ 7, "System_絞盤光纖訊號異常"},{ 8, "System_電極過捲異常"},{ 9, "System_E_00"},{ 10, "System_E_01"},
            { 11, "System_E_02"},{ 12, "System_E_03"},{ 13, "System_E_04"},{ 14, "System_E_04"},{ 15, "System_E_31"}
        };

        public static Dictionary<int, string> CollectionOfDriverAlarm = new Dictionary<int, string>()
        {
            { 1, "Driver_過電流" },{ 2, "Driver_過電壓" },{ 3, "Driver_低電壓"},{ 4, "Driver_馬達匹配異常"},{ 5, "Driver_回生錯誤"},
            { 6, "Driver_過負載"},{ 7, "Driver_速度控制誤差過大"},{ 8, "Driver_異常脈波控制命令"},{ 9, "Driver_位置控制誤差過大"},{ 16, "Driver_回生狀態下電壓異常"},{ 17, "Driver_CN2通訊失敗"},
            { 19, "Driver_緊急停止"},{ 20, "Driver_反向極限異常"},{ 21, "Driver_正向極限異常"},{ 22, "Driver_IGBT溫度異常"},{ 23, "Driver_記憶體異常"},

            { 24, "Driver_OA與OB輸出異常" },{ 32, "Driver_串列通訊逾時" },{ 34, "Driver_主迴路電源異常"},{ 35, "Driver_預先過負載警告"},{ 36, "Driver_編碼器初始磁場錯誤"},
            { 37, "Driver_編碼器內部錯誤"},{ 38, "Driver_編碼器內部資料可靠度錯誤"},{ 39, "Driver_編碼器內部重置錯誤"},{ 40, "Driver_電池電壓異常或編碼器內部錯誤"},{ 41, "Driver_格雷碼錯誤"},{ 42, "Driver_編碼器圈數計數錯誤"},
            { 43, "Driver_馬達資料異常"},{ 44, "Driver_驅動器過負載"},{ 45, "Driver_防堵轉保護"},{ 48, "Driver_馬達碰撞錯誤"},{ 49, "Driver_馬達動力線錯線偵測"},

            { 50, "Driver_編碼器振動異常" },{ 51, "Driver_馬達異常" },{ 52, "Driver_編碼器內部通訊異常"},{ 53, "Driver_編碼器溫度超過保護上限"},{ 54, "Driver_編碼器異警狀態錯誤"},
            { 66, "Driver_類比速度指令的電壓輸入過高"},{ 68, "Driver_驅動器功能使用率警告"},{ 69, "Driver_電子齒輪比設定錯誤"},{ 83, "Driver_馬達參數異常"},{ 86, "Driver_馬達速度過高"},{ 92, "Driver_馬達位置回授異常"},
            { 96, "Driver_絕對位置遺失"},{ 97, "Driver_編碼器電壓過低"},{ 98, "Driver_絕對型位置圈數溢位_編碼器"},{ 100, "Driver_編碼器振動警告"},{ 102, "Driver_絕對型位置圈數溢位_驅動器"},

            { 103, "Driver_編碼器溫度警告" },{ 104, "Driver_絕對型資料IO傳輸錯誤" },{ 105, "Driver_馬達型式錯誤"},{ 107, "Driver_驅動器內部座標與編碼器座標誤差過大"},{ 108, "Driver_編碼器型態無法識別"},
            { 109, "Driver_絕對位置建立未完成"},{ 112, "Driver_編碼器讀寫未完成警告"},{ 113, "Driver_編碼器圈數錯誤"},{ 114, "Driver_編碼器過速度"},{ 115, "Driver_編碼器記憶體錯誤"},{ 116, "Driver_編碼器單圈絕對位置錯誤"},
            { 117, "Driver_編碼器絕對圈數錯誤"},{ 121, "Driver_編碼器參數設置未完成錯誤"},{ 122, "Driver_編碼器Z相位置遣失"},{ 123, "Driver_編碼器記憶體忙碌"},{ 124, "Driver_馬達轉速超過200rpm時下達清除絕對位置"},

            { 125, "Driver_沒有解除AL07C就重新上電馬達停止運轉"},{ 126, "Driver_編碼器清除程序錯誤"},{ 127, "Driver_編碼器版號異常"},{ 131, "Driver_驅動器輸出電流過大"},{ 133, "Driver_回生設定異常"},
            { 134, "Driver_回生電阻過負載" },{ 136, "Driver_驅動器功能使用率報警" },{ 137, "Driver_電流感測遭受干擾"},{ 138, "Driver_自動增益調整命令異常"},{ 139, "Driver_自動增益調整停止時間過短"},
            { 140, "Driver_自動增益調整慣量估測異常"},{ 153, "Driver_DSP韌體錯誤"},{ 156, "Driver_參數重置失敗"},{ 157, "Driver_電容充電異常"},{ 166, "Driver_驅動器與馬達的絕對位置座標不匹配"},{ 863, "Driver_緊急停止_減速過程中"},

            { 1058, "Driver_控制電源斷電造成寫入失敗"},{ 1313, "Driver_撓性補償參數異常"},{ 3121, "Driver_馬達動力線斷線偵測"},{ 3291, "Driver_驅動器機種碼異常"},
        };

        public static Dictionary<int, string> CollectionOfCommonAlarm = new Dictionary<int, string>()
        {
            { 0, "酸液液位過低" },{ 1, "Buf.A液位過低" },{ 2, "Buf.B液位過低"},{ 3, "氣壓壓力異常"},
        };
    }
}
