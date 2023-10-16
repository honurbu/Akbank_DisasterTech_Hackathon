using ATOM.Core.DTOs;
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
        public Task AverageWrackPop(AddWreckDemandDto wreckDemand);
        public Task<(WreckPopulation, float distance)> GetWreckOperation(string id);
        public void ChangeStatus(int wreckPopId);
    }
}