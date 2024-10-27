using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IDNumber { get; set; }
        public string? Governorate { get; set; }
        public string? Municipality { get; set; } //البلديه
        public string? TransportationType { get; set; } // Motorcycle, Scooter, Walker, Car
        public string? IDCardPhotoFront { get; set; }
        public string? IDCardPhotoBack { get; set; }
        public string? LicensePlateNumber { get; set; }
        public string? FrontViewPhoto { get; set; }
        public string? RearViewPhoto { get; set; }
        public string? FullViewWithPlatePhoto { get; set; }
        public string? DriverLicensePhoto { get; set; } // Optional
    }


}
