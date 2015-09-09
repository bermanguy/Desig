using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using C15_Ex01_Guy_301582359_Tamir_300514049.Logic;

namespace C15_Ex01_Guy_301582359_Tamir_300514049
{

    public partial class FormFacebook : Form
    {
        private const string k_DisplayMembersName = "Name";
        private static readonly DateTime sr_MinDateSearch = new DateTime(1950, 1, 1);
        private LogicManager m_LogicManager;
        private readonly Thread sr_StartAfterLoginProcessThread;

        public FormFacebook()
        {
            FacebookWrapper.FacebookService.s_CollectionLimit = 1000;
            InitializeComponent();
            m_LogicManager = new LogicManager();
            dateTimePickerTo.MaxDate = DateTime.Today;
            dateTimePickerFrom.MaxDate = DateTime.Today;
            dateTimePickerFrom.MinDate = sr_MinDateSearch;
            sr_StartAfterLoginProcessThread = new Thread(startAfterLoginProcess);
            m_LogicManager.m_onLoginSuccess += onLoginSuccess;
            //tabsController.Enabled = true;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            //bool loginSuccess;
            //string errorMessage;

            //loginSuccess = m_LogicManager.LoginAndInit(out errorMessage);
            m_LogicManager.LoginAndInit();

            //if (loginSuccess)
            //{
            //    tabsController.Enabled = true;
            //    sr_StartAfterLoginProcessThread.Start();
            //}
        }

        private void onLoginSuccess(string i_LoginErrorMessage)
        {
            tabsController.Invoke(new Action( () => tabsController.Enabled = true));
            //tabsController.Enabled = true;
            sr_StartAfterLoginProcessThread.Start();
        }

        private void startAfterLoginProcess()
        {
            profilePic_PictureBox.LoadAsync(m_LogicManager.LoggedUser.GetPictureLargeUrl());

            if (m_LogicManager.LoggedUser.GetCover() != null)
            {
                coverPhotoPictureBox.LoadAsync(m_LogicManager.LoggedUser.GetCover().SourceURL);
            }

            fetchUserInfo();
        }

        private void fetchUserInfo()
        {
            friendsListBox.Invoke(new Action(() => fetchFriends()));
            EventsListBox.Invoke(new Action(() => fetchEvents()));
            //EventsListBox.Invoke(new Action(
            //    () =>
            //    {
            //        foreach (FacebookWrapper.ObjectModel.Event eve in m_LogicManager.LoggedUser.GetEvents())
            //        {
            //            EventsListBox.Items.Add(eve.Name);
            //        }
            //    }
            //    ));
        }

        private void fetchFriends()
        {
            friendsListBox.Items.Clear();
            friendsListBox.DisplayMember = k_DisplayMembersName;
            m_LogicManager.FetchInfo(friendsListBox.Items.Add, m_LogicManager.LoggedUser.Friends);
        }

        private void fetchEvents()
        {
            //List<ItemInfo> events = m_LogicManager.FetchEvents();

            EventsListBox.Items.Clear();
            EventsListBox.DisplayMember = k_DisplayMembersName;

            //foreach (ItemInfo item in events)
            //{
            //    EventsListBox.Items.Add(item.Name);
            //}
            //FacebookWrapper.ObjectModel.FacebookObjectCollection<FacebookWrapper.ObjectModel.Event> events = m_LogicManager.LoggedUser.GetUpcomingEvents();
            m_LogicManager.FetchInfo(EventsListBox.Items.Add, m_LogicManager.LoggedUser.UpcomingEvents);
        }

        private void locationSearchButton_Click(object sender, EventArgs e)
        {
            string[] locationInput = userLocationTextBox.Text.ToUpper().Split();
            List<LocationItemInfo> locationsList = searchByDateCheckBox.Checked ? m_LogicManager.FetchLocationsByDate(locationInput, dateTimePickerFrom, dateTimePickerTo) :
                m_LogicManager.FetchLocations(locationInput);

            locationTable.Rows.Clear();

            foreach (LocationItemInfo location in locationsList)
            {
                this.locationTable.Rows.Add(location.GetItemName(), location.GetOwnerName(), location.GetCreatedDate(), location.GetItemImageUrl().StringExistance());
                locationTable.ClearSelection();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void educationSearchButton_Click(object sender, EventArgs e)
        {
            string[] educationInput = academicTextBox.Text.ToUpper().Split();
            List<EducationItemInfo> educationList = m_LogicManager.FetchEducation(educationInput);

            educationTable.Rows.Clear();

            foreach (EducationItemInfo item in educationList)
            {
                this.educationTable.Rows.Add(item.GetOwnerName(), item.GetSchoolName());
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void searchByDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dateSearchPanel.Enabled = !dateSearchPanel.Enabled;
        }

        private void postStatusButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(statusTextBox.Text))
            {
                MessageBox.Show("Please write something...");
            }
            else
            {
                m_LogicManager.PostStatus(statusTextBox.Text);
                MessageBox.Show("Your status has been published");
            }
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            if (sender is DateTimePicker)
            {
                dateTimePickerTo.MinDate = (sender as DateTimePicker).Value;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            userLocationTextBox.Clear();
            dateSearchPanel.Enabled = false;
            locationTable.Rows.Clear();
            searchByDateCheckBox.Checked = false;
            itemImagePictureBox.Image = null;
        }

        private void locationTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (locationTable.SelectedRows.Count == 1)
            {
                LocationItemInfo locationItemInfo = (LocationItemInfo)m_LogicManager.m_LocationCommonGroup.Items.ElementAt(locationTable.CurrentCell.RowIndex);

                displayItemPictures(locationItemInfo.GetItemImageUrl());
            }
        }

        private void displayItemPictures(string i_ItemImageUrl)
        {
            if (i_ItemImageUrl != null)
            {
                itemImagePictureBox.LoadAsync(i_ItemImageUrl);
            }
            else
            {
                itemImagePictureBox.Image = null;
            }
        }
    }
}
