using JwtUser.Core.DTOs.Request;
using JwtUser.Core.Entities;
using JwtUser.Core.Repositories;
using JwtUser.Core.Services;
using JwtUser.Core.UnitOfWorks;
using JwtUser.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Service.Services
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
    }
}
