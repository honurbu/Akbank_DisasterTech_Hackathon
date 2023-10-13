using JwtUser.Core.DTOs.Request;
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
    public class WreckDemandRepository : GenericRepository<WreckDemand>, IWreckDemandRepository
    {
        public WreckDemandRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddWreckDemand(AddWreckDemandDto wreckDemand)
        {
            string districtName = wreckDemand.DistrictName;
            string countyName = wreckDemand.CountyName;

            var matchingCounty = _dbContext.Counties.FirstOrDefault(c => c.Name == countyName); // İlçe (county) için arama

            if (matchingCounty != null)
            {
                var countyId = matchingCounty.Id; // İlçe (county) ID'sini al

                var matchingDistrict = _dbContext.Districts.FirstOrDefault(d => d.Name == districtName && d.CountyId == countyId); // İlçe bölgesini (district) arama

                if (matchingDistrict != null)
                {
                    var wreckDemands = new WreckDemand
                    {
                        Date = DateTime.Now,
                        Latitude = wreckDemand.Latitude,
                        Longitude = wreckDemand.Longitude,
                        DistrictId = matchingDistrict.Id // İlçe bölgesinin (district) ID'sini ata
                    };

                    _dbContext.WreckDemands.Add(wreckDemands);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
        }

        public async Task<(float AverageLatitude, float AverageLongitude)> AverageWreckLocation()
        {
            float averageLatitude = await _dbContext.WreckDemands.AverageAsync(x => x.Latitude);
            float averageLongitude = await _dbContext.WreckDemands.AverageAsync(x => x.Longitude);

            return (averageLatitude, averageLongitude);
        }
    }
}
