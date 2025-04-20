using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Shared
{
    public class PageTitleService
    {
        public event Action OnChange;

        private string _title;
        public string Title {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
