using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public class LocationItemInfo : ItemInfo
    {
        public PostedItem Item { private get; set; }

        public DateTime GetCreatedDate()
        {
            DateTime createdTime;

            createdTime = ((DateTime)Item.CreatedTime).Date;

            return createdTime;
        }

        public string GetItemImageUrl()
        {
            string itemImageUrl = null;

            if (Item is Album)
            {
                itemImageUrl = (Item as Album).PictureAlbumURL;
            }

            return itemImageUrl;
        }

        public string GetItemName()
        {
            string itemName = null;

            if (Item is Album)
            {
                itemName = (Item as Album).Name;
            }
            else if(Item is Checkin)
            {
                itemName = (Item as Checkin).Place.Name;
            }

            return itemName;
        }
        public override object[] GetValues()
        {
            object[] baseValues = base.GetValues();
            List<object> values = new List<object>();

            values.Add(GetItemName());
            
            foreach (object obj in baseValues)
            {
                values.Add(obj);
            }
            values.Add(GetCreatedDate());
            values.Add(GetItemImageUrl().StringExistance());

            
            //object[] objects = values.Cast<object>().ToArray();
            //mylist.Cast<object>().ToArray()
            return values.ToArray();
        }
    }
}
