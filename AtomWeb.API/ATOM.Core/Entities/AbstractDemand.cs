using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public abstract class AbstractDemand
    {
        public AbstractDemand()
        {
            this.Date = DateTime.Now;
        }

        public int Id { get; set; }

        [Column(TypeName = "decimal(8,6)")]
        public decimal Longitude { get; set; }        //boylam

        [Column(TypeName = "decimal(8,6)")]
        public decimal Latitude { get; set; }         //enlem
        public int DistrictId { get; set; }
    

        [JsonIgnore]
        public DateTime Date { get; set; }

    }
}
