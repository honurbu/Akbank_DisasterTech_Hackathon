using ATOM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Repositories
{
    public interface IGatheringCenterRepository : IGenericRepository<GatheringCenter>
    {
        Task<(BaseCenter, float distance)> NearGatheringCenter(decimal longitude, decimal latitude);
        Task<List<BaseCenter>> NearGatheringCenters(decimal longitude, decimal latitude);
    }
}
