using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Gutenberg
{
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Link {  get; set; }
        public BitmapImage ImageSource {  get; set; }
    }
}
