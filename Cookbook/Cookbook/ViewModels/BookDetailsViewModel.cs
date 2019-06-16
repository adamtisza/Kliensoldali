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
    class BookDetailsViewModel: ViewModelBase
    {
        private Book book;

        public Book Book
        {
            get { return book; }
            set { book = value; }
        }

        public List<Item> Characters { get; set; }

        public List<Item> POVCharacters { get; set; }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var bookId = (int)parameter;

            var service = new DataService();
            this.Book = await service.GetBook(bookId);
            
            var cache = await CacheService.GetInstance();
            this.Characters = cache.GetCharacterNames(Book.characters);
            this.POVCharacters = cache.GetCharacterNames(Book.povCharacters);

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToCharacter(string url)
        {
            if (url.Length > 0)
            {
                NavigationService.Navigate(typeof(CharacterDetailsPage), int.Parse(url.Substring(url.LastIndexOf('/') + 1)));
            }
        }

        public void NavigateToMainPage()
        {
            NavigationService.Navigate(typeof(MainPage));
        }
    }
}
