using System;
using System.Linq.Expressions;
using Nidaa_Fidaa.Core.Entities;

namespace Nidaa_Fidaa.Core.Specification.Handller
{
    public class ProductSearchSpecification : BaseSpecification<Product>
    {
        public ProductSearchSpecification(string searchTerm)
            : base(product =>
                (string.IsNullOrEmpty(searchTerm) || product.Title.Contains(searchTerm)) ||
                (string.IsNullOrEmpty(searchTerm) || product.Description.Contains(searchTerm)))
        {

            Includes.Add(i => i.Images);
            Includes.Add(s => s.ProductSizes);
            Includes.Add(pd => pd.ProductAdditions);
            Includes.Add(c => c.Category);

        }
    }
}
