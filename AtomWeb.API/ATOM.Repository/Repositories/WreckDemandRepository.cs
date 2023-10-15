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
    public class WreckDemandRepository : GenericRepository<WreckDemand>, IWreckDemandRepository
    {
        public WreckDemandRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddWreckDemand(AddWreckDemandDto wreckDemand)
        {
            string districtName = wreckDemand.DistrictName;
            string countyName = wreckDemand.CountyName;

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

                    // WreckDemand eklemeyi gerçekleştirin
                    var wreckDemands = new WreckDemand
                    {
                        Date = DateTime.Now,
                        Latitude = wreckDemand.Latitude,
                        Longitude = wreckDemand.Longitude,
                        DistrictId = districtId
                    };
                    _dbContext.WreckDemands.Add(wreckDemands);
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

                    // WreckDemand eklemeyi gerçekleştirin
                    var wreckDemands = new WreckDemand
                    {
                        Date = DateTime.Now,
                        Latitude = wreckDemand.Latitude,
                        Longitude = wreckDemand.Longitude,
                        DistrictId = districtId
                    };
                    _dbContext.WreckDemands.Add(wreckDemands);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    // District zaten mevcut, WreckDemand eklemeyi gerçekleştirin
                    var wreckDemands = new WreckDemand
                    {
                        Date = DateTime.Now,
                        Latitude = wreckDemand.Latitude,
                        Longitude = wreckDemand.Longitude,
                        DistrictId = matchingDistrict.Id
                    };
                    _dbContext.WreckDemands.Add(wreckDemands);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<(decimal AverageLatitude, decimal AverageLongitude)> AverageWreckLocation()
        {
            decimal averageLatitude = await _dbContext.WreckDemands.AverageAsync(x => x.Latitude);
            decimal averageLongitude = await _dbContext.WreckDemands.AverageAsync(x => x.Longitude);

            return (averageLatitude, averageLongitude);
        }
    }
}
