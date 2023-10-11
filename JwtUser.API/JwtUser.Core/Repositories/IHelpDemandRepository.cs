using JwtUser.Core.DTOs.Request;
using JwtUser.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Core.Repositories
{
    public interface IHelpDemandRepository : IGenericRepository<HelpDemand>
    {
        public Task AddHelpDemand(AddHelpDemandDto helpDemand);
    }
}
