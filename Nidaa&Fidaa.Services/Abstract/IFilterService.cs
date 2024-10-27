using Nidaa_Fidaa.Core.Dtos.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface IFilterService
    {
        public Task<FilterResultDto> FilterAsync(string? searchTerm , decimal? minDiscontedPrice , decimal? maxDiscontedPrice);

    }
}
