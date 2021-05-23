using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_shell_zelewik.Models;

namespace app_shell_zelewik.Services
{
    public class MockDataStore : IDataStore<Item, Category>
    {
        readonly List<Item> items;

        readonly List<Category> categories;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Int32.Parse(Guid.NewGuid().ToString()), Text = "First item", Description="This is an item description.", Date = "12/12/2020" },
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            categories.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<Category> GetCategoryByTitleAsync(string title)
        {
            return await Task.FromResult(categories.FirstOrDefault(s => s.Title == title));
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var oldCategory = categories.Where((Category arg) => arg.Id == category.Id).FirstOrDefault();
            categories.Remove(oldCategory);
            categories.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var oldCategory = categories.Where((Category arg) => arg.Id == id).FirstOrDefault();
            categories.Remove(oldCategory);

            return await Task.FromResult(true);
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await Task.FromResult(categories.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool forseRefresh = false)
        {
            return await Task.FromResult(categories);
        }
    }
}