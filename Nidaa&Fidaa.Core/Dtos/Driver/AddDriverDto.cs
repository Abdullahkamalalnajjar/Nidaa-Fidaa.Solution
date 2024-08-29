using Microsoft.AspNetCore.Http;

namespace Nidaa_Fidaa.Core.Dtos.Driver
{
    public class AddDriverDto
    {
        public string Name { get; set; }
        public int IDNumber { get; set; }
        public string Governorate { get; set; }
        public string Municipality { get; set; } //البلديه
        public string TransportationType { get; set; } // Motorcycle, Scooter, Walker, Car

        public IFormFile IDCardPhotoFront { get; set; }
        public IFormFile IDCardPhotoBack { get; set; }
        public string LicensePlateNumber { get; set; }
        public IFormFile FrontViewPhoto { get; set; }
        public IFormFile RearViewPhoto { get; set; }
        public IFormFile FullViewWithPlatePhoto { get; set; }
        public IFormFile DriverLicensePhoto { get; set; } // Optional
    }
}



