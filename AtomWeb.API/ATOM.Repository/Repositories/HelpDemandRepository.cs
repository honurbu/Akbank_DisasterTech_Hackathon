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

                        GatheringCenterId = helpDemand.GatheringCenterId
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

            await AverageHelpPop(helpDemand);
        }

        public async Task<(decimal AverageLatitude, decimal AverageLongitude)> AverageHelpLocation(int id)
        {
            decimal averageLatitude = await _dbContext.HelpDemands.Where(x => x.CategoryId == id).AverageAsync(x => x.Latitude);
            decimal averageLongitude = await _dbContext.HelpDemands.Where(x => x.CategoryId == id).AverageAsync(x => x.Longitude);

            return (averageLatitude, averageLongitude);
        }


        public async Task AverageHelpPop(AddHelpDemandDto helpDemantDto)
        {
            var district = await _dbContext.Districts.FirstOrDefaultAsync(x => x.Name == helpDemantDto.DistrictName);
            var districtId = district!.Id;

            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == helpDemantDto.CategoryId);
            var categoryId = category!.Id;

            var checkHelpPop = await _dbContext.HelpPopulations.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.DistrictId == districtId);

            if (checkHelpPop == null)
            {
                if (helpDemantDto.HelpCenterId == null && helpDemantDto.GatheringCenterId == null)
                {
                    HelpPopulation helpPopulation = new HelpPopulation
                    {
                        DistrictId = districtId,
                        CategoryId = categoryId,
                        Latitude = helpDemantDto.Latitude,
                        Longitude = helpDemantDto.Longitude,
                        People = 1,
                        GatheringCenterId = helpDemantDto.GatheringCenterId,
                        HelpCenterId = helpDemantDto.HelpCenterId
                    };

                    await _dbContext.HelpPopulations.AddAsync(helpPopulation);
                }
            }
            else
            {
                checkHelpPop!.Latitude = ((checkHelpPop.Latitude * checkHelpPop.People) + helpDemantDto.Latitude) / (checkHelpPop.People + 1);
                checkHelpPop!.Longitude = ((checkHelpPop.Longitude * checkHelpPop.People) + helpDemantDto.Longitude) / (checkHelpPop.People + 1);

                checkHelpPop.People++;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}