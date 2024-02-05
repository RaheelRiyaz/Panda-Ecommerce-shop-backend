﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IRepository
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<int> AddAsync(T model);
        Task<int> AddRangeAsync(List<T> models);
        Task<int> UpdateAsync(T model);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteRangeAsync(List<Guid> ids);
        Task<int> DeleteRangeAsync(List<T> models);
        Task<int> DeleteAsync(T model);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T,bool>> expression);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T,bool>> expression);
        Task<bool> IsExistsAsync(Expression<Func<T,bool>> expression);
    }
}
