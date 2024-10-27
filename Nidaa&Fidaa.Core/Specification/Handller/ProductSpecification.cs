using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Specification.Handller
{
    public class ProductSpecification:BaseSpecification<Product>
    {
        public ProductSpecification(string? title=null) {

            if (!string.IsNullOrEmpty(title))
            {
                Critaria = p => p.Title.Contains(title);
            }
            Includes.Add(i=>i.Images);
            Includes.Add(s=>s.ProductSizes);   
            Includes.Add(pd=>pd.ProductAdditions);
            Includes.Add(c => c.Category);


        }

        public ProductSpecification(int id) : base(p =>p.ProductId.Equals(id)) 
        {
            Includes.Add(i => i.Images);
            Includes.Add(s => s.ProductSizes);
            Includes.Add(pd => pd.ProductAdditions);
            Includes.Add(c => c.Category);

        }
    }
}
