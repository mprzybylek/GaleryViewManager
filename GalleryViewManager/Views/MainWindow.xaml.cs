using GalleryViewManager.Core.Model;
using GalleryViewManager.Core.Services;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GalleryViewManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GaleryView _galeryView = new GaleryView();
        private SavedImagesView _favouriteView = new SavedImagesView();
        public MainWindow()
        {
            InitializeComponent();
            this.ContentView.Children.Add(_galeryView);
        }
        private void PackIcon_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ListViewMenu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dynamic item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                ContentView.Children.Clear();
                switch (item.Name)
                {
                    case "Galery":
                        ContentView.Children.Add(_galeryView);
                        break;
                    case "Favourite":
                        ContentView.Children.Add(_favouriteView);
                        break;
                }
            }
        }
    }
}
