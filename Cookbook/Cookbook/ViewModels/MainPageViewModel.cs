using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Cookbook.Models;
using Cookbook.Services;
using Cookbook.Views;

namespace Cookbook.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<Item> Results { get; set; } = new ObservableCollection<Item>();

        public string SearchPhrase { get; set; } = "";

        public int SelectedType { get; set; } = 0;

        private int _page;

        public int Page
        {
            get { return _page; }
            set { _page = value;
                RaisePropertyChanged("Page");
            }
        }


        public int PageSizeIndex { get; set; } = 0;

        private int PageSize { get; set; } = 10;

        private bool Searched { get; set; } = false;

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var searchPhrase = (string)parameter;
            if(searchPhrase != null && !searchPhrase.Equals(""))
            {
                this.SearchPhrase = searchPhrase;
                this.SearchCommand();
            }
            await CacheService.GetInstance();

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void LoadData()
        {
            if (this.Searched)
                this.SearchContaining();
            else
                this.LoadWithoutSearch();
        }

        private async void LoadWithoutSearch()
        {
            var service = new DataService();

            switch (SelectedType)
            {
                case 0:
                    var books = await service.GetBooksAsync(Page, PageSize);
                    Results.Clear();
                    foreach (var book in books)
                    {
                        Results.Add(book);
                    }
                    break;
                case 1:
                    var characters = await service.GetNamedCharactersAsync(Page, PageSize);
                    Results.Clear();
                    foreach (var character in characters)
                    {
                        Results.Add(character);
                    }
                    break;
                case 2:
                    var houses = await service.GetHousesAsync(Page, PageSize);
                    Results.Clear();
                    foreach (var house in houses)
                    {
                        Results.Add(house);
                    }
                    break;
                default:
                    break;
            }
        }

        public async void Search()
        {
            var service = new DataService();

            switch (SelectedType)
            {
                case 0:
                    var books = await service.GetBooksByNameAsync(this.SearchPhrase, Page, PageSize);
                    Results.Clear();
                    foreach (var book in books)
                    {
                        Results.Add(book);
                    }
                    break;
                case 1:
                    var characters = await service.GetCharactersByNameAsync(this.SearchPhrase, Page, PageSize);
                    Results.Clear();
                    foreach (var character in characters)
                    {
                        Results.Add(character);
                    }
                    break;
                case 2:
                    var houses = await service.GetHousesByNameAsync(this.SearchPhrase, Page, PageSize);
                    Results.Clear();
                    foreach (var house in houses)
                    {
                        Results.Add(house);
                    }
                    break;
                default:
                    break;
            }
        }

        public async void SearchContaining()
        {
            var service = new DataService();

            switch (SelectedType)
            {
                case 0:
                    var books = await service.GetBooksByNameContainingAsync(this.SearchPhrase, Page, PageSize);
                    Results.Clear();
                    foreach (var book in books)
                    {
                        Results.Add(book);
                    }
                    break;
                case 1:
                    var characters = await service.GetCharactersByNameContainingAsync(this.SearchPhrase, Page, PageSize);
                    Results.Clear();
                    foreach (var character in characters)
                    {
                        Results.Add(character);
                    }
                    break;
                case 2:
                    var houses = await service.GetHousesByNameContainingAsync(this.SearchPhrase, Page, PageSize);
                    Results.Clear();
                    foreach (var house in houses)
                    {
                        Results.Add(house);
                    }
                    break;
                default:
                    break;
            }
        }

        public void NavigateToDetails(int Id)
        {
            Type type = typeof(BookDetailsPage);
            switch (this.SelectedType)
            {
                case 0: type = typeof(BookDetailsPage);
                        break;
                case 1:
                    type = typeof(CharacterDetailsPage);
                    break;
                case 2:
                   type = typeof(HouseDetailsPage);
                    break;
                default:
                    break;
            }

            NavigationService.Navigate(type, Id);
        }

        public void PageSizeChanged()
        {
            Page = 1;
            switch (this.PageSizeIndex)
            {
                case 0:
                    this.PageSize = 10;
                    break;
                case 1:
                    this.PageSize = 20;
                    break;
                case 2:
                    this.PageSize = 50;
                    break;
                default:
                    break;
            }
            this.LoadData();
        }

        public void TypeChanged()
        {
            this.SearchPhrase = "";
            this.Page = 1;
            this.Searched = false;
            this.LoadData();
        }

        public void SearchCommand()
        {
            this.Page = 1;
            this.Searched = true;
            this.LoadData();
        }

        public void NextPage()
        {
            if(this.Results.Count >= this.PageSize)
                this.Page += 1;
            this.LoadData();
        }

        public void PreviousPage()
        {
            if (this.Page >= 2)
                this.Page -= 1;
            this.LoadData();
        }

        public void FirstPage()
        {
            this.Page = 1;
            this.LoadData();
        }
    }
}

