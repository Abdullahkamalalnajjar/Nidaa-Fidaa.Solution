using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;

namespace Nidaa_Fidaa.Core.Specification.Handller
{
    public class ShopSpecification : BaseSpecification<Shop>
    {
        public ShopSpecification()
         
        {

            AddInclude(p => p.Products);
            AddInclude(p => p.ShopCategory);
             
        
        }

        public ShopSpecification(int id):base(s=>s.Id.Equals(id))
        {

            AddInclude(p => p.Products);
            AddInclude(p => p.ShopCategory);
        }



    }
}
