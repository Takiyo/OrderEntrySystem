using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public abstract class ViewModel
    {
        public string DisplayName { get; private set; }

        public ViewModel(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
