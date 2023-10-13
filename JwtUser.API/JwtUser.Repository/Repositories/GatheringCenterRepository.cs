using JwtUser.Core.Entities;
using JwtUser.Core.Repositories;
using JwtUser.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Repository.Repositories
{
    public class GatheringCenterRepository : GenericRepository<GatheringCenter>, IGatheringCenterRepository
    {
        public GatheringCenterRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BaseCenter> NearGatheringCenter(float longitude, float latitude)
        {
            var closestGatheringCenter = await _dbContext.GatheringCenters
            .OrderBy(gc => Math.Pow(gc.Longitude - longitude, 2) + Math.Pow(gc.Latitude - latitude, 2))
            .FirstOrDefaultAsync();
           
            var closestHelpCenter = await _dbContext.HelpCenters
            .OrderBy(gc => Math.Pow(gc.Longitude - longitude, 2) + Math.Pow(gc.Latitude - latitude, 2))
            .FirstOrDefaultAsync();
           
            if(closestHelpCenter != null) 
            {
                return closestHelpCenter;
            }
            else
            {
                return closestGatheringCenter;
            }
        }
    }
}
