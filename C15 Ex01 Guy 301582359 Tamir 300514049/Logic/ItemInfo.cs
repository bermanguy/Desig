using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public class ItemInfo
    {
        //public string Name { get;  set; }

        public User User { protected get; set; }

        public string GetOwnerName()
        {
            string ownerName = null;

            ownerName = User.Name;

            return ownerName;
        }

        public string GetOwnerImageUrl()
        {
            string userImageUrl = null;

            userImageUrl = User.PictureLargeURL;

            return userImageUrl;
        }

        public virtual object[] GetValues()
        {
            //List<object> values = new List<object>();
            //object[] objects = values.Cast<object>().ToArray();
            //mylist.Cast<object>().ToArray()
            return new object[] { User.Name };
        }
    }
}
