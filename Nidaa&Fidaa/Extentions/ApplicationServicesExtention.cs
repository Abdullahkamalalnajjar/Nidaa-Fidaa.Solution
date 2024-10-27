//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.Tokens;
//using RASM.Api.Helpers;
//using RASM.Core.Entities.Identity;
//using RASM.Core.Repository;
//using RASM.Core.Services;
//using RASM.Repository.AppDbContext;
//using RASM.Respository;
//using RASM.Service;
//using System.Text;

//namespace RASM.Api.Extentions
//{
//    public static class ApplicationServicesExtention
//    {
//        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
//        {


//            Services.AddSignalR();
//            Services.AddHttpContextAccessor();

//            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
//            Services.AddScoped(typeof(IContactService), typeof(ContactService));
//            Services.AddScoped(typeof(IOperationsAndMaintenanceRequestService), typeof(OperationsAndMaintenanceRequestService));
//            Services.AddScoped(typeof(INewProjectService), typeof(NewProjectService));
//            Services.AddScoped(typeof(IAdminService), typeof(AdminService));

//            #region Config AutoMapper 
//            Services.AddAutoMapper(cfg => cfg.AddProfile(typeof(ProfilesMapping)));
//            #endregion

//            #region cfg igeneric
//            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//            #endregion



           

//            return Services;

//        }
//    }



//}
