using Microsoft.AspNetCore.Http;


namespace Nidaa_Fidaa.Core.Dtos.Identity
{
    public class RegisterDto
    {
        public string UserType { get; set; } // نوع المستخدم
        public string Password { get; set; } 

        #region Customer
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? Governorate { get; set; }

        public string? Zone { get; set; }
        #endregion
        #region Trader
     
        public string? Municipality { get; set; }
        public string? CommercialRegistrationNumber { get; set; } // رقم السجل التجاري
        public string? TradeActivityName { get; set; }
        #endregion
        #region Driver
        public int IDNumber { get; set; }
        public string? TransportationType { get; set; } // Motorcycle, Scooter, Walker, Car
        public string? IDCardPhotoFront { get; set; }
        public string? IDCardPhotoBack { get; set; }
        public string? LicensePlateNumber { get; set; }
        public string? FrontViewPhoto { get; set; }
        public string? RearViewPhoto { get; set; }
        public string? FullViewWithPlatePhoto { get; set; }
        public string? DriverLicensePhoto { get; set; } // Optional
        #endregion
    }

}
