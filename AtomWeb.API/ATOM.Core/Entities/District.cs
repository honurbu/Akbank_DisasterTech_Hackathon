using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class District : BaseEntity       //Mahalle
    {
        public string Name { get; set; }


        public int CountyId { get; set; }
        [JsonIgnore]
        public County County { get; set; }


        [JsonIgnore]
        public ICollection<HelpDemand> HelpDemands { get; set; }


        [JsonIgnore]
        public ICollection<WreckDemand> WreckDemands { get; set; }

        [JsonIgnore]
        public WreckPopulation WreckPopulation { get; set; }


    }
}
