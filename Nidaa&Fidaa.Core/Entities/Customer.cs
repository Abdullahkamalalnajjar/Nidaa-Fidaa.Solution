using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
  
        public class Customer
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Governorate { get; set; }
            public string Zone { get; set; }
            public string ProfilePictureUrl { get; set; }
            public string? CommercialRegistrationNumber { get; set; } // رقم السجل التجاري

            // المفتاح الأجنبي للنشاط التجاري
            public int TradeActivityId { get; set; }

            // خاصية التنقل للنشاط التجاري
            public TradeActivity TradeActivity { get; set; }
        }
    

}
