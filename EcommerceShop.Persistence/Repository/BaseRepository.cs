using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Domain.Models;
using EcommerceShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly EcommerceShopDbContext context;
        private readonly DbSet<T> dbSet;

        public BaseRepository(EcommerceShopDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }



        public async Task<int> AddAsync(T model)
        {
            this.dbSet.Add(model);
            return await context.SaveChangesAsync();
        }




        public async Task<int> AddRangeAsync(List<T> models)
        {
           await dbSet.AddRangeAsync(models);
           return await context.SaveChangesAsync();
        }




        public async Task<int> DeleteRangeAsync(List<Guid> ids)
        {
            List<T> entities = new List<T>();
            foreach (var id in ids)
            {
                entities.Add(new T { Id = id});
            }

            dbSet.RemoveRange(entities);
            return await context.SaveChangesAsync();
        }




        public async Task<int> DeleteRangeAsync(List<T> models)
        {
            dbSet.RemoveRange(models);
            return await context.SaveChangesAsync();
        }




        public Task<int> DeleteAsync(Guid id)
        {
            dbSet.Remove(new T { Id = id });
            return context.SaveChangesAsync();
        }




        public Task<int> DeleteAsync(T model)
        {
            dbSet.Remove(model);
            return context.SaveChangesAsync();
        }




        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Where(expression).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.FirstOrDefaultAsync(expression);
        }




        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }




        public async Task<T?> GetByIdAsync(Guid id)
        {
           return await dbSet.FindAsync(id);
        }




        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.AnyAsync(expression);
        }




        public async Task<int> UpdateAsync(T model)
        {
            dbSet.Update(model);
            return await context.SaveChangesAsync();
        }
    }
}
