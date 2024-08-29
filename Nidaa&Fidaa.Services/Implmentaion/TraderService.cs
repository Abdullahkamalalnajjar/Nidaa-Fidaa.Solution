using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos.Trader;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Implmentaion
{
    public class TraderService : ITraderService
    {

        #region Data
        // Example lists and dictionaries
        private readonly List<string> _governorates = new List<string> { "طرابلس", "بنغازي", "مصراتة", "الزاوية", "البيضاء", "سبها" };

        private readonly Dictionary<string, List<string>> _zonesByCity = new Dictionary<string, List<string>>
    {
        { "طرابلس", new List<string> { "وسط المدينة", "القره بوللي", "عين زارة", "السراج" } },
        { "بنغازي", new List<string> { "الهواري", "الكويفية", "سيدي خليفة", "السلماني" } },
        { "مصراتة", new List<string> { "الكراريم", "السكت", "طمينة", "قصر أحمد" } },
        { "الزاوية", new List<string> { "الحرشة", "المطرد", "أبو عيسى", "النمروش" } },
        { "البيضاء", new List<string> { "شحات", "بلقس", "العويلية", "الوسطة" } },
        { "سبها", new List<string> { "القرضة", "المهدية", "المنشية", "الجديد" } }
    };
        #endregion

        private readonly IGenericRepository<Trader> customerRepo;
        private readonly IGenericRepository<Shop> _merchantRepo;
        private readonly IMapper mapper;
        #region Constractor
        public TraderService(IGenericRepository<Trader> customerRepo, IGenericRepository<Shop> merchantRepo,IMapper mapper)
        {
            this.customerRepo = customerRepo;
            _merchantRepo = merchantRepo;
            this.mapper = mapper;
        }
        #endregion


        #region StaticService
        public async Task<Trader> AddTrader(AddTrader addCustomer)
        {
            var check = customerRepo.GetTableNoTracking().Where(c => c.Name == addCustomer.Name).FirstOrDefault();
            if (check == null)
            {
                var customer = mapper.Map<Trader>(addCustomer);
                await customerRepo.AddAsync(customer);
                return customer;
            }

            return null;

        }

        public async Task<string> DeleteTraderByid(int id)
        {
            var customer = await customerRepo.GetByIdAsync(id);

            if (customer != null)
            {
                await customerRepo.DeleteAsync(customer);
                return "Deleted";
            }
            return "NotFound";
        }

        public async Task<Trader> GetTraderByid(int id)
        {


            var customer = await customerRepo.GetByIdAsync(id);

            return customer;

        }

        public Task<IReadOnlyCollection<Trader>> GetTraders()
        {
            return customerRepo.GetAllAsync();
        }

        ////////////////////////// Location///////////////////

        public Task<IEnumerable<string>> GetCitiesAsync()
        {
            return Task.FromResult<IEnumerable<string>>(_governorates);
        }

        public Task<IEnumerable<string>> GetZonesByCityAsync(string city)
        {
            _zonesByCity.TryGetValue(city, out var zones);
            return Task.FromResult(zones ?? Enumerable.Empty<string>());
        }

        public Task<IEnumerable<string>> SearchGovernoratesAsync(string query)
        {
            var result = _governorates
                .Where(g => g.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult<IEnumerable<string>>(result);
        }

        public Task<IEnumerable<string>> SearchZonesByCityAsync(string city, string query)
        {
            if (!_zonesByCity.ContainsKey(city))
            {
                return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());
            }

            var zones = _zonesByCity[city];
            var result = zones
                .Where(z => z.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult<IEnumerable<string>>(result);
        }

        #endregion


        #region DynamicService


        public Task<IReadOnlyCollection<Trader>> GetTradersWithSepc(ISpecification<Trader> spec)
        {
            var customers = customerRepo.GetAllWithSpecAsync(spec);
            return customers;
        }

        public async Task<Trader> GetTraderByidWithSpec(ISpecification<Trader> spec)
        {
            var customer = await customerRepo.GetByIdWithSpecAsync(spec);    
            return customer;
        }


        #endregion
    }
}
