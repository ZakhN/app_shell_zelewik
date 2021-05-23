using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace app_shell_zelewik.Services
{
    public interface IDataStore<T, Y>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<bool> AddCategoryAsync(Y category);
        Task<bool> UpdateCategoryAsync(Y category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<Y> GetCategoryAsync(int id);
        Task<Y> GetCategoryByTitleAsync(string title);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<Y>> GetCategoriesAsync(bool forceRefresh = false);
    }
}
