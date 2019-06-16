using Cookbook.Models;
using System;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace Cookbook.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ResultItemClicked(object sender, ItemClickEventArgs e)
        {
            var item = (Item)e.ClickedItem;
            ViewModel.NavigateToDetails(item.ID);
        }

        private void KeyHandler(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if(e.Key == VirtualKey.Enter)
            {
                ViewModel.SearchCommand();
            }
        }
    }
}
