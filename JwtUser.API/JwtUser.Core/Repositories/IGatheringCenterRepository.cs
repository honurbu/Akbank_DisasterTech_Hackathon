using JwtUser.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Core.Repositories
{
    public interface IGatheringCenterRepository : IGenericRepository<GatheringCenter>
    {
       Task<BaseCenter> NearGatheringCenter(float longitude, float latitude);
    }
}
