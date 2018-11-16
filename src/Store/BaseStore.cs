using EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class BaseStore<T> where T:class
    {
       
        public MySqlContext _mySqlContext;

        public BaseStore(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }
        public async Task<List<T>> GetListByPageAsync(int pageIndex,int pageSize)
        {
            var skipCount = (pageIndex - 1) * pageSize;
            var result =await _mySqlContext.Set<T>().AsNoTracking().Skip(skipCount).Take(pageSize).ToListAsync(); ;
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result =await _mySqlContext.Set<T>().FindAsync(id);
            return result;
        }


        public async Task<int> DeleteModelAsync(T model)
        {
            _mySqlContext.Set<T>().Remove(model);
            var result =await  _mySqlContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteRangeAsync(List<T> models)
        {
            _mySqlContext.Set<T>().RemoveRange(models);
            var result = await _mySqlContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateModelAsync(T model)
        {
            _mySqlContext.Set<T>().Update(model);
            var result = await _mySqlContext.SaveChangesAsync();
            return result;
        }
        public async Task<int> AddModelAsync(T model)
        {
            _mySqlContext.Set<T>().Add(model);
            var result = await _mySqlContext.SaveChangesAsync();
            return result;
        }

    }
}
