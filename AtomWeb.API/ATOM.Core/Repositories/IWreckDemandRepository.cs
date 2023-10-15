using ATOM.Core.DTOs.Request;
using ATOM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Repositories
{
    public interface IWreckDemandRepository : IGenericRepository<WreckDemand>
    {
        public Task AddWreckDemand(AddWreckDemandDto wreckDemand);
        public Task<(decimal AverageLatitude, decimal AverageLongitude)> AverageWreckLocation();

    }
}
