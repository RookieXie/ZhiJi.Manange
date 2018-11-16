using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BaseService<T> where T:class
    {
        private IBaseStore<T> _baseStore;
        public BaseService(IBaseStore<T> baseStore)
        {
            _baseStore = baseStore;
        }

        public async Task<int> AddModelAsync(T Model)
        {
            return await _baseStore.AddModelAsync(Model);
        }

        public async Task<int> DeleteModelAsync(T Model)
        {
            return await _baseStore.DeleteModelAsync(Model);
        }

        public async Task<int> DeleteRangeAsync(List<T> models)
        {
            return await _baseStore.DeleteRangeAsync(models);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _baseStore.GetByIdAsync(id);
        }

        public async Task<List<T>> GetListByPageAsync(int pageIndex, int pageSize)
        {
            return await _baseStore.GetListByPageAsync(pageIndex, pageSize);
        }

        public async Task<int> UpdateModelAsync(T Model)
        {
            return await _baseStore.UpdateModelAsync(Model);
        }
    }
}
