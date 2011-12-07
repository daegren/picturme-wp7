using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Text;
using System.IO;

namespace picturme_wp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        CameraCaptureTask camera;
        PhotoChooserTask librarySelect;

        BitmapImage takenImage;

        public MainPage()
        {
            InitializeComponent();
            camera = new CameraCaptureTask();
            camera.Completed += new EventHandler<PhotoResult>(camera_Completed);

            librarySelect = new PhotoChooserTask();
            librarySelect.Completed += new EventHandler<PhotoResult>(librarySelect_Completed);
        }

        void librarySelect_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                takenImage = new BitmapImage();
                takenImage.SetSource(e.ChosenPhoto);
            }
        }

        void camera_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                takenImage = new BitmapImage();
                takenImage.SetSource(e.ChosenPhoto);

                MemoryStream ms = new MemoryStream();
                e.ChosenPhoto.Position = 0;
                e.ChosenPhoto.CopyTo(ms);
                byte[] data = ms.ToArray();

                WebClient wc = new WebClient();
                wc.Headers[HttpRequestHeader.ContentType] = "multipart/form-data";
                wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted);
                wc.UploadStringAsync(new Uri("http://pictur.me/upload.ajax"), "POST", UTF8Encoding.UTF8.GetString(data, 0, data.Length), e.ChosenPhoto);
            }
        }

        void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void imageFromCamera_Click(object sender, RoutedEventArgs e)
        {
            camera.Show();
        }
    }
}