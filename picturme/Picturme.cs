using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace picturme_wp7
{
    public class Picturme
    {
        public void UploadImage()
        {

        }

        public event UploadImageCompleteEventHandler UploadImageCompleted;
        private void NotifyUploadComplete(PicturmeResponseData data)
        {
            UploadImageCompleteEventHandler handler = UploadImageCompleted;
            if (null != handler)
            {
                handler(this, new UploadImageCompleteEventArgs(data));
            }
        }
    }

    public delegate void UploadImageCompleteEventHandler(object sender, UploadImageCompleteEventArgs e);
}
