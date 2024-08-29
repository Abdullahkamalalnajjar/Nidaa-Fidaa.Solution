namespace Nidaa_Fidaa.Core.Dtos.Trader
{
    public class TraderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Governorate { get; set; }
        public string Zone { get; set; }
        public string? CommercialRegistrationNumber { get; set; } // رقم السجل التجاري
        public string? TradeActivity { get; set; }
    }
}
