namespace Nidaa_Fidaa.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Governorate { get; set; }
        public string Zone { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string? CommercialRegistrationNumber { get; set; } // رقم السجل التجاري
        public string? TradeActivity { get; set; }
    }
}
