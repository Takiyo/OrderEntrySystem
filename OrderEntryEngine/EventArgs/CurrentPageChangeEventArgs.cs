using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class CurrentPageChangeEventArgs
    {
        public CurrentPageChangeEventArgs(int startIndex, int itemCount)
        {

        }

        public int StartIndex { get; private set; }

        public int ItemCount { get; private set; }
    }
}
