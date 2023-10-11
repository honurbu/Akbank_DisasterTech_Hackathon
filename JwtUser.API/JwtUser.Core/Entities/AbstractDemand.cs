using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JwtUser.Core.Entities
{
    public abstract class AbstractDemand
    {
        public AbstractDemand()
        {
            this.Date = DateTime.Now;
        }

        public int Id { get; set; }

        public float Longitude { get; set; }        //boylam
        public float Latitude { get; set; }         //enlem
        public int DistrictId { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

    }
}
