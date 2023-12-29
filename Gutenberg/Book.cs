using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Gutenberg
{
    internal class Book : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Link {  get; set; }
        private BitmapImage _image;
        public BitmapImage ImageSource 
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
