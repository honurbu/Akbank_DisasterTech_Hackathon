using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JwtUser.Core.DTOs.Request
{
    public class AddWreckDemandDto
    {
        public AddWreckDemandDto()
        {
            this.Date  = DateTime.Now;
        }
        public float Longitude { get; set; }        //boylam
        public float Latitude { get; set; }         //enlem
        public string DistrictName { get; set; }
        public string CountyName { get; set; }      //???

        [JsonIgnore]
        public DateTime Date { get; set; }
    }
}
