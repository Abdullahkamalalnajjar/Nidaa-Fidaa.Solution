using Nidaa_Fidaa.Core.Dtos.Driver;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Specification.Handller
{
    public class DriverSpecification:BaseSpecification<Driver>
    {
        public DriverSpecification(AddDriverDto? addDriverDto=null) {

            if (addDriverDto != null)
            {
                Critaria = d =>
                    d.Name == addDriverDto.Name &&
                    d.IDNumber == addDriverDto.IDNumber &&
                    d.Governorate == addDriverDto.Governorate &&
                    d.Municipality == addDriverDto.Municipality;
            }
        }

        public DriverSpecification(int id):base(d=>d.Id.Equals(id)) 
        {
                
        }
    }
}
