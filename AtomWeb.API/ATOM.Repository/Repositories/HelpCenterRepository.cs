using ATOM.Core.Entities;
using ATOM.Core.Repositories;
using ATOM.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Repository.Repositories
{
    public class HelpCenterRepository : GenericRepository<HelpCenter>, IHelpCenterRepository
    {
        public HelpCenterRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
