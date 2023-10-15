using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class County : BaseEntity        //İlçe
    {
        public string Name { get; set; }


        public int CityId { get; set; }


        [JsonIgnore]
        public City City { get; set; }


        [JsonIgnore]
        public ICollection<District> Districts { get; set; }
    }
}
