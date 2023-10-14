using JwtUser.Core.Entities;
using JwtUser.Core.Repositories;
using JwtUser.Core.Services;
using JwtUser.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Service.Services
{
    public class GatheringCenterService : GenericService<GatheringCenter>, IGatheringCenterService
    {
        private readonly IGatheringCenterRepository _gatheringCenterRepository;

        public GatheringCenterService(IGenericRepository<GatheringCenter> genericRepository, IUnitOfWork unitOfWork, IGatheringCenterRepository gatheringCenterRepository) : base(genericRepository, unitOfWork)
        {
            _gatheringCenterRepository = gatheringCenterRepository;
        }

        public Task<List<BaseCenter>> ListNearGatheringCenters(float longitude, float latitude)
        {
            throw new NotImplementedException();
        }

        public async Task<(BaseCenter, float distance)> NearGatheringCenter(float longitude, float latitude)
        {
            return await _gatheringCenterRepository.NearGatheringCenter(longitude, latitude);
        }
    }
}
