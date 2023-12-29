using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HtmlAgilityPack;

namespace Gutenberg
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GutenbergWebSite GutenbergWebSite;
        public MainWindow()
        {
            InitializeComponent();
            GutenbergWebSite = new GutenbergWebSite();
            PopularBook.ItemsSource = GutenbergWebSite.Books;
            
        }

        private async void LoadText_Click(object sender, RoutedEventArgs e)
        {
            GutenbergWebSite.Books.Clear();
            await GutenbergWebSite.LoadPopularBookAsync();
        }               
        private async void PopularBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBook.Text = await GutenbergWebSite.LoadTextBookAsync((PopularBook.SelectedValue as Book).Link);
        }

        private void SaveBookButton_Click(object sender, RoutedEventArgs e)
        {
            Book book = PopularBook.SelectedValue as Book;
            using (StreamWriter writer = new StreamWriter(book.Title + ".txt"))
            {
                writer.Write(TextBook.Text);
            }
        }
    }
}
