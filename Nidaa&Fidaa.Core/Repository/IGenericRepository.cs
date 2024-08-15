using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nidaa_Fidaa.Core.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<string> AddCustomerAsync(T entity);
        public Task<IReadOnlyCollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);

    }
}
