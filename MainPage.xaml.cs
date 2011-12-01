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
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Imaging;

namespace picturme_wp7
{
    public partial class MainPage : PhoneApplicationPage
    {

        CameraCaptureTask camera;
        Picture p;
        BitmapImage i;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            camera = new CameraCaptureTask();
            camera.Completed += new EventHandler<PhotoResult>(camera_Completed);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            camera.Show();
            Console.Out.Write("test: this should show after the camera is finished?");
        }

        void camera_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                i = new BitmapImage();
                i.SetSource(e.ChosenPhoto);
                image1.Source = i;
            }
        }
    }
}