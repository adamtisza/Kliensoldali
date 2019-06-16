using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    class CacheService
    {
        private static List<Item> BookNames;
        private static List<Item> CharacterNames;
        private static List<Item> HouseNames;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private static CacheService _instance;

        private CacheService()
        {
        }

        public static async Task<CacheService> GetInstance()
        {
            await semaphore.WaitAsync();
            if (_instance == null)
            {
                _instance = new CacheService();
                await LoadData();
            }
            semaphore.Release();
            return _instance;
        }



        private static async Task<bool> LoadData()
        {
            var service = new DataService();

            BookNames = await service.GetBookNames();

            CharacterNames = await service.GetCharacterNames();

            HouseNames = await service.GetHouseNames();

            return true;
        }

        public List<Item> GetBookNames(string[] urls)
        {
            var bookNames = new List<Item>();

            foreach(var url in urls)
            {
                foreach(var book in BookNames)
                {
                    if(url.Equals(book.url))
                    {
                        bookNames.Add(book);
                    }
                }
            }
            return bookNames;
        }

        public List<Item> GetBookNames()
        {
            return BookNames;
        }

        public List<Item> GetCharacterNames(string[] urls)
        {
            var characterNames = new List<Item>();

            foreach (var url in urls)
            {
                foreach (var character in CharacterNames)
                {
                    if (character.url.Equals(url))
                    {
                        characterNames.Add(character);
                    }
                }
            }
            return characterNames;
        }

        public List<Item> GetCharacterNames()
        {
            return CharacterNames;
        }

        public List<Item> GetCharacterName(string url)
        {
            var characterNames = new List<Item>();

            foreach (var character in CharacterNames)
            {
                if (character.url.Equals(url))
                {
                    characterNames.Add(character);
                }
            }

            return characterNames;
        }

        public List<Item> GetHouseNames(string[] urls)
        {
            var houseNames = new List<Item>();

            foreach (var url in urls)
            {
                foreach (var house in HouseNames)
                {
                    if (url.Equals(house.url))
                    {
                        houseNames.Add(house);
                    }
                }
            }
            return houseNames;
        }

        public List<Item> GetHouseNames()
        {
            return HouseNames;
        }


    }
}
