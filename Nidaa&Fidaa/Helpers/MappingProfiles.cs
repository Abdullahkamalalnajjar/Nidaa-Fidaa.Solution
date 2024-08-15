using AutoMapper;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Dtos;

namespace Nidaa_Fidaa.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() {
            CreateMap<Customer, CustomerDto>().ForMember(dest=>dest.TradeActivity ,opt=>opt.MapFrom(src=>src.TradeActivity.Name));
        }
    }
}
