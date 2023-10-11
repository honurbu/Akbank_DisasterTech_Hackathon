using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwtUser.Service.Mapping;

namespace JwtUser.Service.Mapping
{
    public static class ObjectMapper
    {

        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapProfile>();
            });

            return config.CreateMapper();

        });

        public static IMapper Mapper => lazy.Value;


    }
}
