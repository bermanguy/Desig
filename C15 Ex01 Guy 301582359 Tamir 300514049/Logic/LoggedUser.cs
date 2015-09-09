using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15_Ex01_Guy_301582359_Tamir_300514049.Logic
{
    public class LoggedUserWrapper
    {
        private User m_User;
        public FacebookObjectCollection<Event> UpcomingEvents { private set; get; }
        public FacebookObjectCollection<User> Friends { private set; get; }

        public LoggedUserWrapper(User i_LoggedUser)
        {
            m_User = i_LoggedUser;

            initUpcomingEvents();
            initFriends();
        }

        private void initFriends()
        {
            Friends = new FacebookObjectCollection<User>();

            foreach (User user in m_User.Friends)
            {
                Friends.Add(user);
            }
        }

        public string GetPictureLargeUrl()
        {
            string pictureLargeUrl = m_User.PictureLargeURL;

            return pictureLargeUrl;
        }

        public Cover GetCover()
        {
            Cover cover = m_User.Cover;

            return cover;
        }

        private void initUpcomingEvents()
        {
            UpcomingEvents = new FacebookObjectCollection<Event>();

            foreach (Event eve in m_User.EventsNotYetReplied)
            {
                //if (eve.EndTime > DateTime.Now)
                {
                    UpcomingEvents.Add(eve);
                }
            }
        }
    }
}
