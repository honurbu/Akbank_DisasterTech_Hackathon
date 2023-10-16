using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ATOM.Core.DTOs.Request
{
    public class AddWreckDemandDto
    {
        public AddWreckDemandDto()
        {
            CountyName = "";
            this.Date = DateTime.Now;
        }

        [Column(TypeName = "decimal(8,6)")]
        public decimal Longitude { get; set; }        //boylam

        [Column(TypeName = "decimal(8,6)")]
        public decimal Latitude { get; set; }         //enlem

        public string DistrictName { get; set; }
        public string? CountyName { get; set; }      //???

        public bool? IsClaimed { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }
    }
}
