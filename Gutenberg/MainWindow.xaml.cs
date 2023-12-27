using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        private void LoadText_Click(object sender, RoutedEventArgs e)
        {
            GutenbergWebSite.LoadPopularBook();
        }       
        

        private void PopularBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GutenbergWebSite.LoadTextBook((PopularBook.SelectedValue as Book).Link);
        }
        
    }
}
