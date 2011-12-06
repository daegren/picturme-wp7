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
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Json;

namespace picturme_wp7
{
    public partial class MainPage : PhoneApplicationPage
    {

        CameraCaptureTask camera;
        Picture p;
        BitmapImage i;
        Stream s;

        private static String host = "http://10.108.120.73:8181/";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;

            camera = new CameraCaptureTask();
            camera.Completed += new EventHandler<PhotoResult>(camera_Completed);

            i = new BitmapImage();

            image1.Source = i;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            camera.Show();
        }

        void camera_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                i = new BitmapImage();
                s = e.ChosenPhoto;
                i.SetSource(s);
                image1.Source = i;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // send code to pictur.me server


            WebRequest wr = WebRequest.Create(host + "v1/upload");
            wr.Method = "POST";
            wr.ContentType = "application/x-www-form-urlencoded";

            RequestState rs = new RequestState()
            {
                webRequest = wr,
                data = s
            };

            wr.BeginGetRequestStream(new AsyncCallback(SendData), rs);

        }

        private void SendData(IAsyncResult asyncResult)
        {
            RequestState rs = (RequestState)asyncResult.AsyncState;

            rs.data.Position = 0; // rest position to read file in its entirety
            MemoryStream ms = new MemoryStream();
            rs.data.CopyTo(ms);
            byte[] c = ms.ToArray();

            Stream postStream = rs.webRequest.EndGetRequestStream(asyncResult);

            postStream.Write(c, 0, c.Length);
            postStream.Close();

            rs.webRequest.BeginGetResponse(new AsyncCallback(RecieveImage), rs);
        }

        private void RecieveImage(IAsyncResult asyncResult)
        {

            RequestState rs = (RequestState)asyncResult.AsyncState;
            WebResponse wr;

            wr = (WebResponse)rs.webRequest.EndGetResponse(asyncResult);
            Stream rStream = wr.GetResponseStream();
            StreamReader sr = new StreamReader(rStream);
            String res = sr.ReadToEnd();
            rStream.Close();
            sr.Close();
            wr.Close();

            DisplayImage(res);

        }

        private void DisplayImage(string res)
        {
            JsonValue jv = JsonArray.Parse(res);
            if (bool.Parse(jv["success"]))
            {
                NavigationService.Navigate(new Uri("/MosaicDetail.xaml?url=" + host + jv["image"]));
            }
        }
    }

    public class RequestState
    {
        public WebRequest webRequest;
        public Stream data;
    }
}