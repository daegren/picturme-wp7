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
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

namespace picturme_wp7
{
    public partial class MosaicDetail : PhoneApplicationPage
    {
        public MosaicDetail()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            String uri = "";
            if (NavigationContext.QueryString.TryGetValue("uri", out uri))
            {
                WebClient wc = new WebClient();
                wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
                wc.OpenReadAsync(new Uri(uri));
            }
            
        }

        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                BitmapImage i = new BitmapImage();
                i.SetSource(e.Result);
                image1.Source = i;
            }
        }
    }
}