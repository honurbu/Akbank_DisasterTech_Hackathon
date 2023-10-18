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

        public async Task<List<WreckDemand>> GetPeopleLocation(int wreckId)
        {
            var wrecks = await _dbContext.WreckPopulations.FindAsync(wreckId);
            int distId =wrecks.DistrictId;

            var values = await _dbContext.WreckDemands.Where(x=>x.DistrictId==distId).ToListAsync();

            return values;

        }

        public async Task<(WreckPopulation, float distance)> GetWreckOperation(string id)
        {
            var appUser = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            double distanceKm = 0;

            if (appUser != null)
            {
                var nearestWreckPopulations = await _dbContext.WreckPopulations
            .Where(x => x.IsClaimed == false)
            .Include(x => x.District)
            .ThenInclude(x => x.County)
            .ToListAsync();

                var nearestWreckPopulation = nearestWreckPopulations
                    .OrderBy(wp => CalculateDistance((double)appUser.Latitude, (double)appUser.Longitude, (double)wp.Latitude, (double)wp.Longitude))
                    .First();

                distanceKm = CalculateDistance((double)appUser.Latitude, (double)appUser.Longitude, (double)nearestWreckPopulation.Latitude, (double)nearestWreckPopulation.Longitude);

                return (nearestWreckPopulation, (float)distanceKm);
            }

            return (null, 0);
        }

        public void RemoveWreck(int wreckPopId)
        {
            var wrecks =  _dbContext.WreckPopulations.Find(wreckPopId);
            int distId = wrecks.DistrictId;

            var values = _dbContext.WreckDemands.Where(x => x.DistrictId == distId).ToListAsync();

            _dbContext.Remove(values);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var radius = 6371; // Dünya'nın yarıçapı (km)
            var dLat = Deg2Rad(lat2 - lat1);
            var dLon = Deg2Rad(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = radius * c;

            return distance;
        }

        private double Deg2Rad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

    }
}

