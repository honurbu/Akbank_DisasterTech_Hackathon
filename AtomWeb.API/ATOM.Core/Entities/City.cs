using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<County> Counties { get; set; }
    }
}
