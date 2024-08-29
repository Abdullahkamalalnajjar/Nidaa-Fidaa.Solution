using Nidaa_Fidaa.Core.Dtos.Driver;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface IDriverService
    {
        public Task<Driver> AddDriver(AddDriverDto addDriver);
        public Task<IReadOnlyCollection<Driver>> GetDrivers ();

        #region DynamicService
        public Task<IReadOnlyCollection<Driver>> GetDriversWithSepc(ISpecification<Driver> spec);

        #endregion
    }
}
