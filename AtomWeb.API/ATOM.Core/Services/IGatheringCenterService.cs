using ATOM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Services
{
    public interface IGatheringCenterService : IGenericService<GatheringCenter>
    {
        Task<(BaseCenter, decimal distance)> NearGatheringCenter(decimal longitude, decimal latitude);
        Task<List<BaseCenter>> ListNearGatheringCenters(decimal longitude, decimal latitude);

    }
}
