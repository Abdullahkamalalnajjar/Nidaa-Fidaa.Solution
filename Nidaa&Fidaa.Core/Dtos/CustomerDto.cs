using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos
{
    public  class CustomerDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? Governorate { get; set; }

        public string? Zone { get; set; }
    }
}
