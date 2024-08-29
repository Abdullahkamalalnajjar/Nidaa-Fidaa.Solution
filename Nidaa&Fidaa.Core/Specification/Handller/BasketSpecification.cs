using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Specification.Handller
{
    public class BasketSpecification:BaseSpecification<Basket>
    {
        public BasketSpecification()
        {
            Includes.Add(b => b.Items);
        }
    }
}
