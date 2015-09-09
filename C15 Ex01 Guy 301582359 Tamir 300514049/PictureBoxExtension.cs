using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C15_Ex01_Guy_301582359_Tamir_300514049
{
    public static class PictureBoxExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // Extention ctor that get parameters
        public static PictureBox WordCount(this PictureBox pb)
        {
            return new PictureBox()
            {
                Height = 20
            };
        }
    }
}
