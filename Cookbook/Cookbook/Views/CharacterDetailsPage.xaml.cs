using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Cookbook.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterDetailsPage : Page
    {
        public CharacterDetailsPage()
        {
            this.InitializeComponent();
        }

        private void HouseClicked(object sender, ItemClickEventArgs e)
        {
            ViewModel.NavigateToHouse(((Item)e.ClickedItem).url);
        }

        private void CharacterClicked(object sender, ItemClickEventArgs e)
        {
            ViewModel.NavigateToCharacter(((Item)e.ClickedItem).url);
        }

        private void BookClicked(object sender, ItemClickEventArgs e)
        {
            ViewModel.NavigateToBook(((Item)e.ClickedItem).url);
        }
    }
}
