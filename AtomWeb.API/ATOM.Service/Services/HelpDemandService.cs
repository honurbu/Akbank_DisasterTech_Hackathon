using ATOM.Core.DTOs;
using ATOM.Core.DTOs.Request;
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
    public class HelpDemandService : GenericService<HelpDemand>, IHelpDemandService
    {
        private readonly IHelpDemandRepository _helpDemandRepository;

        public HelpDemandService(IGenericRepository<HelpDemand> genericRepository, IUnitOfWork unitOfWork, IHelpDemandRepository helpDemandRepository) : base(genericRepository, unitOfWork)
        {
            _helpDemandRepository = helpDemandRepository;
        }

        public async Task AddHelpDemand(AddHelpDemandDto helpDemand)
        {
            await _helpDemandRepository.AddHelpDemand(helpDemand);
        }

        public async Task<(decimal AverageLatitude, decimal AverageLongitude)> AverageHelpLocation(int id)
        {
            return await _helpDemandRepository.AverageHelpLocation(id);
        }

        public async Task Test(HelpPopulationDto helpDemand)
        {
            await _helpDemandRepository.Test(helpDemand);
        }
    }
}
