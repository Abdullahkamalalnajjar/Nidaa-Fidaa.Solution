using Nidaa_Fidaa.Core.Dtos.Trader;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface ITraderService
    {


        #region StaticService
        public Task<Trader> AddTrader(AddTrader addCustomer);
        public Task<IReadOnlyCollection<Trader>> GetTraders();
        public Task<Trader> GetTraderByid(int id);
        public Task<string> DeleteTraderByid(int id);
        public Task<IEnumerable<string>> GetCitiesAsync();
        public Task<IEnumerable<string>> GetZonesByCityAsync(string city);
        public Task<IEnumerable<string>> SearchGovernoratesAsync(string query);
        public Task<IEnumerable<string>> SearchZonesByCityAsync(string city, string query);
        #endregion


        #region DynamicService
        public Task<IReadOnlyCollection<Trader>> GetTradersWithSepc(ISpecification<Trader> spec);
        public  Task<Trader> GetTraderByidWithSpec(ISpecification<Trader> spec);


        #endregion
    }
}
