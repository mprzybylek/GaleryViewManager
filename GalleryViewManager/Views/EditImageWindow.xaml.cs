using GalleryViewManager.Core;
using System.ComponentModel;
using System.Windows;

namespace GalleryViewManager.Views
{
    public partial class EditImageWindow : Window
    {
        public EditImageWindow()
        {
            InitializeComponent();
            Locator.EditImageViewModel.Scale = 1;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
        private void imageOriginal_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Locator.EditImageViewModel.Scale++;
        }
    }
}
