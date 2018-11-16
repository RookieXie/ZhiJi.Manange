using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Service
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetListByPageAsync(int pageIndex, int pageSize);
        Task<T> GetByIdAsync(int id);
        //ManagerUser GetByWhere();

        Task<int> DeleteModelAsync(T model);
        Task<int> DeleteRangeAsync(List<T> models);
        Task<int> UpdateModelAsync(T model);
        Task<int> AddModelAsync(T model);

    }
}
