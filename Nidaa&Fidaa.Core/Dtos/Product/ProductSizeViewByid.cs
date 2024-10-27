using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Product
{
    public class ProductSizeViewByid
    {
        public int Id { get; set; } // تأكد من وجود الـ id هنا
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}
