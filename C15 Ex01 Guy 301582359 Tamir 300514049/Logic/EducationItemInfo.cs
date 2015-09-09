using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public class EducationItemInfo : ItemInfo
    {
        public Education Education { private get; set; }
        public string GetSchoolName()
        {
            string schoolName = null;

            schoolName = Education.School.Name;

            return schoolName;
        }
    }
}
