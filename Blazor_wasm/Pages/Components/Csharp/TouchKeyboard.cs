using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Blazor_wasm.Pages.Components.Csharp
{
    public class TouchKeyboard
    {
        public Process onScreenKeyboardProc;

        public void Keyboard_InFocus()
        {
            Process[] processArray = Process.GetProcessesByName("TabTip");
            foreach (Process onScreenProcess in processArray)
            {
                onScreenProcess.Kill();
            }

            string progFiles = @"C:\Program Files\Common Files\microsoft shared\ink";
            string onScreenKeyboardPath = Path.Combine(progFiles, "TabTip.exe");
            onScreenKeyboardProc = Process.Start(new ProcessStartInfo
            {
                FileName = onScreenKeyboardPath,
                UseShellExecute = true,
            });
        }

        public void Keyboard_OutFocus()
        {

        }
    }
}
