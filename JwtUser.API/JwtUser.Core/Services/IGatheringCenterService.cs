using JwtUser.Core.Entities;
using JwtUser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Core.Services
{
    public interface IGatheringCenterService : IGenericService<GatheringCenter>
    {
        Task<(BaseCenter, float distance)> NearGatheringCenter (float longitude, float latitude);
        Task<List<BaseCenter>> ListNearGatheringCenters(float longitude, float latitude);

    }
}
