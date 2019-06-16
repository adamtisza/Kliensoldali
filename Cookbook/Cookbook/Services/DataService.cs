using Cookbook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    class DataService
    {
        private readonly Uri serverUrl = new Uri("https://www.anapioficeandfire.com");

        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        public async Task<List<Book>> GetBooksAsync(int page = 1, int pagesize = 10)
        {
            return await GetAsync<List<Book>>(new Uri(serverUrl, $"/api/books?page={page}&pagesize={pagesize}"));
        }

        public async Task<List<Character>> GetCharactersAsync(int page = 1, int pagesize = 10)
        {
            return await GetAsync<List<Character>>(new Uri(serverUrl, $"/api/characters?page={page}&pagesize={pagesize}"));
        }

        public async Task<List<Character>> GetCharactersByNameAsync(string name, int page = 1, int pagesize = 10)
        {
            return await GetAsync<List<Character>>(new Uri(serverUrl, $"/api/characters?page={page}&pagesize={pagesize}&name={name}"));
        }

        public async Task<Book> GetBook(int id)
        {
            return await GetAsync<Book>(new Uri(serverUrl, $"/api/books/{id}"));
        }

        public async Task<Character> GetCharacter(int id)
        {
            return await GetAsync<Character>(new Uri(serverUrl, $"/api/characters/{id}"));
        }

        public async Task<House> GetHouse(int id)
        {
            return await GetAsync<House>(new Uri(serverUrl, $"/api/houses/{id}"));
        }

        public async Task<List<Item>> GetCharactersByNameContainingAsync(string name, int page = 1, int pagesize = 10)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            var list = new List<Item>();

            var cache = await CacheService.GetInstance();


                foreach (var character in cache.GetCharacterNames())
                {
                    if (character.name.Contains(name))
                    {
                        list.Add(character);
                    }
                }

            list.RemoveRange(0, (page - 1) * pagesize);
            if (list.Count > pagesize)
                list.RemoveRange(pagesize, list.Count - pagesize);

            return list;
        }

        public async Task<List<Item>> GetBooksByNameContainingAsync(string name, int page = 1, int pagesize = 10)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            var list = new List<Item>();

            var cache = await CacheService.GetInstance();


            foreach (var book in cache.GetBookNames())
            {
                if (book.name.Contains(name))
                {
                    list.Add(book);
                }
            }

            list.RemoveRange(0, (page - 1) * pagesize);
            if (list.Count > pagesize)
                list.RemoveRange(pagesize, list.Count - pagesize);

            return list;
        }

        public async Task<List<Item>> GetHousesByNameContainingAsync(string name, int page = 1, int pagesize = 10)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            var list = new List<Item>();

            var cache = await CacheService.GetInstance();


            foreach (var house in cache.GetHouseNames())
            {
                if (house.name.Contains(name))
                {
                    list.Add(house);
                }
            }

            list.RemoveRange(0, (page - 1) * pagesize);
            if (list.Count > pagesize)
                list.RemoveRange(pagesize, list.Count - pagesize);

            return list;
        }


        public async Task<List<Book>> GetBooksByNameAsync(string name, int page = 1, int pagesize = 10)
        {
            return await GetAsync<List<Book>>(new Uri(serverUrl, $"/api/books?page={page}&pagesize={pagesize}&name={name}"));
        }

        public async Task<List<House>> GetHousesByNameAsync(string name, int page = 1, int pagesize = 10)
        {
            return await GetAsync<List<House>>(new Uri(serverUrl, $"/api/houses?page={page}&pagesize={pagesize}&name={name}"));
        }

        public async Task<List<Character>> GetNamedCharactersAsync(int page = 1, int pagesize = 10)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            var list = new List<Character>();
            for (int i = 1; list.Count < pagesize * page; i++)
            {
                var tmp = await GetAsync<List<Character>>(new Uri(serverUrl, $"/api/characters?page={i}&pagesize=50"));
                foreach (var character in tmp)
                {
                    if (!character.name.Equals(""))
                    {
                        list.Add(character);
                    }
                }
            }

            list.RemoveRange(0, (page - 1) * pagesize);
            if (list.Count > pagesize)
                list.RemoveRange(pagesize, list.Count - pagesize);

            return list;
        }



        public async Task<List<House>> GetHousesAsync(int page = 1, int pagesize = 10)
        {
            return await GetAsync<List<House>>(new Uri(serverUrl, $"/api/houses?page={page}&pagesize={pagesize}"));
        }

        public async Task<List<Item>> GetBookNames()
        {
            var items = new List<Item>();

            var list = await GetAsync<List<Book>>(new Uri(serverUrl, $"/api/books?page=1&pagesize=50"));

            foreach(var book in list)
            {
                items.Add(new Item(book.url, book.name));
            }

            return items;
        }

        private static readonly int CharacterPages = 43;

        public async Task<List<Item>> GetCharacterNames()
        {
            var items = new List<Item>();

            for (int i = 1; i <= CharacterPages; i++)
            {
                var tmp = await GetAsync<List<Character>>(new Uri(serverUrl, $"/api/characters?page={i}&pagesize=50"));

                foreach(var character in tmp)
                {
                    items.Add(new Item(character.url, character.name, character.aliases[0]));
                }
            }

            return items;
        }

        private static readonly int HousePages = 9;

        public async Task<List<Item>> GetHouseNames()
        {
            var items = new List<Item>();

            for (int i = 1; i <= HousePages; i++)
            {
                var tmp = await GetAsync<List<Character>>(new Uri(serverUrl, $"/api/houses?page={i}&pagesize=50"));

                foreach (var character in tmp)
                {
                    items.Add(new Item(character.url, character.name));
                }
            }

            return items;
        }
    }
}
