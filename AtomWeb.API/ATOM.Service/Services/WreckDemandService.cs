﻿using ATOM.Core.DTOs;
using ATOM.Core.DTOs.Request;
using ATOM.Core.DTOs.Response;
using ATOM.Core.Entities;
using ATOM.Core.Repositories;
using ATOM.Core.Services;
using ATOM.Core.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Service.Services
{
    public class WreckDemandService : GenericService<WreckDemand>, IWreckDemandService
    {
        private readonly IWreckDemandRepository _wreckDemandRepository;
        private readonly IMapper _mapper;

        public WreckDemandService(IGenericRepository<WreckDemand> genericRepository, IUnitOfWork unitOfWork, IWreckDemandRepository wreckDemandRepository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _wreckDemandRepository = wreckDemandRepository;
            _mapper = mapper;
        }

        public async Task AddWreckDemand(AddWreckDemandDto wreckDemand)
        {
            await _wreckDemandRepository.AddWreckDemand(wreckDemand);
        }

        public async Task AverageWrackPop(AddWreckDemandDto wreckDemand)
        {
            await _wreckDemandRepository.AverageWrackPop(wreckDemand);
        }

        public async Task<(decimal AverageLatitude, decimal AverageLongitude)> AverageWreckLocation()
        {
            return await _wreckDemandRepository.AverageWreckLocation();
        }

        public void ChangeStatus(int wreckPopId)
        {
            _wreckDemandRepository.ChangeStatus(wreckPopId);
        }

        public async Task<List<PeopleLocationDto>> GetPeopleLocation(int wreckId)
        {
            var locations = await _wreckDemandRepository.GetPeopleLocation(wreckId);
            var locationsDto = _mapper.Map<List<PeopleLocationDto>>(locations);
            return locationsDto;
        }

        public async Task<(WreckPopulation, float distance)> GetWreckOperation(string id)
        {
            return await _wreckDemandRepository.GetWreckOperation(id);
        }

        public async void RemoveWreck(int wreckPopId)
        {
            _wreckDemandRepository.RemoveWreck(wreckPopId);
        }
    }
}
