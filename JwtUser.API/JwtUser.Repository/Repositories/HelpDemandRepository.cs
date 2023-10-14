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
    public class HelpDemandRepository : GenericRepository<HelpDemand>, IHelpDemandRepository
    {
        public HelpDemandRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddHelpDemand(AddHelpDemandDto helpDemand)
        {
            string districtName = helpDemand.DistrictName;
            string countyName = helpDemand.CountyName;

            var matchingCounty = _dbContext.Counties.FirstOrDefault(c => c.Name == countyName); // İlçe (county) için arama

            if (matchingCounty != null)
            {
                var countyId = matchingCounty.Id; // İlçe (county) ID'sini al

                var matchingDistrict = _dbContext.Districts.FirstOrDefault(d => d.Name == districtName && d.CountyId == countyId); // İlçe bölgesini (district) arama

                if (matchingDistrict != null)
                {
                    var helpDemands = new HelpDemand
                    {
                        CategoryId = helpDemand.CategoryId,
                        Date = DateTime.Now,
                        Latitude = helpDemand.Latitude,
                        Longitude = helpDemand.Longitude,
                        DistrictId = matchingDistrict.Id // İlçe bölgesinin (district) ID'sini ata
                    };

                    _dbContext.HelpDemands.Add(helpDemands);
                    await _dbContext.SaveChangesAsync(); 
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
        }

        public async Task<(float AverageLatitude, float AverageLongitude)> AverageHelpLocation(int id)
        {
            float averageLatitude = await _dbContext.HelpDemands.Where(x=>x.CategoryId==id).AverageAsync(x => x.Latitude);
            float averageLongitude = await _dbContext.HelpDemands.Where(x => x.CategoryId == id).AverageAsync(x => x.Longitude);

            return (averageLatitude, averageLongitude);
        }
    }
}
