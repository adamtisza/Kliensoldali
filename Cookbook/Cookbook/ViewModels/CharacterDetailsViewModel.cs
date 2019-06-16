using Cookbook.Models;
using Cookbook.Services;
using Cookbook.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Cookbook.ViewModels
{
    class CharacterDetailsViewModel: ViewModelBase
    {
        private Character _character;

        public Character Character
        {
            get { return _character; }
            set { _character = value; }
        }

        public List<Item> Mother { get; set; }

        public List<Item> Father { get; set; }

        public List<Item> Spouse { get; set; }

        public List<Item> Allegiances { get; set; }

        public List<Item> Books { get; set; }

        public List<Item> POVBooks { get; set; }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var characterId = (int)parameter;

            var service = new DataService();
            this.Character = await service.GetCharacter(characterId);

            var cache = await CacheService.GetInstance();

            Mother = cache.GetCharacterName(Character.mother);

            Father = cache.GetCharacterName(Character.father);

            Spouse = cache.GetCharacterName(Character.spouse);

            Allegiances = cache.GetHouseNames(Character.allegiances);

            Books = cache.GetBookNames(Character.books);

            POVBooks = cache.GetBookNames(Character.povBooks);

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToCharacter(string url)
        {
            if (url.Length > 0)
            {
                NavigationService.Navigate(typeof(CharacterDetailsPage), int.Parse(url.Substring(url.LastIndexOf('/') + 1)));
            }
        }

        public void NavigateToHouse(string url)
        {
            if (url.Length > 0)
            {
                NavigationService.Navigate(typeof(HouseDetailsPage), int.Parse(url.Substring(url.LastIndexOf('/') + 1)));
            }
        }

        public void NavigateToBook(string url)
        {
            if (url.Length > 0)
            {
                NavigationService.Navigate(typeof(BookDetailsPage), int.Parse(url.Substring(url.LastIndexOf('/') + 1)));
            }
        }

        public void NavigateToMainPage()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

    }
}
