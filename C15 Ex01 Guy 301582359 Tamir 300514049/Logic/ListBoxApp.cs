using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public class ListBoxApp : IUIGroupComponent
    {
        public ListBox ListBoxOriginal { set; get; }

        public int Add(params object[] values)
        {
            int result;

            result = ListBoxOriginal.Items.Add(values[0]);

            return result;
        }

        public void Clear()
        {
            ListBoxOriginal.Items.Clear();
        }
    }
}
