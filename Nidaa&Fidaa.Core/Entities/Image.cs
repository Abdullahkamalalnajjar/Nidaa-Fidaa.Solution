using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

    }

}
