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

        #region Data of location
        // Example lists and dictionaries
        private readonly List<string> _governorates = new List<string> { "طرابلس",
            "بنغازي",
            "مصراتة",
            "الزاوية", 
            "البيضاء",
            "سبها",
            "الجميل",
            "العجيلات",
            "رقدالين",
            "زلطن",
            "مسلاتة",
            "الخميس",
            "قصر الاخيار",
            "الجبل الاخضر",
            "الواحات",
            "وادي الشاطئ",
            "وادي الحياة",
            "غات",
            "المرقب",
            "الجفارة",
            "الجبل الغربي",
            "توكرة",
            "الأبيار",
            "أوجلة",
            "مردة",
            "البطنان",
            "تاورغاء",
            "يفرن",
            "زوارة",
            "أوباري",
            "مرزق",
            "البريقة",
            "هون",
            "الكفرة",
            "القبة",
            "بني وليد",
            "المرج",
            "نالوت",
            "صرمان",
            "ودان",
            "براك",
            "الماية",
            "غدامس",
            "غريان",
            "سرت",
            "اجدابيا",
            "درنة",
            "طبرق",
            "ترهونة",
            "سوسة",
        
        
        };

        private readonly Dictionary<string, List<string>> _zonesByCity = new Dictionary<string, List<string>>
    {
        { "طرابلس", new List<string> { 
        "وسط المدينة",
        "القره بوللي",
        "عين زارة",
        "السراج",
        "أبو ستة",
        "أبو سليم",
        "أبو مشماشة",
        "أبو نواس",
        "اسبيعة",
        "الباعيش",
        "البيفي",
        "الجبس",
        "الجداعه",
        "الحشان",
        "الحي الإسلامي",
        "الحي الدبلوماسي",
        "الحي الصناعي",
        "الخروبة",
        "القره بوللي",
        "القيو",
        "الكرامه",
        "الكريمية",
        "المدينه السياحية",
        "المدينه القديمة",
        " المطاويح",
        "الصريم",
        "الظهرة",
        "العزيب",
        "العلوص",
        "العوينية",
        "الفرناج",
        "القاسي",
        "الدريبي",
        "الزهراء",
        "السبعة",
        "السدرة",
        "السواني",
        "الصريم",
        "المنصورة",
        "النجيلة",
        "النوفليين",
        "الهاني",
        "الهضبة الخضراء",
        "بئر توته",
        "باب العزيزية",
        "باب بن غشير",
        "بشير السعداوي",
        "بلوزة",
        "بن عاشور",
        "بونعيم",
        "تاجوراء",
        "جامع صقع",
        "جامعة طرابلس",
        "جزيرة الفحم",
        "جنزور",
        "حي الأنتصار",
        "حي الأندلس",
        "حي البطاطا",
        "سوق خميس",
        "سيدي السائح ",
        "سيدي المصري",
        "سيدي خليفة",
        "شارع الاستقلال",
        "شارع الجماهيرية",
        "شارع الخلاطات",
        "ذات العماد",
        "رأس حسن",
        "زاوية الدهماني",
        "زطارنه",
        "زناتة",
        "سوق الجمعة",
        "سوق المشير",
        "حي السلام",
        "حي القادسية",
        "حي الكويت",
        "حي دمشق",
        "خلة الفرجان",
        "شارع الزاوية",
        "شارع السيدي",
        "شارع المسيرة الكبري",
        "شارع النصر",
        "شارع جرابة",
        "شارع سليم",
        "شارع شوقي",
        "شارع عمر المختار",
        "شارع ميزران",
        "شارع هايتي",
        "شرفة الملاحة",
        "شيل البوعيشي",
        "صلاح الدين",
        "طريق الأحياء البرية",
        "طريق السور القديم",
        "طريق الشوك",
        "طريق الفلاح",
        "طريق المشتل",
        "طريق المطار",
        "قرقارش",
        "قصر بن غشير",
        "كشلاف",
        "معسكر اليرموك",
        "منقطة المزاوغة",
        "ميدان الجزائر",
        "وادي الربيع",
        "أخري",
        "غوط الشعال",
        "فشلوم",
        "قرجي",
        "قرقارش",
        "طريق الهضبة الشرقية",
        "طريق الهضبة طول",
        "عرادة",
        "عين زرارة",
        "غرغور",
        }
            },

        { "بنغازي", new List<string> { "الهواري", "الكويفية", "سيدي خليفة", "السلماني" } },
        { "مصراتة", new List<string> { "الكراريم", "السكت", "طمينة", "قصر أحمد" } },
        { "الزاوية", new List<string> { "الحرشة", "المطرد", "أبو عيسى", "النمروش" } },
        { "البيضاء", new List<string> { "شحات", "بلقس", "العويلية", "الوسطة" } },
        { "سبها", new List<string> { "القرضة", "المهدية", "المنشية", "الجديد" } },
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
