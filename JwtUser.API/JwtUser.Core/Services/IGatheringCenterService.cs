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
        Task<BaseCenter> NearGatheringCenter(float longitude, float latitude);

    }
}
