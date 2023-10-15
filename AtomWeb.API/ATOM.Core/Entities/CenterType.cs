using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class CenterType : BaseEntity
    {
        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<HelpCenter> HelpCenters { get; set; }

        [JsonIgnore]
        public ICollection<GatheringCenter> GatheringCenters { get; set; }
    }
}
