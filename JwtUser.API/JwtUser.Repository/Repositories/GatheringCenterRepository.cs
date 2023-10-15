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



        public async Task<(BaseCenter, float distance)> NearGatheringCenter(float longitude, float latitude)
        {
            HelpCenter closestHelpCenter;
            GatheringCenter closestGatheringCenter;
            float distanceKm = 0;

            closestHelpCenter = await _dbContext.HelpCenters.Include(x=>x.CenterType)
            .OrderBy(gc => Math.Pow(gc.Longitude - longitude, 2) + Math.Pow(gc.Latitude - latitude, 2))
            .FirstOrDefaultAsync();

            if (closestHelpCenter != null)
            {
                #region

                var radius = 6371;
                var dLat = Deg2Rad(closestHelpCenter.Latitude - latitude);
                var dLong = Deg2Rad(closestHelpCenter.Longitude - longitude);


                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(Deg2Rad(latitude)) * Math.Cos(Deg2Rad(closestHelpCenter.Latitude)) *
                    Math.Sin(dLong / 2) * Math.Sin(dLong / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double d = radius * c;

                distanceKm = (float)d;

                #endregion

            }

            if (distanceKm > 0 && distanceKm < 8)
            {
                return (closestHelpCenter!, distanceKm);
            }
            else
            {
                closestGatheringCenter = await _dbContext.GatheringCenters.Include(x => x.CenterType)
                .OrderBy(gc => Math.Pow(gc.Longitude - longitude, 2) + Math.Pow(gc.Latitude - latitude, 2))
                .FirstOrDefaultAsync();

                var radius = 6371;
                var dLat = Deg2Rad(closestGatheringCenter.Latitude - latitude);
                var dLong = Deg2Rad(closestGatheringCenter.Longitude - longitude);


                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(Deg2Rad(latitude)) * Math.Cos(Deg2Rad(closestGatheringCenter.Latitude)) *
                    Math.Sin(dLong / 2) * Math.Sin(dLong / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double d = radius * c;

                if(closestHelpCenter != null)
                {
                    if (distanceKm > (float)d +2)
                    {
                        distanceKm = (float)d;

                        return (closestGatheringCenter, distanceKm);
                    }
                    else
                    {
                        distanceKm = (float)d;

                        return (closestHelpCenter, distanceKm);
                    }

                }

                distanceKm = (float)d;

                return (closestGatheringCenter, distanceKm);
            }
        }

        public Task<List<BaseCenter>> NearGatheringCenters(float longitude, float latitude)
        {
            //            var closestGatheringCenter = await _dbContext.GatheringCenters
            //                        .OrderBy(gc => Math.Pow(gc.Longitude - longitude, 2) + Math.Pow(gc.Latitude - latitude, 2))
            //                        .ToListAsync();

            //            var closestHelpCenter = await _dbContext.HelpCenters
            //                        .OrderBy(gc => Math.Pow(gc.Longitude - longitude, 2) + Math.Pow(gc.Latitude - latitude, 2))
            //                        .ToListAsync();

            //            if (closestHelpCenter != null)
            //            {
            //                return closestHelpCenter.Take(3);
            //            }
            //            else
            //            {
            //                return closestGatheringCenter.Take(3);
            //;
            //            }
            throw new NotImplementedException();
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

    }
}
