using JwtUser.Core.DTOs.Request;
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
    public class WreckDemandService : GenericService<WreckDemand>, IWreckDemandService
    {
        private readonly IWreckDemandRepository _wreckDemandRepository;

        public WreckDemandService(IGenericRepository<WreckDemand> genericRepository, IUnitOfWork unitOfWork, IWreckDemandRepository wreckDemandRepository) : base(genericRepository, unitOfWork)
        {
            _wreckDemandRepository = wreckDemandRepository;
        }

        public async Task AddWreckDemand(AddWreckDemandDto wreckDemand)
        {
            await _wreckDemandRepository.AddWreckDemand(wreckDemand);
        }

        public async Task<(float AverageLatitude, float AverageLongitude)> AverageWreckLocation()
        {
            return await _wreckDemandRepository.AverageWreckLocation();
        }
    }
}
