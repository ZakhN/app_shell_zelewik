using app_shell_zelewik.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace app_shell_zelewik.Services
{
    class FireBaseDataStore : IDataStore<Item, Category>
    {
        List<Item> items;
        int databaseId;
        List<Category> categories;
        Random random = new Random();
        FirebaseClient firebase = new FirebaseClient("https://appshell-e0b06-default-rtdb.europe-west1.firebasedatabase.app/");

        public FireBaseDataStore()
        {
            items = new List<Item>();

            object databaseIdObject;
            if (App.Current.Properties.TryGetValue("databaseId", out databaseIdObject))
            {
                databaseId = (int)databaseIdObject;
                Debug.WriteLine("Found databaseId " + databaseId);
            }
            else
            {
                databaseId = random.Next();
                App.Current.Properties.Add("databaseId", databaseId);
                Debug.WriteLine("Create new databaseId " + databaseId);
            }

        }

        public async Task<bool> AddItemAsync(Item item)
        {
            //items.Add(item);
            await firebase
                    .Child("Items_" + databaseId)
                    .PostAsync(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var itemToUpdate = (await firebase.Child("Items_" + databaseId).OnceAsync<Item>()).Where(x => x.Object.Id == item.Id).FirstOrDefault();
            if (itemToUpdate != null)
            {
                await firebase.Child("Items_" + databaseId).Child(itemToUpdate.Key).PutAsync(item);
            }
            else
            {
                await AddItemAsync(item);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var itemToDelete = (await firebase.Child("Items_" + databaseId).OnceAsync<Item>()).Where(x => x.Object.Id == id).FirstOrDefault();
            if (itemToDelete != null)
            {
                await firebase.Child("Items_" + databaseId).Child(itemToDelete.Key).DeleteAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            items = (await firebase
                .Child("Items_" + databaseId)
                .OnceAsync<Item>()).Select(item => new Item
                {
                    Id = item.Object.Id,
                    Text = item.Object.Text,
                    Description = item.Object.Description,
                    Date = item.Object.Date,
                    Importance = item.Object.Importance,
                    Category = item.Object.Category
                }).ToList();
            Debug.WriteLine($"Items: {items}");

            return items;
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            //items.Add(item);
            await firebase.Child("Categories_" + databaseId).PostAsync(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var categoryToUpdate = (await firebase.Child("Categories_" + databaseId).OnceAsync<Category>()).Where(x => x.Object.Id == category.Id).FirstOrDefault();
            if (categoryToUpdate != null)
            {
                await firebase.Child("Categories_" + databaseId).Child(categoryToUpdate.Key).PutAsync(category);
            }
            else
            {
                await AddCategoryAsync(category);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var categoryToDelete = (await firebase.Child("Categories_" + databaseId).OnceAsync<Category>()).Where(x => x.Object.Id == id).FirstOrDefault();
            if (categoryToDelete != null)
            {
                await firebase.Child("Categories_" + databaseId).Child(categoryToDelete.Key).DeleteAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await Task.FromResult(categories.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Category> GetCategoryByTitleAsync(string title)
        {
            return await Task.FromResult(categories.FirstOrDefault(s => s.Title == title));
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool forseRefresh = false)
        {
            categories = (await firebase.Child("Categories_" + databaseId).OnceAsync<Category>()).Select(category => new Category
            {
                Id = category.Object.Id,
                Title = category.Object.Title,
                Description = category.Object.Description,
                SuccessRate = category.Object.SuccessRate
            }).ToList();
            return categories;
        }
    }
}
