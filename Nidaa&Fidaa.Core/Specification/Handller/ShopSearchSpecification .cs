using System;
using System.Linq.Expressions;
using Nidaa_Fidaa.Core.Entities;

namespace Nidaa_Fidaa.Core.Specification.Handller
{
    public class ShopSearchSpecification : BaseSpecification<Shop>
    {
        public ShopSearchSpecification(string searchTerm)
            : base(shop =>
                (string.IsNullOrEmpty(searchTerm) || shop.BusinessName.Contains(searchTerm)) ||
                (string.IsNullOrEmpty(searchTerm) || shop.Location.Contains(searchTerm)))
        {
            // تضمين الفئات (ShopCategory) ثم تضمين خاصية متداخلة
            AddInclude(shop => shop.ShopCategory);
                       
        }
    }
}
