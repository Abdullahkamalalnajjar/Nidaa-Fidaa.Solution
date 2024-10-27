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
                    .Include(s => s.Products)
                        .ThenInclude(p => p.ProductSizes)
                    .Include(s => s.Products)
                        .ThenInclude(p => p.ProductAdditions)
                    .Include(s => s.ShopCategory)
                        .ThenInclude(sc => sc.Category)
                            .ThenInclude(c => c.Products) // تضمين المنتجات في الفئة
                                .ThenInclude(p => p.ProductSizes) // تضمين أحجام المنتجات
                    .Include(s => s.ShopCategory)
                        .ThenInclude(sc => sc.Category)
                            .ThenInclude(c => c.Products) // تضمين المنتجات
                                .ThenInclude(p => p.ProductAdditions) // تضمين الإضافات على المنتجات
                    .ToListAsync();
            }

            if (typeof(T) == typeof(Trader))
            {
                return (IReadOnlyCollection<T>)await dbContext.Set<Trader>()
                    .Include(m => m.Shops)
                        .ThenInclude(s => s.Products)
                            .ThenInclude(p => p.ProductSizes)
                    .ToListAsync();
            }

            if (typeof(T) == typeof(Category))
            {
                return (IReadOnlyCollection<T>)await dbContext.Set<Category>()
                    .Include(c => c.ShopCategory)
                        .ThenInclude(sc => sc.Shop)
                            .ThenInclude(s => s.Products) // تضمين المنتجات داخل الفئة
                                .ThenInclude(p => p.ProductSizes) // تضمين أحجام المنتجات
                    .ToListAsync();
            }

            return await dbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
              if (typeof(T) == typeof(Shop))
    {
                var shop = await dbContext.Set<Shop>()
             .Include(s => s.ShopCategory)
                 .ThenInclude(sc => sc.Category)
                     .ThenInclude(c => c.Products.Where(p => p.ShopId == id))
                .Include(s => s.ShopCategory)
                 .ThenInclude(sc => sc.Category)
                     .ThenInclude(c => c.Products).ThenInclude(ps=>ps.ProductSizes)  
                     .Include(s => s.ShopCategory)
                 .ThenInclude(sc => sc.Category)
                     .ThenInclude(c => c.Products).ThenInclude(pa=>pa.ProductAdditions)
             .Include(s => s.Products)
                 .ThenInclude(p => p.ProductSizes)
             .Include(s => s.Products)
                 .ThenInclude(p => p.ProductAdditions)
             .AsSplitQuery() // تحسين الأداء عن طريق تقسيم الاستعلام
             .AsNoTracking() // إذا لم تكن بحاجة لتتبع التغييرات
             .SingleOrDefaultAsync(s => s.Id == id); // جلب المحل المطلوب حسب المعرف

                return (T)(object)shop;
            }
    
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
        public async Task<T?> GetSingleWithSpecAsync(ISpecification<T> specification)
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
