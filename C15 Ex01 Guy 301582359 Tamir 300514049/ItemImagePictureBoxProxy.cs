using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace C15_Ex01_Guy_301582359_Tamir_300514049
{
    // Proxy class for picture box.
    // Before loading async picture - check if the picture already in cache.
    class ItemImagePictureBoxProxy : IAsyncPictureBoxLoader
    {
        public PictureBox ItemImagePictureBox { get; set; }
        public Dictionary<string, MemoryStream> PicturesCacheDictionary { get; set; }
        public WebClient m_WebClient { get; set; }

        public ItemImagePictureBoxProxy(PictureBox i_PictureBox)
        {
            ItemImagePictureBox = i_PictureBox;
            PicturesCacheDictionary = new Dictionary<string, MemoryStream>();
            m_WebClient = new WebClient();
        }
        public void LoadAsync(string i_Url)
        {
            if (PicturesCacheDictionary.ContainsKey(i_Url))
            {
                MemoryStream ms = PicturesCacheDictionary[i_Url];
                Image img = Image.FromStream(ms);
                ItemImagePictureBox.Image = img;
            }
            else
            {
                m_WebClient.Proxy = null;
                byte[] bFile = m_WebClient.DownloadData(i_Url);
                MemoryStream ms = new MemoryStream(bFile);
                Image img = Image.FromStream(ms);
                ItemImagePictureBox.Image = img;

                ItemImagePictureBox.LoadAsync(i_Url);
                PicturesCacheDictionary.Add(i_Url, ms);
            }
        }
    }
}
