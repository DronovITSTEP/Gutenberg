using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Gutenberg
{
    internal class GutenbergWebSite
    {
        //const string HamletURL = "https://www.gutenberg.org/cache/epub/2265/pg2265";
        //const string ImageBook = "https://www.gutenberg.org/cache/epub/46/pg46.cover.medium.jpg";

        const string TextBookUrl = "https://www.gutenberg.org/cache/epub/";
        const string Top100Book = "https://www.gutenberg.org/browse/scores/top";
        const string MainLogo = "https://www.gutenberg.org/gutenberg/pg-logo-129x80.png";

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        // загрузка текста книги
        public string LoadTextBook(string idBook)
        {
            string url = String.Format("{0}{1}/pg{1}", TextBookUrl, idBook);
            WebRequest req = WebRequest.Create(url);
            req.Method = "GET";
            WebResponse rez = req.GetResponse();
            using (StreamReader sr = new StreamReader(rez.GetResponseStream(), Encoding.Default))
            {
                string textBook = sr.ReadToEnd();
                return textBook;
            }
        }
        // загрузка топ-100 книг
        public void LoadPopularBook()
        {
            var htmlWeb = new HtmlWeb();
            var htmlDocument = htmlWeb.Load(Top100Book);
            var bookNodes = htmlDocument.DocumentNode.SelectSingleNode("//ol");
            foreach (var book in bookNodes.Descendants())
            {
                var title = book.InnerText;
                var id = book.GetAttributeValue("href", "").Split('/').LastOrDefault();
                if (id != "")
                {
                    BitmapImage bitmap = LoadImageBook(id);
                    Books.Add(new Book { Title = title, Link = id, ImageSource = bitmap });
                }
            }
        }
        // загрузка обложки книги
        private BitmapImage LoadImageBook(string idBook)
        {
            string uri = String.Format("{0}{1}/pg{1}.cover.medium.jpg", TextBookUrl, idBook);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(uri);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
