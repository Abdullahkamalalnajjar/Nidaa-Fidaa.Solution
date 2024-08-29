using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Specification.Handller
{
    public class TraderSpecification : BaseSpecification<Trader>
    {
        public TraderSpecification()
        {

            Includes.Add(m => m.Shops);


        }

        public TraderSpecification(int id) : base(c => c.Id.Equals(id))
        {
            Includes.Add(m => m.Shops);

        }
    }
}
