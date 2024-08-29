using Microsoft.AspNetCore.Http;

namespace Nidaa_Fidaa.Core.Dtos.Trader
{
    public class AddTrader
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Governorate { get; set; }
        public string Municipality { get; set; }
        public string? CommercialRegistrationNumber { get; set; } // رقم السجل التجاري
        public string? TradeActivityName { get; set; }
    }
}
