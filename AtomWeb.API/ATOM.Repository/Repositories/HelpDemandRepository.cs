using ATOM.Core.DTOs.Request;
using ATOM.Core.Entities;
using ATOM.Core.Repositories;
using ATOM.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Repository.Repositories
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

            var matchingCounty = _dbContext.Counties.FirstOrDefault(c => c.Name == countyName);

            if (matchingCounty == null)
            {
                // County veritabanında yoksa ekleyin
                var newCounty = new County
                {
                    Name = countyName,
                    CityId = 1
                };
                _dbContext.Counties.Add(newCounty);
                await _dbContext.SaveChangesAsync();

                // Yeni eklenen County'in ID'sini alın
                var countyId = newCounty.Id;

                // District veritabanında yoksa ekleyin
                var matchingDistrict = _dbContext.Districts.FirstOrDefault(d => d.Name == districtName && d.CountyId == countyId);
                if (matchingDistrict == null)
                {
                    var newDistrict = new District
                    {
                        Name = districtName,
                        CountyId = countyId
                    };
                    _dbContext.Districts.Add(newDistrict);
                    await _dbContext.SaveChangesAsync();
                    // Şimdi yeni District'in ID'sini alabilirsiniz.
                    var districtId = newDistrict.Id;

                    // helpDemand eklemeyi gerçekleştirin
                    var helpDemands = new HelpDemand
                    {
                        Date = DateTime.Now,
                        Latitude = helpDemand.Latitude,
                        Longitude = helpDemand.Longitude,
                        DistrictId = districtId,
                        CategoryId = helpDemand.CategoryId,
                        HelpCenterId = helpDemand.HelpCenterId,
                        GatheringCenterId =helpDemand.GatheringCenterId  
                    };
                    _dbContext.HelpDemands.Add(helpDemands);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                // Var olan County'in ID'sini alın
                var countyId = matchingCounty.Id;

                // İlçeyi kontrol edin
                var matchingDistrict = _dbContext.Districts.FirstOrDefault(d => d.Name == districtName && d.CountyId == countyId);
                if (matchingDistrict == null)
                {
                    var newDistrict = new District
                    {
                        Name = districtName,
                        CountyId = countyId
                    };
                    _dbContext.Districts.Add(newDistrict);
                    await _dbContext.SaveChangesAsync();
                    // Şimdi yeni District'in ID'sini alabilirsiniz.
                    var districtId = newDistrict.Id;

                    // helpDemand eklemeyi gerçekleştirin
                    var helpDemands = new HelpDemand
                    {
                        Date = DateTime.Now,
                        Latitude = helpDemand.Latitude,
                        Longitude = helpDemand.Longitude,
                        DistrictId = districtId,
                        CategoryId = helpDemand.CategoryId,
                        HelpCenterId = helpDemand.HelpCenterId,
                        GatheringCenterId = helpDemand.GatheringCenterId
                    };
                    _dbContext.HelpDemands.Add(helpDemands);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    // District zaten mevcut, helpDemand eklemeyi gerçekleştirin
                    var helpDemands = new HelpDemand
                    {
                        Date = DateTime.Now,
                        Latitude = helpDemand.Latitude,
                        Longitude = helpDemand.Longitude,
                        DistrictId = matchingDistrict.Id,
                        CategoryId = helpDemand.CategoryId,
                        HelpCenterId = helpDemand.HelpCenterId,
                        GatheringCenterId = helpDemand.GatheringCenterId
                    };
                    _dbContext.HelpDemands.Add(helpDemands);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<(decimal AverageLatitude, decimal AverageLongitude)> AverageHelpLocation(int id)
        {
            decimal averageLatitude = await _dbContext.HelpDemands.Where(x => x.CategoryId == id).AverageAsync(x => x.Latitude);
            decimal averageLongitude = await _dbContext.HelpDemands.Where(x => x.CategoryId == id).AverageAsync(x => x.Longitude);

            return (averageLatitude, averageLongitude);
        }
    }
}
