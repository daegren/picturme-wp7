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
using System.ComponentModel;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace picturme_wp7
{
    public class MosaicViewModel : INotifyPropertyChanged
    {
        private Stream _mosaicData;
        public Stream MosaicData
        {
            get
            {
                _mosaicData.Position = 0;
                return _mosaicData;
            }
            set
            {
                if (value != _mosaicData)
                {
                    _mosaicData = value;
                    NotifyPropertyChanged("MosaicData");
                }
            }
        }

        private Stream _originalImageData;
        public Stream OriginalImageData
        {
            get
            {
                _originalImageData.Position = 0;
                return _originalImageData;
            }
            set
            {
                if (value != _originalImageData)
                {
                    _originalImageData = value;
                    if (null == _originalImage)
                    {
                        _originalImage = new BitmapImage();
                    }
                    _originalImage.SetSource(_originalImageData);
                    NotifyPropertyChanged("OriginalImageData");
                }
            }
        }

        private BitmapImage _originalImage;
        public BitmapImage OriginalImage
        {
            get { return _originalImage; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
