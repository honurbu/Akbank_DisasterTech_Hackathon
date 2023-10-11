using JwtUser.Core.DTOs.Request;
using JwtUser.Core.Entities;
using JwtUser.Core.Repositories;
using JwtUser.Repository.Context;
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

            var matchingDistrict = _dbContext.Districts.FirstOrDefault(d => d.Name == districtName);
            if (matchingDistrict != null)
            {
                var helpDemands = new HelpDemand
                {
                    CategoryId = helpDemand.CategoryId,
                    Date = DateTime.Now,
                    Latitude = helpDemand.Latitude,
                    Longitude = helpDemand.Longitude,
                    DistrictId = matchingDistrict.Id // Eşleşen mahallenin Id'sini ata
                };

                _dbContext.HelpDemands.Add(helpDemands);
                await _dbContext.SaveChangesAsync(); // Async olarak veritabanına kaydedin
            }
        }

    }
}
