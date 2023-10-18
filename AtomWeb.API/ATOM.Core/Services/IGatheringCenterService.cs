﻿using ATOM.Core.DTOs.Response;
using ATOM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Services
{
    public interface IGatheringCenterService : IGenericService<GatheringCenter>
    {
        Task<(BaseCenter, float distance)> NearGatheringCenter(decimal longitude, decimal latitude);
    }
}
