using ATOM.Core.DTOs;
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

                await AverageWrackPop(wreckDemand);
            }
        }

        public async Task AverageWrackPop(AddWreckDemandDto wreckDemand)
        {
            var districtPopulation = await _dbContext.Districts.FirstOrDefaultAsync(x => x.Name == wreckDemand.DistrictName);
            var districtPopulationId = districtPopulation.Id;

            var wrackPop = await _dbContext.WreckPopulations.FirstOrDefaultAsync(x => x.DistrictId == districtPopulationId);

            if (wrackPop == null)
            {
                WreckPopulation newWreck = new WreckPopulation
                {
                    DistrictId = districtPopulationId,
                    Latitude = wreckDemand.Latitude,
                    Longitude = wreckDemand.Longitude,
                    People = 1
                };
                await _dbContext.WreckPopulations.AddAsync(newWreck);
            }
            else
            {
                // Mahalleye ait kayıt var, enlem ve boylamı güncelle
                wrackPop.Latitude = ((wrackPop.Latitude * wrackPop.People) + wreckDemand.Latitude) / (wrackPop.People + 1);
                wrackPop.Longitude = ((wrackPop.Longitude * wrackPop.People) + wreckDemand.Longitude) / (wrackPop.People + 1);

                // People sayısını artır
                wrackPop.People++;
            }
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<(decimal AverageLatitude, decimal AverageLongitude)> AverageWreckLocation()
        {
            decimal averageLatitude = await _dbContext.WreckDemands.AverageAsync(x => x.Latitude);
            decimal averageLongitude = await _dbContext.WreckDemands.AverageAsync(x => x.Longitude);

            return (averageLatitude, averageLongitude);
        }

        public void ChangeStatus(int wreckPopId)
        {
            var values = _dbContext.WreckPopulations.Find(wreckPopId);
            if (values != null)
            {
                values.IsClaimed = true;
                _dbContext.SaveChanges();
            }
        }




        public async Task<(WreckPopulation, float distance)> GetWreckOperation(string id)
        {
            var appUser = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            float distanceKm = 0;

            if (appUser != null)
            {
                var nearestWreckPopulation = await _dbContext.WreckPopulations.Where(x => x.IsClaimed == false).Include(x => x.District).ThenInclude(x => x.County)
                 .OrderBy(wp => Math.Pow((double)appUser.Latitude - (double)wp.Latitude, 2) + Math.Pow((double)appUser.Longitude - (double)wp.Longitude, 2))
                 .FirstAsync();

                var radius = 6371;
                var dLat = Deg2Rad((Convert.ToDouble(appUser.Latitude - nearestWreckPopulation.Latitude)));
                var dLong = Deg2Rad((Convert.ToDouble(appUser.Longitude - nearestWreckPopulation.Longitude)));

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(Deg2Rad(Convert.ToDouble(appUser.Latitude))) * Math.Cos(Deg2Rad(Convert.ToDouble(nearestWreckPopulation.Latitude))) *
                    Math.Sin(dLong / 2) * Math.Sin(dLong / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double d = radius * c;

                distanceKm = (float)d;

                return (nearestWreckPopulation, distanceKm);
            }

            return (null, 0);
        }

        private double Deg2Rad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
