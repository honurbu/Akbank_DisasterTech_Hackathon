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
    public class HelpCenterService : GenericService<HelpCenter>, IHelpCenterService
    {
        private readonly IHelpCenterRepository _helpCenterRepository;
        public HelpCenterService(IGenericRepository<HelpCenter> genericRepository, IUnitOfWork unitOfWork, IHelpCenterRepository helpCenterRepository) : base(genericRepository, unitOfWork)
        {
            _helpCenterRepository = helpCenterRepository;
        }
    }
}
