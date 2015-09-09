using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public class CommonGroup
    {
        private readonly List<ItemInfo> r_Items = new List<ItemInfo>();

        public List<ItemInfo> Items
        {
            get { return r_Items; }
        }
    }
}
