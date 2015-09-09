using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public delegate int AddItemAction<T>(T i_Object);
    public delegate int AddItemAction(params object[] values);
    public delegate void onLoginSuccess(string i_LoginErrorMessage);

    public class LogicManager
    {
        private User m_LoggedInUser;
        public onLoginSuccess m_onLoginSuccess;
        public LoggedUserWrapper LoggedUser { get; private set; }

        public CommonGroup m_LocationCommonGroup { get; set; }

        public LogicManager()
        {
        }

        //public void LoginAndInit(out string o_LoginErrorMessage)
        public void LoginAndInit()
        {
            //bool loginSuccess;
            string loginErrorMessage = null;

            LoginResult result = FacebookService.Login(
                "1084476921567330", "user_about_me", "user_friends", "publish_actions", "user_events", "user_posts", "user_photos", "user_status", "user_tagged_places", "user_education_history");

            //o_LoginErrorMessage = result.ErrorMessage;
            loginErrorMessage = result.ErrorMessage;

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                //loginSuccess = true;
                m_LoggedInUser = result.LoggedInUser;

                new System.Threading.Thread(() =>
                {
                    LoggedUser = new LoggedUserWrapper(m_LoggedInUser);

                    if (m_onLoginSuccess != null)
                    {
                        m_onLoginSuccess.Invoke(loginErrorMessage);
                    }
                }).Start();
            }
            //else
            //{
            //    loginSuccess = false;
            //}

            //return loginSuccess;
        }

        public void FetchInfo<T>(AddItemAction<object> i_AddItem, ICollection<T> i_Collection)
        {
            if (m_LoggedInUser != null)
            {
                foreach (var item in i_Collection)
                {
                    i_AddItem.Invoke(item);
                }
            }
        }

        public void fetchInfo(AddItemAction i_AddItem, ICollection<ItemInfo> i_Collection)
        {
            if (m_LoggedInUser != null)
            {
                foreach (ItemInfo item in i_Collection)
                {
                    object[] values = item.GetValues();
                    i_AddItem.Invoke(values);
                }
            }
        }

        public List<LocationItemInfo> FetchLocations(string[] i_Location)
        {
            List<LocationItemInfo> locationList = new List<LocationItemInfo>();
            m_LocationCommonGroup = new CommonGroup();

            foreach (User user in m_LoggedInUser.Friends)
            {
                foreach (Checkin checkin in user.Checkins)
                //User user = m_LoggedInUser;
                //foreach(Checkin checkin in m_LoggedInUser.Checkins)
                {
                    if (checkIfCheckinRelevant(i_Location, checkin))
                    {
                        LocationItemInfo itemInfo = new LocationItemInfo { User = user, Item = checkin };
                        //LocationItemInfo itemInfo = new LocationItemInfo { CreatedTime = (DateTime)checkin.CreatedTime, User = user.Name, Name = checkin.Place.Name, UserImageUrl = user.PictureNormalURL };

                        locationList.Add(itemInfo);
                        m_LocationCommonGroup.Items.Add(itemInfo);
                    }
                }

                foreach (Album album in user.Albums)
                //foreach(Album album in m_LoggedInUser.Albums)
                {
                    if (isRelevant(i_Location, album.Name.ToUpper().Split()))
                    {
                        LocationItemInfo itemInfo = new LocationItemInfo { User = user, Item = album };
                        //LocationItemInfo itemInfo = new LocationItemInfo { CreatedTime = (DateTime)album.CreatedTime, User = user.Name, Name = album.Name, UserImageUrl = user.PictureNormalURL, ItemImageUrl = album.PictureAlbumURL };

                        locationList.Add(itemInfo);
                        m_LocationCommonGroup.Items.Add(itemInfo);
                    }
                }
            }

            return locationList;
        }

        private bool checkIfCheckinRelevant(string[] i_Location, Checkin i_Checkin)
        {
            string[] inputCheckinString = i_Checkin.Place.Name.ToUpper().Split();

            return isRelevant(i_Location, inputCheckinString);
        }

        private bool isRelevantEducation(string[] i_SplittesWords, string educationName)
        {
            bool isRelevant = false;
            foreach (string splittedWord in i_SplittesWords)
            {
                if (educationName.Contains(splittedWord))
                {
                    isRelevant = true;
                    break;
                }
            }

            return isRelevant;
        }

        private bool isRelevant(string[] i_strings, string[] inputStrings)
        {
            bool isRelevant = false;

            foreach (string splittedWord in inputStrings)
            {
                foreach (string str in i_strings)
                {
                    if (str.Equals(splittedWord))
                    {
                        isRelevant = true;
                        break;
                    }
                }
            }

            return isRelevant;
        }

        public void PostStatus(string i_Status)
        {
            m_LoggedInUser.PostStatus(i_Status);
        }

        public List<LocationItemInfo> FetchLocationsByDate(string[] i_Location, System.Windows.Forms.DateTimePicker i_DateFrom, System.Windows.Forms.DateTimePicker i_DateTo)
        {
            List<LocationItemInfo> locationList = FetchLocations(i_Location);
            List<LocationItemInfo> filteredLocationList = new List<LocationItemInfo>();

            foreach (LocationItemInfo location in locationList)
            {
                if (isItemDateInRange(i_DateFrom, i_DateTo, location.GetCreatedDate()))
                {
                    filteredLocationList.Add(location);
                }
            }

            return filteredLocationList;
        }

        private bool isItemDateInRange(System.Windows.Forms.DateTimePicker i_InputDateFrom, System.Windows.Forms.DateTimePicker i_InputDateTo, DateTime i_ItemDate)
        {
            bool isDateInRange = i_ItemDate >= i_InputDateFrom.Value.Date && i_ItemDate.Date <= i_InputDateTo.Value.Date;

            return isDateInRange;
        }

        public List<EducationItemInfo> FetchEducation(string[] i_EducationInput)
        {
            List<EducationItemInfo> educationList = new List<EducationItemInfo>();

            foreach (User user in m_LoggedInUser.Friends)
            {
                foreach (Education education in user.Educations)
                {
                    if (isRelevantEducation(i_EducationInput, education.School.Name.ToUpper()))
                    {
                        educationList.Add(new EducationItemInfo { Education = education, User = user });
                        //educationList.Add(new EducationItemInfo { SchoolName = education.School.Name, User = user.Name });
                    }
                }
            }

            return educationList;
        }

        //public List<ItemInfo> FetchEvents()
        //{
        //    List<ItemInfo> eventsList = new List<ItemInfo>();

        //    foreach (Event userEvent in m_LoggedInUser.EventsNotYetReplied)
        //    {
        //        eventsList.Add(new ItemInfo { Name = userEvent.Name });
        //    }

        //    return eventsList;
        //}
    }
}