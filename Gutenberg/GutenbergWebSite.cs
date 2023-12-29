using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gutenberg
{
    internal class GutenbergWebSite
    {
        //const string HamletURL = "https://www.gutenberg.org/cache/epub/2265/pg2265";
        //const string ImageBook = "https://www.gutenberg.org/cache/epub/46/pg46.cover.medium.jpg";

        const string TextBookUrl = "https://www.gutenberg.org/cache/epub/";
        const string Top100Book = "https://www.gutenberg.org/browse/scores/top";
        const string MainLogo = "https://www.gutenberg.org/gutenberg/pg-logo-129x80.png";

        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        // загрузка текста книги
        public async Task<string> LoadTextBookAsync(string idBook)
        {
            string url = string.Format("{0}{1}/pg{1}", TextBookUrl, idBook);
            WebRequest req = WebRequest.Create(url);
            req.Method = "GET";
            WebResponse rez = await req.GetResponseAsync();
            using (StreamReader sr = new StreamReader(rez.GetResponseStream(), Encoding.Default))
            {
                string textBook = await sr.ReadToEndAsync();
                return textBook;
            }
        }
        // загрузка топ-100 книг
        public async Task LoadPopularBookAsync()
        {
            var htmlWeb = new HtmlWeb();
            var htmlDocument = await htmlWeb.LoadFromWebAsync(Top100Book);
            var bookNodes = htmlDocument.DocumentNode.SelectSingleNode("//ol");
            foreach (var book in bookNodes.Descendants())
            {
                var title = book.InnerText;
                var id = book.GetAttributeValue("href", "").Split('/').LastOrDefault();
                if (id != "")
                {
                    Books.Add(new Book { Title = title, Link = id, ImageSource = LoadImage(id) });
                }
            }
        }
        private BitmapImage LoadImage(string id)
        {
            string uri = string.Format("{0}{1}/pg{1}.cover.medium.jpg", TextBookUrl, id);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(uri);
            image.EndInit();
            return image;
        }

    }
}
