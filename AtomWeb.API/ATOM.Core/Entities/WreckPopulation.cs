using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class WreckPopulation : BaseEntity
    {
        [Column(TypeName = "decimal(8,6)")]
        public decimal Longitude { get; set; }        //boylam

        [Column(TypeName = "decimal(8,6)")]
        public decimal Latitude { get; set; }         //enlem
        public int People { get; set; }
        public bool? IsClaimed { get; set; }
        public int DistrictId { get; set; }

        [JsonIgnore]
        public District District { get; set; }
    }
}
