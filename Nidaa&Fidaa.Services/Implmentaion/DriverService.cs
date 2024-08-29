using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos.Driver;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Implmentaion
{
    public class DriverService : IDriverService
    {
        private readonly IGenericRepository<Driver> driverRepo;
        private readonly IMapper mapper;
        #region Constractor
        public DriverService(IGenericRepository<Driver>  driverRepo ,IMapper mapper)
        {
            this.driverRepo = driverRepo;
            this.mapper = mapper;
        }
        #endregion
        public async Task<Driver> AddDriver(AddDriverDto addDriver)
        {
          var check = await driverRepo.GetTableNoTracking().Where(d=>d.IDNumber == addDriver.IDNumber).FirstOrDefaultAsync();
            if (check == null)
            {
                var driver = mapper.Map<Driver>(addDriver);
                await driverRepo.AddAsync(driver);
                return driver;
            }
            return null;
            
        }

    
        public async Task<IReadOnlyCollection<Driver>> GetDrivers()
        {
            var drivers = await driverRepo.GetAllAsync();
            return drivers;
            
        }


        #region DynamicService
        public async Task<IReadOnlyCollection<Driver>> GetDriversWithSepc(ISpecification<Driver> spec)
        {
            var drivers = await driverRepo.GetAllWithSpecAsync(spec);
            return drivers;
        }

 

        #endregion

    }
}
