﻿using JwtUser.Core.DTOs.Request;
using JwtUser.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Core.Services
{
    public interface IWreckDemandService : IGenericService<WreckDemand>
    {
        public Task AddWreckDemand(AddWreckDemandDto wreckDemand);
        public Task<(float AverageLatitude, float AverageLongitude)> AverageWreckLocation();

    }
}
