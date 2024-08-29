using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Repository;
using Nidaa_Fidaa.Respository.Data;

namespace Nidaa_Fidaa.Respository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return dbContext.Set<T>().AsNoTracking().AsQueryable();
        }


        #region Static Repository
        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
           
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Shop))
            {
                return (IReadOnlyCollection<T>)await dbContext.Set<Shop>()
            .Include(m => m.ShopCategory)
                .ThenInclude(mc => mc.Category)
            .Include(m => m.Products)
                .ThenInclude(p => p.ProductSizes)
            .Include(m => m.Products)
                .ThenInclude(p => p.ProductAdditions)
            .ToListAsync();

            } 
            if (typeof(T) == typeof(Category))
            {
                return (IReadOnlyCollection<T>)await dbContext.Set<Category>().Include(m => m.ShopCategory).ThenInclude(c=>c.Shop).ToListAsync();

            }
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //if (typeof(T) == typeof(Merchant))
            //{
            //    return await dbContext.Merchants.Include(m => m.MerchantCategory).ThenInclude(c => c.Category).FirstOrDefaultAsync(m => m.Id == id) as T;

            //}
            return await dbContext.Set<T>().FindAsync(id);
        }

        #endregion

        #region DynamicRepository

        public async Task<IReadOnlyCollection<T>> GetAllWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }


        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> specification)
        { 
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
        }


        #endregion

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQurey(dbContext.Set<T>(), spec);
        }
    }
}
