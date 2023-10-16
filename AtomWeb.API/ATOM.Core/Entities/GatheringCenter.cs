using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class GatheringCenter : BaseCenter
    {


        [JsonIgnore]
        public ICollection<HelpDemand> HelpDemands { get; set; }

        [JsonIgnore]
        public ICollection<HelpPopulation> HelpPopulation { get; set; }
    }
}
