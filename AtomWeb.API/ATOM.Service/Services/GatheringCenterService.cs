using ATOM.Core.Entities;
using ATOM.Core.Repositories;
using ATOM.Core.Services;
using ATOM.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Service.Services
{
    public class GatheringCenterService : GenericService<GatheringCenter>, IGatheringCenterService
    {
        private readonly IGatheringCenterRepository _gatheringCenterRepository;

        public GatheringCenterService(IGenericRepository<GatheringCenter> genericRepository, IUnitOfWork unitOfWork, IGatheringCenterRepository gatheringCenterRepository) : base(genericRepository, unitOfWork)
        {
            _gatheringCenterRepository = gatheringCenterRepository;
        }

        public async Task<(BaseCenter, float distance)> NearGatheringCenter(decimal longitude, decimal latitude)
        {
            return await _gatheringCenterRepository.NearGatheringCenter(longitude, latitude);
        }
    }
}
