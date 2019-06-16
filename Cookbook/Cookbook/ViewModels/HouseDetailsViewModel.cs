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
    class HouseDetailsViewModel: ViewModelBase
    {
        private House _house;

        public House House
        {
            get { return _house; }
            set { _house = value; }
        }

        public List<Item> Heir { get; set; }

        public List<Item> CurrentLord { get; set; }

        public List<Item> Founder { get; set; }

        public List<Item> Overlord { get; set; }

        public List<Item> CadetBranches { get; set; }

        public List<Item> SwornMembers { get; set; }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var houseId = (int)parameter;

            var service = new DataService();
            this.House = await service.GetHouse(houseId);

            var cache = await CacheService.GetInstance();

            Heir = cache.GetCharacterName(House.heir);

            CurrentLord = cache.GetCharacterName(House.currentLord);

            Founder = cache.GetCharacterName(House.founder);

            Overlord = cache.GetCharacterName(House.overlord);

            CadetBranches = cache.GetHouseNames(House.cadetBranches);

            SwornMembers = cache.GetCharacterNames(House.swornMembers);

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

        public void NavigateToMainPage()
        {
            NavigationService.Navigate(typeof(MainPage));
        }
    }
}
